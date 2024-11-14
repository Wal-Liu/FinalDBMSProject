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
using WpfApp1.UC;
using System.Runtime.CompilerServices;
using WpfApp1.Object;

namespace WpfApp1.SanPham
{
        /// <summary>
        /// Interaction logic for ThemSuaSP.xaml
        /// </summary>


        public partial class ThemSuaSP : Window
        {
                private int? sanPhamId = null;
                public ThemSuaSP(int? sanPhamId = null)
                {
                        InitializeComponent();
                        this.sanPhamId = sanPhamId;
                        load();
                        if(sanPhamId != null)
                        {
                                LoadSuaSP(sanPhamId.ToString());
                        }
                }

                private void btnXacNhan_Click(object sender, RoutedEventArgs e)
                {
                        string tenSP = txbTenSP.Text;
                        string dongia = txbDonGia.Text;
                        string mota = txbMoTa.Text;
                        string maloaiSP = (cbbLoaiSP.SelectedItem as ComboBoxItem).Tag.ToString();

                        bool successful = false;
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                if (sanPhamId == null)
                                {
                                        using (SqlCommand command = new SqlCommand("proc_ThemSanPham", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@tenSP", tenSP);
                                                command.Parameters.AddWithValue("@donGia", dongia);
                                                command.Parameters.AddWithValue("@moTa", mota);
                                                command.Parameters.AddWithValue("@maLoaiSP", maloaiSP);
                                                int rowsAffected = command.ExecuteNonQuery();
                                                if (rowsAffected > 0) successful = true;
                                        }
                                }
                                else
                                {
                                        using (SqlCommand command = new SqlCommand("proc_SuaSanPham", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maSP", sanPhamId);
                                                command.Parameters.AddWithValue("@tenSP", tenSP);
                                                command.Parameters.AddWithValue("@donGia", dongia);
                                                command.Parameters.AddWithValue("@moTa", mota);
                                                command.Parameters.AddWithValue("@maLoaiSP", maloaiSP);
                                                int rowsAffected = command.ExecuteNonQuery();
                                                if (rowsAffected > 0) successful = true;
                                        }
                                }
                        }
                        if (successful == true)
                        {
                                MessageBox.Show("Thanh cong");
                                this.Close();
                        }
                        else
                                MessageBox.Show("vui lòng thử lại");

                }

                private void load()
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_LayHetLoaiSP", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        SqlDataReader reader = command.ExecuteReader();
                                        cbbLoaiSP.Items.Clear();
                                        while (reader.Read())
                                        {
                                                ComboBoxItem item = new ComboBoxItem();
                                                item.Content = reader["tenLoaiSP"].ToString();
                                                item.Tag = reader["maLoaiSP"].ToString();
                                                cbbLoaiSP.Items.Add(item);
                                        }
                                }
                        }
                }
                private void LoadSuaSP(string maSP)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_LaySanPham", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@maSP", maSP);
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                                
                                                txbTenSP.Text = reader["tenSP"].ToString();
                                                txbDonGia.Text = reader["donGia"].ToString();
                                                txbMoTa.Text = reader["moTa"].ToString();
                                                string LoaiSP = MaLoaiSPToLoaiSP(reader["maLoaiSP"].ToString());
                                                SelectComboBoxItemByTag(LoaiSP);
                                        }
                                }
                        }
                }
                private string MaLoaiSPToLoaiSP(string maLoaiSP)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT dbo.func_LayTenLoaiSP(" + maLoaiSP + ")", connection))
                                {
                                        var tenLoaiSP = command.ExecuteScalar().ToString();
                                        return tenLoaiSP;
                                }
                        }
                }
                private void SelectComboBoxItemByTag(string tagToSelect)
                {
                        foreach (ComboBoxItem item in cbbLoaiSP.Items)
                        {
                                if (item.Content.ToString() == tagToSelect)
                                {
                                        cbbLoaiSP.SelectedItem = item;
                                        string maloaiSP = (cbbLoaiSP.SelectedItem as ComboBoxItem).Tag.ToString();
                                        break; 
                                }
                        }
                }
        }

}
