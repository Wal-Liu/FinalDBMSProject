﻿using System.Text;
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
                public MainWindow()
                {
                        InitializeComponent();
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