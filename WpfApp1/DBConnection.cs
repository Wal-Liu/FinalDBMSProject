using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class DBConnection
    {
        private static string username = null;
        private static string password = null;
        private static string strCon = null;
        private static SqlConnection sqlcon = null;

        public static void login(string username, string password)
        {
            DBConnection.username = username;
            DBConnection.password = password;
            strCon = @"Data Source=" + Globals.dataSource + "; Initial Catalog=QLSanPham; User ID=" + username + "; Password=" + password + "; Encrypt=True; TrustServerCertificate=True;";
            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strCon);
            }
            sqlcon = new SqlConnection(strCon);
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            sqlcon.Close();
        }
        public static void logout()
        {
            username = null;
            password = null;
            strCon = null;
            sqlcon = null; 
        }
        public static SqlConnection connect()
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
                }
                sqlcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sqlcon;  
        }
    }
}
