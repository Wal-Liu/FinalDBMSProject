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

namespace WpfApp1.SanPham
{
        /// <summary>
        /// Interaction logic for QuanLyLoaiSanPham.xaml
        /// </summary>
        public partial class QuanLyLoaiSanPham : Window
        {
                private string MaLoaiSPDuocChon = "";
                public QuanLyLoaiSanPham()
                {
                        InitializeComponent();
                        lstLoaiSP.SelectedIndex = -1;
                        load();
                        lstLoaiSP.SelectionChanged += LstLoaiSP_SelectionChanged;
                        
                }

                private void LstLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        // Kiểm tra nếu có item nào được
                        if (lstLoaiSP.SelectedIndex >= 0)
                        {
                                String maLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Tag.ToString();
                                String tenLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Content.ToString();
                                //MessageBox.Show(maLoaiSP);
                                txbTenLoaiSP.Text = tenLoaiSP;
                                MaLoaiSPDuocChon = maLoaiSP;
                        }
                }

                private void load()
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_LayHetLoaiSP", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        SqlDataReader reader = command.ExecuteReader();
                                        lstLoaiSP.Items.Clear();
                                        while (reader.Read())
                                        {
                                                ListViewItem item  = new ListViewItem();
                                                item.Content = reader["tenLoaiSP"].ToString();
                                                item.Tag = reader["maLoaiSP"].ToString();
                                                lstLoaiSP.Items.Add(item);
                                        }
                                }
                        }
                }

                private void ListView_SelectionChanged()
                {
                        // Kiểm tra nếu có item nào được
                        if (lstLoaiSP.SelectedIndex >= 0)
                        {
                                String maLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Tag.ToString();
                                String tenLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Content.ToString();
                                //MessageBox.Show(maLoaiSP);
                                txbTenLoaiSP.Text = tenLoaiSP;
                                MaLoaiSPDuocChon = maLoaiSP;
                        }
                }

                private void btnXoa_Click(object sender, RoutedEventArgs e)
                {
                        if (lstLoaiSP.SelectedIndex < 0)
                        {
                                MessageBox.Show("vui lòng chọn 1 loại Sản Phẩm");
                                return;
                        }
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_XoaLoaiSP", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@maLoaiSP", MaLoaiSPDuocChon);
                                        SqlDataReader reader = command.ExecuteReader();
                                }
                        }
                        load();

                }
        }
}
