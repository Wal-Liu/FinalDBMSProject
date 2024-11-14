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
using WpfApp1.CuaHang;
using WpfApp1.Object;
using WpfApp1.UC;

namespace WpfApp1.SanPham
{
        /// <summary>
        /// Interaction logic for QuanLySanPham.xaml
        /// </summary>
        public partial class QuanLySanPham : Window
        {
                public QuanLySanPham()
                {
                        InitializeComponent();
                        
                        lblList.Items.Add("Item 1");
                        lblList.Items.Add("Item 2");
                        lblList.Items.Add("Item 3");

                        lblList.SelectionChanged += ListView_SelectionChanged;

                }
                private void ListView_SelectionChanged(object sender, EventArgs e)
                {
                        // Kiểm tra nếu có item nào được chọn
                        if (lblList.SelectedItems != null)
                        {
                                // Lấy item được chọn
                                string selectedItem = lblList.SelectedItem.ToString();
                                MessageBox.Show("Bạn đã chọn: " + selectedItem);
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
                                                int ID = 1;
                                                lblList.Items.Clear();
                                                while (reader.Read())
                                                {
                                                        QuanLySP quanLySP = new QuanLySP();
                                                        quanlySP.lblID.Content = ID;
                                                        quanLySP.lblTenSP.Content = reader["tenSP"];
                                                        quanLySP.lblDonGia.Content = reader["donGia"];
                                                        quanLySP.lblMoTa.Items.Add(reader["moTa"]);
                                                        quanLySP.lblLoaiSP.Content = MaLoaiSPToLoaiSP(reader["maLoaiSP"].ToString());
                                                        ID++;
                                                }
                                        }
                                }
                        }
                }
                private void btnThem_Click(object sender, RoutedEventArgs e)
                {
                        ThemSuaSP themSuaSP = new ThemSuaSP();
                        themSuaSP.Show();
                }

                private void btnSua_Click(object sender, RoutedEventArgs e)
                {
                        ThemSuaSP themSuaSP = new ThemSuaSP();
                        themSuaSP.Show();
                }

                private void btnXoa_Click(object sender, RoutedEventArgs e)
                {
                        MessageBox.Show("Xoá");
                }

                private void btnLoaiSP_Click(object sender, RoutedEventArgs e)
                {
                        QuanLyLoaiSanPham quanLyLoaiSanPham = new QuanLyLoaiSanPham();
                        quanLyLoaiSanPham.Show();
                }
                private string MaLoaiSPToLoaiSP(string maLoaiSP)
                {
                        return "chua goi func";
                }
        }
}
