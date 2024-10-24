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
    /// Interaction logic for KhoWindow.xaml
    /// </summary>
    public partial class KhoWindow : Window
    {
        string strCon = Globals.strcon;
        SqlConnection sqlcon = null;

        private int MaKho;
        public KhoWindow(int maKho)
        {
            InitializeComponent();
            MaKho = maKho;
            MoKetNoi();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            NhapKho nhapKho = new NhapKho(MaKho);
            nhapKho.Show();
        }

        private void btnXuat_Click(object sender, RoutedEventArgs e)
        {
            XuatKho xuatKho = new XuatKho(MaKho);
            xuatKho.Show();
        }

        private void loadSanPham()
        {
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("proc_LayHetSanPhamTrongKho", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@maKho", MaKho);
                        SqlDataReader reader = command.ExecuteReader();
                        int ID = 1;
                        while (reader.Read())
                        {


                            // Using string interpolation for better formatting
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

    }
}
