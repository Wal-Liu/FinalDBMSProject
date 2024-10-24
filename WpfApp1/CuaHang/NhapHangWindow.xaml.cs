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

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for NhapHangWindow.xaml
        /// </summary>
        public partial class NhapHangWindow : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=QLSanPham;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;
                private int MaCH;
                public NhapHangWindow(int maCH)
                {
                        MaCH = maCH;
                        InitializeComponent();
                        MoKetNoi();
                }
                private void loadSanPham(int MaKho)
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

                private void loadKhoHang()
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("proc_LayHetKho", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                SqlDataReader reader = command.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                                                        comboBoxItem.Content = reader["tenKho"].ToString();
                                                        comboBoxItem.Tag = reader["maKho"].ToString();

                                                        cbbKhoHang.Items.Add(comboBoxItem);
                                                }
                                        }
                                }
                        }
                }
                private int SoLuong = 0;
                private void btnXacNhan_Click(object sender, RoutedEventArgs e)
                {
                        bool isOnlyNumbers = CheckIfOnlyNumbers(tbxSoLuong.Text);
                        bool isEmpty = CheckIfNull(tbxSoLuong.Text);
                        if (!isEmpty || cbbSanPham.SelectedItem == null || cbbKhoHang.SelectedItem == null)
                        {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        }
                        else
                        {
                                if (isOnlyNumbers)
                                {
                                        if (int.Parse(tbxSoLuong.Text) > SoLuong)
                                        {
                                                MessageBox.Show("Số Lượng Xuất ra nhiều hơn số lượng đang có. Vui Lòng Nhập lại");
                                                tbxSoLuong.Text = string.Empty;
                                        }
                                        else
                                        {
                                                String tenSP = (cbbSanPham.SelectedItem as ComboBoxItem).Content.ToString();
                                                String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                                                String makho = (cbbKhoHang.SelectedItem as ComboBoxItem).Tag.ToString();
                                                int soLuong = int.Parse(tbxSoLuong.Text);

                                                bool successful = false;
                                                using (SqlConnection connection = new SqlConnection(strCon))
                                                {
                                                        connection.Open();
                                                        using (SqlCommand command = new SqlCommand("proc_NhapSPVaoCH ", connection))
                                                        {
                                                                command.CommandType = CommandType.StoredProcedure;
                                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                                command.Parameters.AddWithValue("@maCH", MaCH);
                                                                command.Parameters.AddWithValue("@maKho", makho);
                                                                command.Parameters.AddWithValue("@soLuong", soLuong);

                                                                int rowsAffected = command.ExecuteNonQuery();
                                                                if (rowsAffected > 0) successful = true;
                                                                // Display the number of affected rows (if applicable)
                                                                //MessageBox.Show($"{rowsAffected} rows affected.");
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
                                }
                                else
                                {
                                        MessageBox.Show("Số Lượng chỉ được nhập số. Vui lòng nhập lại");
                                        tbxSoLuong.Text = string.Empty;
                                }
                        }

                }

                private void cbbSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        String maSP = (cbbSanPham.SelectedItem as ComboBoxItem).Tag.ToString();
                        string MaKho = (cbbKhoHang.SelectedItem as ComboBoxItem).Tag.ToString();
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

                private void CbbKhoHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        int maKho = int.Parse((cbbKhoHang.SelectedItem as ComboBoxItem).Tag.ToString());
                        loadSanPham(maKho);
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
                                        loadKhoHang();
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
        }
}