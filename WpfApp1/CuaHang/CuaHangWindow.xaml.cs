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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CuaHangWindow.xaml
    /// </summary>
    public partial class CuaHangWindow : Window
    {
        string strCon = Globals.strcon;
        SqlConnection sqlcon = null;
        private int MaCH;
        public CuaHangWindow(int maCH)
        {
            MaCH = maCH;
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void loadSanPham()
        {
            SqlConnection sqlcon = null;
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("proc_LayHetSanPhamCH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@maCuaHang", MaCH);
                        // Sử dụng SqlDataAdapter để đổ dữ liệu vào DataTable
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Gán DataTable làm nguồn dữ liệu cho DataGridView
                        dataGridViewSanPham.DataContext = dataTable;
                    }
                }
            }
        }
        private void btnBan_Click(object sender, RoutedEventArgs e)
        {
            BanHangWindow banHangWindow = new BanHangWindow(MaCH);
            banHangWindow.Show();
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            NhapHangWindow nhapHangWindow = new NhapHangWindow(MaCH);
            nhapHangWindow.Show();
        }


    }
}
