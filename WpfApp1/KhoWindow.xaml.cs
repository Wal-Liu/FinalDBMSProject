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

namespace WpfApp1
{
        /// <summary>
        /// Interaction logic for KhoWindow.xaml
        /// </summary>
        public partial class KhoWindow : Window
        {
                string strCon = @"Data Source=WALL-LIU;Initial Catalog=QLSanPham;Integrated Security=True;Encrypt=false";
                SqlConnection sqlcon = null;
                public KhoWindow()
                {
                        InitializeComponent();
                        MoKetNoi();
                }

                private void btnBack_Click(object sender, RoutedEventArgs e)
                {
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                }

                private void btnNhap_Click(object sender, RoutedEventArgs e)
                {
                        NhapKho nhapKho = new NhapKho();
                        nhapKho.Show();
                }

                private void btnXuat_Click(object sender, RoutedEventArgs e)
                {
                        XuatKho xuatKho = new XuatKho();
                        xuatKho.Show();
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

                                                // Create a DataTable to hold the results
                                                DataTable dataTable = new DataTable();

                                                try
                                                {
                                                        // Use SqlDataAdapter to fill the DataTable
                                                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                                        {
                                                                adapter.Fill(dataTable);
                                                        }

                                                        // Bind the DataTable to a DataGridView (or any other control)
                                                        datagridview.DataContext = dataTable; // Assuming you have a DataGridView named dataGridView1
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
                                }
                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show(ex.Message);

                        }
                }

        }
}
