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

namespace WpfApp1.SanPham
{
        /// <summary>
        /// Interaction logic for QuanLyLoaiSanPham.xaml
        /// </summary>
        public partial class QuanLyLoaiSanPham : Window
        {
                private string MaLoaiSPDuocChon = "";
                public QuanLyLoaiSanPham()
                {
                        InitializeComponent();
                        lstLoaiSP.SelectedIndex = -1;
                        load();
                        lstLoaiSP.SelectionChanged += LstLoaiSP_SelectionChanged;
                        
                }

                private void LstLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                        // Kiểm tra nếu có item nào được
                        if (lstLoaiSP.SelectedIndex >= 0)
                        {
                                String maLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Tag.ToString();
                                String tenLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Content.ToString();
                                //MessageBox.Show(maLoaiSP);
                                txbTenLoaiSP.Text = tenLoaiSP;
                                MaLoaiSPDuocChon = maLoaiSP;
                        }
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
                                        lstLoaiSP.Items.Clear();
                                        while (reader.Read())
                                        {
                                                ListViewItem item  = new ListViewItem();
                                                item.Content = reader["tenLoaiSP"].ToString();
                                                item.Tag = reader["maLoaiSP"].ToString();
                                                lstLoaiSP.Items.Add(item);
                                        }
                                }
                        }
                }

                private void ListView_SelectionChanged()
                {
                        // Kiểm tra nếu có item nào được
                        if (lstLoaiSP.SelectedIndex >= 0)
                        {
                                String maLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Tag.ToString();
                                String tenLoaiSP = (lstLoaiSP.SelectedItem as ListViewItem).Content.ToString();
                                //MessageBox.Show(maLoaiSP);
                                txbTenLoaiSP.Text = tenLoaiSP;
                                MaLoaiSPDuocChon = maLoaiSP;
                        }
                }
                private void btnThem_Click(object sender, RoutedEventArgs e)
                {
                        string tenLoaiSP = txbTenLoaiSP.Text;
                        if (checkExit(tenLoaiSP) == true)
                        {
                                MessageBox.Show("Loại sản phẩm đã tồn tại!");
                                return;
                        }
                        if (tenLoaiSP.Length == 0)
                        {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                                return;
                        }
                        using (SqlConnection connection = DBConnection.connect())
                        {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand("proc_ThemLoaiSP", connection))
                                {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@tenLoaiSP", tenLoaiSP);
                                        SqlDataReader reader = command.ExecuteReader();
                                }
                        }
                        load();
                }
                private bool checkExit(String tenLoaiSP)
                {
                        foreach(ListViewItem item in lstLoaiSP.Items)
                        {
                                if (item.Content.ToString() == tenLoaiSP) return true;
                        }
                        return false;
                }

                private void btnSua_Click(object sender, RoutedEventArgs e)
                {
                        string tenLoaiSP = txbTenLoaiSP.Text;
                        MessageBox.Show(tenLoaiSP.Length.ToString());
                        if (checkExit(tenLoaiSP) == true)
                        {
                                MessageBox.Show("Loại sản phẩm đã tồn tại!");
                                return;
                        }
                        if (lstLoaiSP.SelectedIndex < 0)
                        {
                                MessageBox.Show("vui lòng chọn 1 loại Sản Phẩm");
                                return;
                        }
                        if(tenLoaiSP.Length == 0)
                        {
                                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                                return;
                        }

                        try
                        {
                                using (SqlConnection connection = DBConnection.connect())
                                {
                                        connection.Open();

                                        using (SqlCommand command = new SqlCommand("proc_SuaLoaiSanPham", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.AddWithValue("@maLoaiSP", MaLoaiSPDuocChon);
                                                command.Parameters.AddWithValue("@tenLoaiSP", tenLoaiSP);

                                                // Sử dụng ExecuteNonQuery() nếu thủ tục lưu trữ không trả về dữ liệu
                                                int rowsAffected = command.ExecuteNonQuery();

                                                // Kiểm tra xem có bao nhiêu dòng bị ảnh hưởng
                                                if (rowsAffected > 0)
                                                {
                                                        MessageBox.Show("Cập nhật loại sản phẩm thành công!");
                                                }
                                                else
                                                {
                                                        MessageBox.Show("Cập nhật không thành công. Vui lòng kiểm tra lại.");
                                                }
                                        }
                                }

                                // Tải lại dữ liệu sau khi thực hiện cập nhật
                                load();
                        }
                        catch (Exception ex)
                        {
                                // Xử lý ngoại lệ và hiển thị thông báo lỗi
                                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                        }
                }
        }
}
