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
                        MoKetNoi();
                }

                        private void btnNhapHang_Click(object sender, RoutedEventArgs e)
                        {
                                bool isOnlyNumbers = CheckIfOnlyNumbers(txtSoLuong.Text);
                                if (isOnlyNumbers)
                                {
                                        String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                        String maSP  = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                                        int soLuong = int.Parse(txtSoLuong.Text);

                                        using (SqlConnection connection = new SqlConnection(strCon))
                                        {
                                                connection.Open();
                                                using (SqlCommand command = new SqlCommand("proc_ThemSPVaoKho", connection))
                                                {
                                                        command.CommandType = CommandType.StoredProcedure;
                                                        command.Parameters.AddWithValue("@maSP", maSP);
                                                        command.Parameters.AddWithValue("@maKho", 1);
                                                        command.Parameters.AddWithValue("@soLuong",soLuong);

                                                        int rowsAffected = command.ExecuteNonQuery(); // Use ExecuteReader for SELECT queries

                                                        // Display the number of affected rows (if applicable)
                                                        MessageBox.Show($"{rowsAffected} rows affected.");
                                                }
                                        }
                                        MessageBox.Show("Thanh cong");
                                }
                                else
                                {
                                        MessageBox.Show("Số lượng không được để trống và chỉ được nhập số. Vui lòng nhập lại");
                                        txtSoLuong.Text = string.Empty;
                                }

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
                                                        }
                                                }
                                        }
                                }
                        }


                        private void MoKetNoi()
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
                                                //MessageBox.Show("Ket noi thanh cong");
                                                loadSanPham();
                                        }
                                }
                                catch (Exception ex)
                                {
                                        MessageBox.Show(ex.Message);

                                }
                        }
                        static bool CheckIfOnlyNumbers(string str)
                        {
                                if (string.IsNullOrEmpty(str))
                                {
                                        return false;
                                }
                                foreach (char c in str)
                                {
                                        if (!char.IsDigit(c))
                                        {
                                                return false; 
                                        }
                                }
                                return true;
                        }
        }
}
