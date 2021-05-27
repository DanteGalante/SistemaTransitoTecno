using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD.ConexionBD;

namespace BaseDeDatos
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnection conn = null;
                conn = ConexionBD.GetConnection();
                if (conn != null)
                {
                    Console.WriteLine("Se pudo conectar asies");
                    Console.WriteLine(conn.ConnectionString);
                    Console.ReadLine();
                }

            }
            catch (SqlException exception)
            {

                Console.WriteLine(exception.Message);
            }
        }
    }
}
