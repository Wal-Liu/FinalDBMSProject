using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for NhapKho.xaml
        /// </summary>
        public partial class NhapKho : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=QLSanPham;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;          
                public NhapKho()
                {
                        InitializeComponent();
                        btnMo_Click();
                }

                private void btnNhapHang_Click(object sender, RoutedEventArgs e)
                {
                        MessageBox.Show("Nhap Hang thanh cong");
                }

                private void loadSanPham()
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("proc_LayHetSanPham", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                SqlDataReader reader = command.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                                                        comboBoxItem.Content = reader["tenSP"].ToString();
                                                        comboBoxItem.Tag = reader["maSP"].ToString();
                                                        cbbSanPham.Items.Add(comboBoxItem);

                                                        MessageBox.Show(comboBoxItem.Tag.ToString());

                                                }
                                        }
                                }
                        }
                }


                private void btnMo_Click()
                {
                        try
                        {
                                if (sqlcon == null)
                                {
                                        sqlcon = new SqlConnection(strCon);
                                }
                                sqlcon = new SqlConnection(strCon);
                                if (sqlcon.State == ConnectionState.Closed)
                                {
                                        sqlcon.Open();
                                        MessageBox.Show("Ket noi thanh cong");
                                        loadSanPham();
                                }
                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show(ex.Message);

                        }
                }
        }
}
