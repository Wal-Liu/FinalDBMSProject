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
        public ChonKhoWindow()
        {
            InitializeComponent();
            loadKho(); 
        }

        private void loadKho()
        {
                using (SqlConnection connection = DBConnection.connect())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("proc_LayHetKho", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        cbbKho.Items.Clear();
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


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (cbbKho.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng Chọn Kho");
            }
            else
            {
                int maKho = int.Parse((cbbKho.SelectedItem as ComboBoxItem).Tag.ToString());
                KhoWindow khoWindow = new KhoWindow(maKho);
                khoWindow.Show();
                this.Close();
            }
        }
    }
}
