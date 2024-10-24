using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfApp1.Kho
{
        /// <summary>
        /// Interaction logic for ChonKhoWindow.xaml
        /// </summary>
        public partial class ChonKhoWindow : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=QLSanPham;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;
                public ChonKhoWindow()
                {
                        InitializeComponent();
                        MoKetNoi();
                }
                private void loadKho()
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

                                                        cbbKho.Items.Add(comboBoxItem);
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
                                        loadKho();
                                }
                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show(ex.Message);

                        }
                }

                private void btnNext_Click(object sender, RoutedEventArgs e)
                {
                        if (cbbKho.SelectedItem == null)
                        {
                                MessageBox.Show("Vui lòng Chọn Kho");
                        }
                        else
                        {
                                int  maKho = int.Parse((cbbKho.SelectedItem as ComboBoxItem).Tag.ToString());
                                KhoWindow khoWindow = new KhoWindow(maKho);
                                khoWindow.Show();
                                this.Close();
                        }
                }
        }
}
