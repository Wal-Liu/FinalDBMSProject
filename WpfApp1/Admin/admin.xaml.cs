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
                using (SqlCommand command = new SqlCommand("proc_LayHetTaiKhoan", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.ExecuteNonQuery();
                        TaoTaiKhoan taoTaiKhoan = new TaoTaiKhoan();
                        taoTaiKhoan.Show(); 
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
            using (SqlConnection connection = DBConnection.connect())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("proc_LayHetKho", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.ExecuteNonQuery();
                        ChonKhoWindow chonKho = new ChonKhoWindow();
                        chonKho.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("That Bai: " + ex.Message);
                    }
                }
            }
        }

        private void btnQuanLySanPham_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = DBConnection.connect())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("proc_LayHetSanPham", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.ExecuteNonQuery();
                        QuanLySanPham quanLySanPham = new QuanLySanPham();
                        quanLySanPham.Show();
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
            using (SqlConnection connection = DBConnection.connect())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("proc_LayHetCuaHang", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.ExecuteNonQuery();
                        ChonCuaHang chonCuaHang = new ChonCuaHang();
                        chonCuaHang.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("That Bai: " + ex.Message);
                    }
                }
            }
        }
    }
}
