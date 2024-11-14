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
using WpfApp1.CuaHang;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HoaDonWindow.xaml
    /// </summary>
    public partial class HoaDonWindow : Window
    {

        private int MaCH;
        private List<HoaDon> listHoaDon;
        public HoaDonWindow(int maCH, List<HoaDon> list)
        {
            MaCH = maCH;
            this.listHoaDon = list;
            InitializeComponent();
            loadHoaDon();
            tinhTien();
        }

        private void btnXuatHoaDon_Click(object sender, RoutedEventArgs e)
        {
            foreach (HoaDon hoaDon in listHoaDon)
            {
                string maSP = hoaDon.lblMaSP.Content.ToString();
                int soLuong = int.Parse(hoaDon.tbxSoLuong.Text);
                using (SqlCommand command = new SqlCommand("proc_BanSPTuCH", DBConnection.connect()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@maSP", maSP);
                    command.Parameters.AddWithValue("@maCH", MaCH);
                    command.Parameters.AddWithValue("@soLuong", soLuong);
                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show($"{rowsAffected} rows affected.");
                }


            }
        }

        private int giaTienMoiSanPham(string maSP, string SL)
        {
            int thanhtien = 0;
                using (SqlConnection connection = DBConnection.connect())
                {
                    using (SqlCommand command = new SqlCommand("SELECT dbo.func_giaHoaDon(" + maSP + ", " + SL + ")", connection))
                    {
                        //command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@maSP", int.Parse(maSP));
                        //command.Parameters.AddWithValue("@soLuong", int.Parse(SL));
                        //MessageBox.Show(maSP + ", " + SL); 

                        thanhtien = int.Parse(command.ExecuteScalar().ToString());
                        //MessageBox.Show(thanhtien.ToString()); 

                    }
            }
            //MessageBox.Show(thanhtien.ToString());
            return thanhtien;
        }


        private void tinhTien()
        {

            double TongTien = 0;
            //MessageBox.Show(lstHoaDon.Items.Count.ToString());

            foreach (HoaDon hoaDon in listHoaDon)
            {
                //MessageBox.Show(MaCH.ToString());

                string maSP = hoaDon.lblMaSP.Content.ToString();
                string sl = hoaDon.tbxSoLuong.Text;
                int giaTienMoiSP = giaTienMoiSanPham(maSP, sl);
                //MessageBox.Show(maSP, sl);
                TongTien += giaTienMoiSP;

            }
            lblTongTien.Content = TongTien.ToString();
        }

        private void loadHoaDon()
        {
            foreach (HoaDon hoaDon in listHoaDon)
            {
                lstHoaDon.Items.Add(hoaDon);
            }
        }
    }

}
