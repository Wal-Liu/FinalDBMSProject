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
using WpfApp1.Object;
using WpfApp1.CuaHang;

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for BanHangWindow.xaml
        /// </summary>
        public partial class BanHangWindow : Window
        {
                string strCon = Globals.strcon;
                SqlConnection sqlcon = null;
                private int MaCH;
                private int SoLuong;
                public BanHangWindow(int maCH)
                {
                        MaCH = maCH;
                        InitializeComponent();
                        MoKetNoi();
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
                                                SqlDataReader reader = command.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                                                        comboBoxItem.Content = reader["tenSP"].ToString();
                                                        comboBoxItem.Tag = reader["maSP"].ToString();
                                                        cbbSanPham.Items.Add(comboBoxItem);
                                                }
                                        }
                                }
                        }
                }



                private void cbbSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                        int SoLuongToiDa = 0;
                        string placeholderText = "Max: ";

                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("proc_LaySoLuongSanPhamTrongCH", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                command.Parameters.AddWithValue("@maCH", MaCH);
                                                SqlDataReader reader = command.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                        SoLuongToiDa = int.Parse(reader["soLuong"].ToString());
                                                }
                                        }
                                }
                        }
                        SoLuong = SoLuongToiDa;
                        placeholderText += SoLuong.ToString();
                        lblThongBao.Content = placeholderText;
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

                private int SoLuongDonHang = 0;
                private int soluongconlaisaukhithem = 10;
                private void btnThem_Click(object sender, RoutedEventArgs e)
                {
                        bool isOnlyNumbers = CheckIfOnlyNumbers(tbxSoLuong.Text);
                        bool isEmpty = CheckIfNull(tbxSoLuong.Text);
                        if (!isEmpty || cbbSanPham.SelectedItem == null)
                        {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        }
                        else
                        {
                                if (isOnlyNumbers)
                                {
                                        if (int.Parse(tbxSoLuong.Text) > soluongconlaisaukhithem)
                                        {
                                                MessageBox.Show("Số lượng bán ra nhiều hơn số lượng đang có. Vui Lòng Nhập lại");
                                                tbxSoLuong.Text = string.Empty;
                                        }
                                        else
                                        {
                                                SoLuongDonHang++;
                                                String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                                String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                                                int soLuong = int.Parse(tbxSoLuong.Text);
                                                soluongconlaisaukhithem -= soLuong;
                                                lblThongBao.Content = "Max: " + soluongconlaisaukhithem.ToString();
                                                String item = SoLuongDonHang.ToString() + ".  " + tenSP + "  " + tbxSoLuong.Text;
                                                HoaDon hoadon = new HoaDon();
                                                hoadon.lblID.Content = SoLuongDonHang.ToString();
                                                hoadon.lblTenSP.Content = tenSP;
                                                hoadon.lblMaSP.Content = maSP;
                                                hoadon.lblSL.Content = SoLuong;
                                                lstHoaDon.Items.Add(hoadon);
                                               //lstHoaDon.Items.Add(item);

                                        }
                                }
                                else
                                {
                                        MessageBox.Show("Số Lượng chỉ được nhập số. Vui lòng nhập lại");
                                        tbxSoLuong.Text = string.Empty;
                                }
                        }
                }
                static bool CheckIfOnlyNumbers(string str)
                {
                        foreach (char c in str)
                        {
                                if (!char.IsDigit(c))
                                {
                                        return false;
                                }
                        }
                        return true;
                }
                static bool CheckIfNull(string str)
                {
                        if (string.IsNullOrEmpty(str))
                        {
                                return false;
                        }
                        return true;
                }

                private void btnXacNhan_Click(object sender, RoutedEventArgs e)
                {

                }

                private void Button_Click(object sender, RoutedEventArgs e)
                {
                        string tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                        int soLuong = int.Parse(tbxSoLuong.Text);
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        using (SqlCommand command = new SqlCommand("proc_BanSPTuCH", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                command.Parameters.AddWithValue("@maCH", MaCH);
                                                command.Parameters.AddWithValue("@soLuong", soLuong);
                                                SqlDataReader reader = command.ExecuteReader();

                                        }
                                }
                        }

                        HoaDonWindow hoaDonWindow1 = new HoaDonWindow(maSP, tenSP, soLuong);
                        hoaDonWindow1.Show();
                        this.Close();
                }
        }
}
