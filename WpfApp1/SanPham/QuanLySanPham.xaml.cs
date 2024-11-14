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
                        loadSanPham();

                }
                private int MaSPDuocChon = -1;
                private void ListView_SelectionChanged()
                {
                        // Kiểm tra nếu có item nào được
                        if (lblList.SelectedIndex < 0)
                        {
                                MessageBox.Show("vui lòng chọn 1 sản phẩm");
                                return;
                        }
                        else
                        {
                                //string selectedItem = lblList.SelectedIndex.ToString();

                                string selectedItem = lblList.SelectedItem.ToString();
                                MessageBox.Show("Bạn đã chọn: " + selectedItem);


                                String maSP = (lblList.SelectedItem as QuanLySanPham).Tag.ToString();
                                MessageBox.Show(maSP);
                                MaSPDuocChon = int.Parse(maSP);
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
                                        lblList.Items.Clear();
                                        while (reader.Read())
                                        {
                                                QuanLySP quanLySP = new QuanLySP();
                                                quanLySP.lblID.Content = reader["maSP"];
                                                quanLySP.lblTenSP.Content = reader["tenSP"];
                                                quanLySP.lblDonGia.Content = reader["donGia"];
                                                quanLySP.lblMoTa.Items.Add(reader["moTa"]);
                                                quanLySP.lblLoaiSP.Content = MaLoaiSPToLoaiSP(reader["maLoaiSP"].ToString());
                                                ListViewItem item = new ListViewItem();

                                                item.Tag = quanLySP.lblID.Content;
                                                lblList.Items.Add(quanLySP);
                                        }
                                }
                        }
                        //lblList.SelectionChanged += ListView_SelectionChanged;

                }

                private void btnThem_Click(object sender, RoutedEventArgs e)
                {
                        ThemSuaSP themSuaSP = new ThemSuaSP();
                        themSuaSP.Show();
                }

                private void btnSua_Click(object sender, RoutedEventArgs e)
                {
                        ListView_SelectionChanged();
                        if (MaSPDuocChon == -1) return;

                        ThemSuaSP themSuaSP = new ThemSuaSP(MaSPDuocChon);
                        themSuaSP.Show();
                }

                private void btnXoa_Click(object sender, RoutedEventArgs e)
                {
                        ListView_SelectionChanged();
                        if (MaSPDuocChon == -1) return;

                        bool successful = false;
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_XoaSanPham", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@tenSP", MaSPDuocChon);
                                        SqlDataReader reader = command.ExecuteReader();
                                        int rowsAffected = command.ExecuteNonQuery();
                                        if (rowsAffected > 0) successful = true;
                                }
                        }
                        if (successful == true)
                        {
                                MessageBox.Show("Xoá thành công");
                                this.Close();
                        }
                        else
                                MessageBox.Show("vui lòng thử lại");
                }

                private void btnLoaiSP_Click(object sender, RoutedEventArgs e)
                {
                        QuanLyLoaiSanPham quanLyLoaiSanPham = new QuanLyLoaiSanPham();
                        quanLyLoaiSanPham.Show();
                }
                private string MaLoaiSPToLoaiSP(string maLoaiSP)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT dbo.func_LayTenLoaiSP(" + maLoaiSP + ")", connection))
                                {
                                        var tenLoaiSP = command.ExecuteScalar().ToString();
                                        return tenLoaiSP;
                                }
                        }
                }
        }
}
