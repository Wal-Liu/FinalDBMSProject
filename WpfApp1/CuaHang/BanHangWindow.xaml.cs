﻿using System;
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
using WpfApp1.UC;

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

                private int soLuongToiDa(string maSP)
                {
                        int SoLuongToiDa = 0;
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
                        return SoLuongToiDa;
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
                        if (cbbSanPham.SelectedItem == null)
                        {
                                MessageBox.Show("Vui lòng chọn sản phẩm");
                        }
                        else
                        {
                                string itemAdd = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                if (checkExist(itemAdd))
                                {
                                        MessageBox.Show("Sản phẩm đã tồn tại!");
                                }
                                else
                                {
                                        SoLuongDonHang++;
                                        String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                                        HoaDon hoadon = new HoaDon();
                                        hoadon.lblID.Content = SoLuongDonHang.ToString();
                                        hoadon.lblTenSP.Content = tenSP;
                                        hoadon.lblMaSP.Content = maSP;
                                        hoadon.Name = "hoadon" + SoLuongDonHang.ToString();
                                        lstHoaDon.Items.Add(hoadon);
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
                        if(!checkSoLuong())
                        {
                                MessageBox.Show("Số lượng không hợp lệ! \n -Số Lượng không đủ \n -Không được bỏ trống \n -chỉ bao gồm số");
                        }
                        else
                        {
                                List<HoaDon> listhoadon = new List<HoaDon>();
                                foreach (var item in lstHoaDon.Items)
                                {
                                        HoaDon originalHoaDon = item as HoaDon;
                                        if (originalHoaDon != null)
                                        {
                                                // Create a new instance of HoaDon
                                                HoaDon newHoaDon = new HoaDon();
                                                newHoaDon.lblID.Content = originalHoaDon.lblID.Content;
                                                newHoaDon.lblMaSP.Content = originalHoaDon.lblMaSP.Content;
                                                newHoaDon.lblTenSP.Content = originalHoaDon. lblTenSP.Content;
                                                newHoaDon.tbxSoLuong.Text = originalHoaDon.tbxSoLuong.Text;

                                                listhoadon.Add(newHoaDon);
                                        }
                                }
                                HoaDonWindow hoadonW = new HoaDonWindow(MaCH,listhoadon);
                                hoadonW.Show();
                        }
                }

                private bool checkExist(String tenSP)
                {
                        bool rs = false;
                        foreach (var item in lstHoaDon.Items)
                        {
                                HoaDon hoaDon = item as HoaDon;
                                if (hoaDon.lblTenSP.Content.ToString() == tenSP)
                                        rs = true;

                        }
                        return rs;
                }

                private bool checkSoLuong()
                {
                        bool rs = true;
                        foreach (var item in lstHoaDon.Items)
                        {
                                HoaDon hoaDon = item as HoaDon;
                                string soLuong = hoaDon.tbxSoLuong.Text;
                                string maSP = hoaDon.lblMaSP.Content.ToString();
                                if(!CheckIfOnlyNumbers(soLuong) || !CheckIfNull(soLuong) ||  int.Parse(soLuong) > soLuongToiDa(maSP))
                                {
                                        return false;
                                }
                        }
                        return rs;
                }
        }
}
