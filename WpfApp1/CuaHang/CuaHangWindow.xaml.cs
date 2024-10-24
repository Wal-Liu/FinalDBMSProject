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
            MoKetNoi(); 
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void loadSanPham()
        {
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
                        SqlDataReader reader = command.ExecuteReader();
                        int ID = 1;
                        while (reader.Read())
                        {
                            string item = $"{ID}. MaSP: {reader["maSP"]},           TenSP: {reader["tenSP"]},               SoLuong: {reader["soLuong"]}";
                            lstSP.Items.Add(item);
                            ID++;
                        }
                    }
                }
            }
        }
        private void MoKetNoi()
        {
            try
            {
                if (sqlcon == null)
                {
                    sqlcon = new SqlConnection(strCon);
                }
                sqlcon = new SqlConnection(strCon);
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                    //MessageBox.Show("Ket noi thanh cong");
                    loadSanPham(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
