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
using WpfApp1.CuaHang;
using WpfApp1.Kho;
using WpfApp1.SanPham;

namespace WpfApp1.Admin
{
        /// <summary>
        /// Interaction logic for admin.xaml
        /// </summary>
        public partial class admin : Window
        {
                public admin()
                {
                        InitializeComponent();
                }

                private void btnTaoTK_Click(object sender, RoutedEventArgs e)
                {

                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT HAS_PERMS_BY_NAME('proc_TaoTaiKhoan', 'OBJECT', 'EXECUTE')", connection))
                                {
                                        try
                                        {

                                                int result = int.Parse(command.ExecuteScalar().ToString());
                                                if (result == 1)
                                                {
                                                        TaoTaiKhoan taoTaiKhoan = new TaoTaiKhoan();
                                                        taoTaiKhoan.Show();
                                                }
                                                else
                                                {
                                                        MessageBox.Show("That Bai: Khong Co Quyen");
                                                }
                                        }
                                        catch (Exception ex)
                                        {
                                                MessageBox.Show("That Bai: " + ex.Message);
                                        }
                                }
                        }
                }

                private void btnQuanLyKho_Click(object sender, RoutedEventArgs e)
                {
                        ChonKhoWindow chonKho = new ChonKhoWindow();
                        chonKho.Show();
                }

                private void btnQuanLySanPham_Click(object sender, RoutedEventArgs e)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT HAS_PERMS_BY_NAME('proc_SuaSanPham', 'OBJECT', 'EXECUTE')", connection))
                                {
                                        try
                                        {

                                                int result = int.Parse(command.ExecuteScalar().ToString());
                                                if (result == 1)
                                                {
                                                        QuanLySanPham quanLySanPham = new QuanLySanPham();
                                                        quanLySanPham.Show();
                                                }
                                                else
                                                {
                                                        MessageBox.Show("That Bai: Khong Co Quyen");
                                                }
                                        }
                                        catch (Exception ex)
                                        {
                                                MessageBox.Show("That Bai: " + ex.Message);
                                        }
                                }
                        }
                }

                private void btnQuanLyCuaHang_Click(object sender, RoutedEventArgs e)
                {
                        ChonCuaHang chonCuaHang = new ChonCuaHang();
                        chonCuaHang.Show();
                }

                private void btnDangXuat_Click(object sender, RoutedEventArgs e)
                {
                        try
                        {
                                DBConnection.logout();
                                MessageBox.Show("Đã đăng xuất khỏi hệ thống");
                                this.Close();
                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show("Thử Lại: " + ex.Message);
                        }
                }
        }
}
