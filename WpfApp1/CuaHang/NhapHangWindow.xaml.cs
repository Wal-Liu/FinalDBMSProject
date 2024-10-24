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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for NhapHangWindow.xaml
    /// </summary>
    public partial class NhapHangWindow : Window
    {
        string strCon = Globals.strcon;
        SqlConnection sqlcon = null;
        private int MaCH;
        public NhapHangWindow(int maCH)
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
                        MessageBox.Show("@maCuaHang"); 
                        while (reader.Read())
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = reader["tenSP"].ToString();
                            comboBoxItem.Tag = reader["maSP"].ToString();
                            cbbSanPham.Items.Add(comboBoxItem);
                            MessageBox.Show(comboBoxItem.Tag.ToString());

                        }
                    }
                }
            }
        }

        private void btnXacNhan(object sender, RoutedEventArgs e)
        {
            string tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
            String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
            int soLuong = int.Parse(tbxSoLuong.Text);
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    using (SqlCommand command = new SqlCommand("proc_NhapSPVaoCH ", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@maSP", maSP);
                        command.Parameters.AddWithValue("@maCH", MaCH);
                        command.Parameters.AddWithValue("@soLuong", soLuong);
                        SqlDataReader reader = command.ExecuteReader();

                    }
                }
            }
            MessageBox.Show("OK");

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


        private void cbbSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
