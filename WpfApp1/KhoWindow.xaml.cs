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
    /// Interaction logic for KhoWindow.xaml
    /// </summary>
    public partial class KhoWindow : Window
    {
        public KhoWindow()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
        }

                private void btnNhap_Click(object sender, RoutedEventArgs e)
                {
                        NhapKho nhapKho = new NhapKho();
                        nhapKho.Show();
                }

                private void btnXuat_Click(object sender, RoutedEventArgs e)
                {
                        XuatKho xuatKho = new XuatKho();
                        xuatKho.Show();
        }
    }
}
