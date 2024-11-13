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
    /// Interaction logic for ThemSuaSP.xaml
    /// </summary>


    public partial class ThemSuaSP : Window
    {
        private int? sanPhamId = null;
        public ThemSuaSP(int? sanPhamId = null)
        {
            InitializeComponent();
            this.sanPhamId = sanPhamId;
        }
    }
}
