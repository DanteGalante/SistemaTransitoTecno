using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD.ConexionBD
{
    public static class ConexionBD
    {
        private static string SERVER = "localhost";
        private static string PORT = "1433";
        private static string DATABASE = "BDTransito";
        private static string USER = "ProyectoTransito";
        private static string PASSWORD = "123456x.";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = null;

            try
            {
                string url = String.Format
                (
                    "Data Source = {0},{1};" +
                    "Network Library = DBMSSOCN;" +
                    "Initial Catalog = {2};" +
                    "User ID = {3};" +
                    "Password = {4};",
                    SERVER, PORT, DATABASE, USER, PASSWORD
                );

                conn = new SqlConnection(url);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return conn;
        }
    }
}
