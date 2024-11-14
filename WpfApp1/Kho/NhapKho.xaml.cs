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
                private int MaKho;
                public NhapKho(int maKho)
                {
                        InitializeComponent();
                        MaKho = maKho;
                }

                private void btnNhapHang_Click(object sender, RoutedEventArgs e)
                {
                        bool isOnlyNumbers = CheckIfOnlyNumbers(txtSoLuong.Text);
                        bool isEmpty = CheckIfNull(txtSoLuong.Text);
                        if (!isEmpty || cbbSanPham.SelectedItem == null)
                        {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        }
                        else
                        {
                                if (isOnlyNumbers)
                                {
                                        String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                                        int soLuong = int.Parse(txtSoLuong.Text);

                                        bool successful = false;
                                        using (SqlConnection connection = DBConnection.connect())
                                        {
                                                connection.Open();
                                                using (SqlCommand command = new SqlCommand("proc_ThemSPVaoKho", connection))
                                                {
                                                        command.CommandType = CommandType.StoredProcedure;
                                                        command.Parameters.AddWithValue("@maSP", maSP);
                                                        command.Parameters.AddWithValue("@maKho", MaKho);
                                                        command.Parameters.AddWithValue("@soLuong", soLuong);

                                                        int rowsAffected = command.ExecuteNonQuery(); // Use ExecuteReader for SELECT queries
                                                        if (rowsAffected > 0) successful = true;
                                                        // Display the number of affected rows (if applicable)
                                                        //MessageBox.Show($"{rowsAffected} rows affected.");
                                                }
                                        }
                                        if (successful == true)
                                        {
                                                MessageBox.Show("Thanh cong");
                                                this.Close();
                                        }
                                        else
                                                MessageBox.Show("vui lòng thử lại");
                                }
                                else
                                {
                                        MessageBox.Show("Số lượng không được để trống và chỉ được nhập số. Vui lòng nhập lại");
                                        txtSoLuong.Text = string.Empty;
                                }
                        }

                }

                private void loadSanPham()
                {
                        using (SqlConnection connection = DBConnection.connect())
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

                static bool CheckIfOnlyNumbers(string str)
                {
                        foreach (char c in str)
                        {
                                if (!char.IsDigit(c))
                                {
                                        return false;
                                }
                        }
                        return true;
                }
                static bool CheckIfNull(string str)
                {
                        if (string.IsNullOrEmpty(str))
                        {
                                return false;
                        }
                        return true;
                }
        }
}
