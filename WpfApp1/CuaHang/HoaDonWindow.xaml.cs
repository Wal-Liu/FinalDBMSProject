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
                string strCon = Globals.strcon;
                SqlConnection sqlcon = null;

                private int MaCH;
                private List<HoaDon> listHoaDon;
                public HoaDonWindow(int maCH, List<HoaDon> list)
                {
                        MaCH = maCH;
                        this.listHoaDon = list;
                        InitializeComponent();
                        MoKetNoi();
                        loadHoaDon();
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
                                }
                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show(ex.Message);

                        }
                }

                private void btnXuatHoaDon_Click(object sender, RoutedEventArgs e)
                {

                }

                private int giaTienMoiSanPham(string maSP, string SL)
                {
                        int thanhtien = 0;
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        using (SqlCommand command = new SqlCommand("fun_tinhTien", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                command.Parameters.AddWithValue("@soLuong", SL);
                                               
                                                thanhtien = (int)(decimal)command.ExecuteScalar();

                                        }
                                }
                        }
                        MessageBox.Show(thanhtien.ToString());
                        return thanhtien;
                }


                private void tinhTien()
                {

                        double TongTien = 0;
                                MessageBox.Show(lstHoaDon.Items.Count.ToString());

                        foreach (HoaDon hoaDon  in listHoaDon)
                        {
                                MessageBox.Show(MaCH.ToString());

                                string maSP = hoaDon.lblMaSP.Content.ToString();
                                string sl = hoaDon.tbxSoLuong.Text;
                                int giaTienMoiSP = giaTienMoiSanPham(maSP,sl);
                                MessageBox.Show(maSP, sl);
                                TongTien += giaTienMoiSP;

                        }
                        lblTongTien.Content = TongTien.ToString() ;
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
