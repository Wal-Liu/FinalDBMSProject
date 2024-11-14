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
using WpfApp1.CuaHang;
using WpfApp1.UC;

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for CuaHangWindow.xaml
        /// </summary>
        public partial class CuaHangWindow : Window
        {
                private int MaCH;
                public CuaHangWindow(int maCH)
                {
                        MaCH = maCH;
                        InitializeComponent();
                }
                private void btnBack_Click(object sender, RoutedEventArgs e)
                {
                        ChonCuaHang chonCuaHang = new ChonCuaHang();
                        chonCuaHang.Show();
                        this.Close();
                }
                private void loadSanPham()
                {
                                using (SqlConnection connection = DBConnection.connect())
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("proc_LayHetSanPhamCH", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maCuaHang", MaCH);
                                                SqlDataReader reader = command.ExecuteReader();
                                                lstSP.Items.Clear();
                                                int ID = 1;
                                                while (reader.Read())
                                                {
                                                        listSP listSP = new listSP();
                                                        listSP.lblID.Content = ID;
                                                        listSP.lblMaSP.Content = reader["maSP"];
                                                        listSP.lblTenSP.Content = reader["tenSP"];
                                                        listSP.lblSL.Content = reader["soLuong"];
                                                        lstSP.Items.Add(listSP);
                                                        ID++;
                                                }
                                        }
                                }
                        }
                private void WindowClosed(object sender, EventArgs e)
                  => loadSanPham();


                private void btnBan_Click(object sender, RoutedEventArgs e)
                {
                        BanHangWindow banHangWindow = new BanHangWindow(MaCH);
                        banHangWindow.Closed += WindowClosed;
                        banHangWindow.Show();
                }

                private void btnNhap_Click(object sender, RoutedEventArgs e)
                {
                        NhapHangWindow nhapHangWindow = new NhapHangWindow(MaCH);
                        nhapHangWindow.Closed += WindowClosed;
                        nhapHangWindow.Show();
                }


        }
}
