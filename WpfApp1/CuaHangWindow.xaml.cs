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
    /// Interaction logic for CuaHangWindow.xaml
    /// </summary>
    public partial class CuaHangWindow : Window
    {
        public CuaHangWindow()
        {
            InitializeComponent();
        }
                private void btnBack_Click(object sender, RoutedEventArgs e)
                {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void btnBan_Click(object sender, RoutedEventArgs e)
        {
            BanHangWindow banHangWindow = new BanHangWindow();
            banHangWindow.Show();
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            NhapHangWindow nhapHangWindow = new NhapHangWindow();
            nhapHangWindow.Show();
        }
    }
}
