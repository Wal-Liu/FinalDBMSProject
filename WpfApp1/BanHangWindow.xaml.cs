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
    /// Interaction logic for BanHangWindow.xaml
    /// </summary>
    public partial class BanHangWindow : Window
    {
        public BanHangWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void tbxSoLuong(object sender, TextChangedEventArgs e)
        {

        }

        private void tbxMasp(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HoaDonWindow hoaDonWindow = new HoaDonWindow();
            hoaDonWindow.Show();

            this.Close();
        }
    }
}
