using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;


namespace WpfApp1
{
        public partial class MainWindow : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=DBMS;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;


                public MainWindow()
                {
                        InitializeComponent();
                        loadSanPham();
                }

                private void btnMo_Click(object sender, RoutedEventArgs e)
                {
                        try
                        {
                                if(sqlcon == null)
                                {
                                        sqlcon = new SqlConnection(strCon);
                                }
                                sqlcon = new SqlConnection(strCon);
                                if(sqlcon.State == ConnectionState.Closed ) 
                                {
                                        sqlcon.Open();
                                        MessageBox.Show("Ket noi thanh cong");
                                        loadSanPham();
                                }
                        }
                        catch(Exception ex)
                        {
                                MessageBox.Show(ex.Message);

                        }
                }

                private void btnDong_Click(object sender, RoutedEventArgs e)
                {
                        if(sqlcon != null && sqlcon.State== ConnectionState.Open) 
                        {
                                sqlcon.Close();
                                MessageBox.Show("Dong ket noi");
                        }
                        else
                        {
                                MessageBox.Show("Chua tao ket noi");
                        }
                }

                private void loadSanPham()
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("getSP", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                                SqlDataReader reader = command.ExecuteReader();
                                               // lstSP.Items.Clear();
                                                while (reader.Read())
                                                {
                                                        //lstSP.Items.Add(reader["tenSanPham"].ToString());
                                                }
                                        }
                                }
                        }
                }

                private void btnThem_Click(object sender, RoutedEventArgs e)
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("them_san_pham", connection))
                                        {
                                                command.CommandType = CommandType.StoredProcedure;
                                               // command.Parameters.AddWithValue("@ten", txtName.Text);
                                                command.ExecuteNonQuery();
                                                MessageBox.Show("Thêm sản phẩm thành công");
                                                loadSanPham();
                                        }
                                }
                        } 
                }

                private void btnTimKiem_Click(object sender, RoutedEventArgs e)
                {
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                        {
                                using (SqlConnection connection = new SqlConnection(strCon))
                                {
                                        connection.Open();
                                        using (SqlCommand command = new SqlCommand("SELECT dbo.find_id_by_name(@ten)", connection))
                                        {
                                               // command.Parameters.AddWithValue("@ten", txtName.Text);
                                                command.ExecuteNonQuery();
                                                object result = command.ExecuteScalar();
                                                string output = result.ToString();
                                                //MessageBox.Show(output);
                                                if (output != "")
                                                {
                                                        MessageBox.Show("id: " + output);
                                                }
                                                else
                                                {
                                                        MessageBox.Show("Không tìm thấy sản phẩm");
                                                }
                                        }
                                }
                        }
                }

                private void btnKho_Click(object sender, RoutedEventArgs e)
                {
                        KhoWindow khoWindow = new KhoWindow();
                        khoWindow.Show();
                        this.Close();
                }

                private void btnCuaHang_Click(object sender, RoutedEventArgs e)
                {
                        CuaHangWindow cuaHangWindow = new CuaHangWindow();
                        cuaHangWindow.Show();
                        this.Close();
                }
        }
}