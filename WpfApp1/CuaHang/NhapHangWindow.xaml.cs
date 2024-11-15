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
                private int MaCH;
                public NhapHangWindow(int maCH)
                {
                        MaCH = maCH;
                        InitializeComponent();
                        loadKhoHang();
                }
                private void loadSanPham(int MaKho)
                {
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_LayHetSanPhamTrongKho", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@maKho", MaKho);
                                        SqlDataReader reader = command.ExecuteReader();
                                        cbbSanPham.Items.Clear();
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

                private void loadKhoHang()
                {
                        using (SqlConnection connection = DBConnection.connect())
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
                                                using (SqlConnection connection = DBConnection.connect())
                                                {
                                                        connection.Open();
                                                        using (SqlCommand command = new SqlCommand("proc_NhapSPVaoCH ", connection))
                                                        {
                                                                command.CommandType = CommandType.StoredProcedure;
                                                                command.Parameters.AddWithValue("@maSP", maSP);
                                                                command.Parameters.AddWithValue("@maCH", MaCH);
                                                                command.Parameters.AddWithValue("@maKho", makho);
                                                                command.Parameters.AddWithValue("@soLuong", soLuong);

                                                                try
                                                                {
                                                                        int rowsAffected = command.ExecuteNonQuery();
                                                                        if (rowsAffected > 0) successful = true;

                                                                        
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                        // Xử lý ngoại lệ và hiển thị thông báo lỗi
                                                                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
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
                        string maKho = (cbbKhoHang.SelectedItem as ComboBoxItem).Tag.ToString();
                        int SoLuongToiDa = 0;
                        string placeholderText = "Max: ";

                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("SELECT dbo.func_LaySoLuongSanPhamTrongKho(" + maSP + ", " + maKho + ")", connection))
                                {
                                        SoLuongToiDa = int.Parse(command.ExecuteScalar().ToString());
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