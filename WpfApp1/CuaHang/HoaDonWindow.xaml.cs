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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HoaDonWindow.xaml
    /// </summary>
    public partial class HoaDonWindow : Window
    {   int donGia;
        string strCon = @"Data Source=QUOCTHINH;Initial Catalog=QuanLySanPham;Integrated Security=True";
        SqlConnection sqlcon = null;
        public HoaDonWindow(string maSP, string tenSP, int soLuong)
        {   
           
            InitializeComponent();
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {   
                    
                    using (SqlCommand command = new SqlCommand("proc_giaHoaDon", connection))
                    {
                        command.Parameters.AddWithValue("@maSP", maSP);
                        command.Parameters.AddWithValue("@soLuong", soLuong);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();

                       while (reader.Read())
                        {
                            Price.Content = reader["dongia"].ToString();
                        }
                    }
                }
            }
            ProductName.Content = tenSP;
            QuantityLabel.Content = soLuong;
        }
        BanHangWindow banHangWindow = new BanHangWindow(1);
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
