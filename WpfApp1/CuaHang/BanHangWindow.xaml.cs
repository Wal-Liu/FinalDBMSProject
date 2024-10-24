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
using WpfApp1.Object;

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for BanHangWindow.xaml
        /// </summary>
        public partial class BanHangWindow : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=QLSanPham;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;
                private int MaCH;
                public BanHangWindow(int maCH)
                {
                        MaCH = maCH;
                        InitializeComponent();
                        loadSanPham();

                }

                private void loadSanPham()
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("proc_LayHetSanPhamCH", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maCuaHang", MaCH);
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



                private void Button_Click(object sender, RoutedEventArgs e)
                {
                        string tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                        int soLuong = int.Parse(tbxSoLuong.Text);
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        using (SqlCommand command = new SqlCommand("proc_BanSPTuCH", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                command.Parameters.AddWithValue("@maCH", MaCH);
                                                command.Parameters.AddWithValue("@soLuong", soLuong);
                                                SqlDataReader reader = command.ExecuteReader();

                                        }
                                }
                        }

                        HoaDonWindow hoaDonWindow1 = new HoaDonWindow(maSP, tenSP, soLuong);
                        hoaDonWindow1.Show();
                        this.Close();
                }

                private void tbxSoLuong_TextChanged(object sender, TextChangedEventArgs e)
                {

                }

                private void cbbSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {

                }
        }
}
