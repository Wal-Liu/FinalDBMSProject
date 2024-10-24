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
using System.Drawing;
using Color = System.Windows.Media.Color;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for XuatKho.xaml
    /// </summary>
    public partial class XuatKho : Window
    {
        string strCon = Globals.strcon;
        SqlConnection sqlcon = null;
        private int MaKho;
        public XuatKho(int maKho)
        {
            InitializeComponent();
            MaKho = maKho;
            MoKetNoi();
        }
        private int SoLuong = 0;

        private void btnXuatHang_Click(object sender, RoutedEventArgs e)
        {
            bool isOnlyNumbers = CheckIfOnlyNumbers(txtSoLuong.Text);
            bool isEmpty = CheckIfNull(txtSoLuong.Text);
            if (!isEmpty || cbbSanPham.SelectedItem == null || CbbCuaHang.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                if (isOnlyNumbers)
                {
                    if (int.Parse(txtSoLuong.Text) > SoLuong)
                    {
                        MessageBox.Show("Số Lượng Xuất ra nhiều hơn số lượng đang có. Vui Lòng Nhập lại");
                        txtSoLuong.Text = string.Empty;
                    }
                    else
                    {
                        String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                        String maCH = (CbbCuaHang.SelectedItem as ComboBoxItem).Tag.ToString();
                        int soLuong = int.Parse(txtSoLuong.Text);

                        bool successful = false;
                        using (SqlConnection connection = new SqlConnection(strCon))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("proc_NhapSPVaoCH ", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@maSP", maSP);
                                command.Parameters.AddWithValue("@maCH", maCH);
                                command.Parameters.AddWithValue("@maKho", MaKho);
                                command.Parameters.AddWithValue("@soLuong", soLuong);

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0) successful = true;
                                // Display the number of affected rows (if applicable)
                                //MessageBox.Show($"{rowsAffected} rows affected.");
                            }
                        }
                        if (successful == true)
                            MessageBox.Show("Thanh cong");
                        else
                            MessageBox.Show("vui lòng thử lại");
                    }
                }
                else
                {
                    MessageBox.Show("Số Lượng chỉ được nhập số. Vui lòng nhập lại");
                    txtSoLuong.Text = string.Empty;
                }
            }

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
        private void loadCuaHang()
        {
            if (sqlcon != null && sqlcon.State == ConnectionState.Open)
            {
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("proc_LayHetCuaHang", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = reader["tenCH"].ToString();
                            comboBoxItem.Tag = reader["maCH"].ToString();

                            CbbCuaHang.Items.Add(comboBoxItem);
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
                    loadCuaHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

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
                    using (SqlCommand command = new SqlCommand("proc_LaySoLuongSanPhamTrongKho", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@maSP", maSP);
                        command.Parameters.AddWithValue("@maKho", MaKho);
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
    }
}
