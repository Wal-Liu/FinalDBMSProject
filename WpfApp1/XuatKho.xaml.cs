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

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for XuatKho.xaml
        /// </summary>
        public partial class XuatKho : Window
        {
                public XuatKho()
                {
                        InitializeComponent();
                }

                private void btnXuatHang_Click(object sender, RoutedEventArgs e)
                {
                        MessageBox.Show("Xuat Hang thanh cong");
        }

        private void cbbSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
