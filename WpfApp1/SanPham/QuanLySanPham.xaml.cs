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
        }
}
