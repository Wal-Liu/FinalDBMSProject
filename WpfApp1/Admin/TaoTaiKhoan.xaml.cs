using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1.Admin
{
        /// <summary>
        /// Interaction logic for TaoTaiKhoan.xaml
        /// </summary>
        public partial class TaoTaiKhoan : Window
        {
                public TaoTaiKhoan()
                {
                        InitializeComponent();
                }

                private void btnGui_Click(object sender, RoutedEventArgs e)
                {
                        string tentk = txbTenDangNhap.Text;
                        string mk = txbMatKhau.Text;
                        if (checkExit(tentk) ==  true )
                        {
                                MessageBox.Show("Tài Khoản đã tồn tại!");
                                return;
                        }
                        if (tentk.Length == 0 )
                        {
                                MessageBox.Show("Vui Lòng điền đầy đủ thông tin");
                                return;
                        }
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        SqlDataReader reader = command.ExecuteReader();
                                }
                        }

                }
                private bool checkExit(string tenTK)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_LayHetTaiKhoan", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                                if (tenTK == reader["tenTaiKhoan"].ToString())
                                                {
                                                        return true;
                                                }
                                        }
                                }
                        }
                        return false;
                }
        }
}
