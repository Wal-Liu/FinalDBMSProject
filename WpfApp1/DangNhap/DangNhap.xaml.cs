﻿using System;
using System.Collections.Generic;
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
using WpfApp1.Admin;

namespace WpfApp1.DangNhap
{
        /// <summary>
        /// Interaction logic for DangNhap.xaml
        /// </summary>
        public partial class DangNhap : Window
        {
                public DangNhap()
                {
                        InitializeComponent();
                }
                private void WindowClosed(object sender, EventArgs e)
                => load();

                private void btnGui_Click(object sender, RoutedEventArgs e)
                {
                        try
                        {
                                string username = txbTenDangNhap.Text;
                                string password = txbMatKhau.Text;
                                DBConnection.login(username, password);
                                MessageBox.Show("Dang Nhap Thanh Cong");
                                admin admin = new admin();
                                this.Hide();
                                admin.Show();
                                admin.Closed += WindowClosed;

                        }
                        catch (Exception ex)
                        {
                                MessageBox.Show("Dang Nhap That Bai: " + ex.Message);
                        }
                }

                private void load()
                {
                        DBConnection.logout();
                        txbTenDangNhap.Text = string.Empty;
                        txbMatKhau.Text = string.Empty;
                        this.Show();
                }
        }
}
