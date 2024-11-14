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

namespace WpfApp1.CuaHang
{
    /// <summary>
    /// Interaction logic for ChonCuaHang.xaml
    /// </summary>
    public partial class ChonCuaHang : Window
    {
        public ChonCuaHang()
        {
            InitializeComponent();
            loadCuaHang();
        }
        private void loadCuaHang()
        {
            using (SqlConnection connection = DBConnection.connect())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("proc_LayHetCuaHang", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ComboBoxItem comboBoxItem = new ComboBoxItem();
                            comboBoxItem.Content = reader["tenCH"].ToString();
                            comboBoxItem.Tag = reader["maCH"].ToString();

                            cbbCuaHang.Items.Add(comboBoxItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("That Bai: " + ex); 
                    }
                }
            }
        }



        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (cbbCuaHang.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                int maCH = int.Parse((cbbCuaHang.SelectedItem as ComboBoxItem).Tag.ToString());
                CuaHangWindow cuaHangWindow = new CuaHangWindow(maCH);
                cuaHangWindow.Show();
                this.Close();
            }
        }
    }
}
