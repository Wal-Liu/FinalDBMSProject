using System;
using System.Collections.Generic;
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
                        TaoTaiKhoan taoTaiKhoan = new TaoTaiKhoan();
                        taoTaiKhoan.Show();
                }

                private void btnQuanLyKho_Click(object sender, RoutedEventArgs e)
                {
                        ChonKhoWindow chonKho = new ChonKhoWindow();
                        chonKho.Show();
                }

                private void btnQuanLySanPham_Click(object sender, RoutedEventArgs e)
                {
                        QuanLySanPham quanLySanPham = new QuanLySanPham();
                        quanLySanPham.Show();
                }

                private void btnQuanLyCuaHang_Click(object sender, RoutedEventArgs e)
                {
                        ChonCuaHang chonCuaHang = new ChonCuaHang();
                        chonCuaHang.Show();
                }
        }
}
