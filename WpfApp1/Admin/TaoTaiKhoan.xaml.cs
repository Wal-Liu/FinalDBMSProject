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
            string vitri = (cbbViTri.SelectedItem as ComboBoxItem).Tag.ToString();

            if (checkExit(tentk) == true)
            {
                MessageBox.Show("Tài Khoản đã tồn tại!");
                return;
            }
            if (tentk.Length == 0)
            {
                MessageBox.Show("Vui Lòng điền đầy đủ thông tin");      
                return;
            }
            using (SqlConnection connection = DBConnection.connect())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("proc_TaoTaiKhoan", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@tenTaiKhoan", tentk); 
                    command.Parameters.AddWithValue("@MatKhau", mk); 
                    command.Parameters.AddWithValue("@Role", vitri);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("thanh Cong");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("That Bai: " + ex.Message);
                    }
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
                    try
                    {

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (tenTK == reader["name"].ToString())
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("That Bai: " + ex.Message);
                    }
                }
            }
            return false;
        }
    }
}
