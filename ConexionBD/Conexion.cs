using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public static class Conexion
    {
        public static string connectionString = @"Server = DESKTOP-F8TM6K2\SQLEXPRESS; Database=Usuarios;Trusted_Connection=True;";

        public static void SetConnection(string server, string database, bool trusted_connection = true)
        {
            string newConnString = "Server = " + server + "; Database=" + database + ";Trusted_Connection=" + (trusted_connection?"True":"False") + ";";
            connectionString = newConnString;
        }
    }
}
