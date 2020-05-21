using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConexionBD
{
    public static class Conexion
    {
        #region Datos conexion

        public static string connectionString = @"Server = DESKTOP-F8TM6K2\SQLEXPRESS; Database=Usuarios;Trusted_Connection=True;";

        public static void SetConnection(string server, string database, bool trusted_connection = true)
        {
            string newConnString = "Server = " + server + "; Database=" + database + "; Trusted_Connection=" + (trusted_connection?"True":"False") + ";";
            connectionString = newConnString;
        }

        public static bool isConnectionPosible()
        {
            bool open = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    connection.Open();
                    open = connection.State == System.Data.ConnectionState.Open;
                    connection.Close();
            }
            return open;
        }

        #endregion

        #region NoQuery

        public static int? Delete(string table, string where)
        {
            int? numDelete = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "DELETE FROM @table WHERE @where";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                    numDelete = rownum;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de eliminacion." +
                        "\nTable: " + table + 
                        "\nWhere: " + where + 
                        "\nException Message: \n" + ex.Message);
                }
                connection.Close();
            }
            return numDelete;
        }

        public static int? Update(string table, string set, string where = "True")
        {
            int? numUpdate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "UPDATE @table SET @set WHERE @where";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@set", set);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                    numUpdate = rownum;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de actualizacion." +
                        "\nTable: " + table +
                        "\nSet: " + set + 
                        "\nWhere: " + where +
                        "\nException Message: \n" + ex.Message);
                }
                connection.Close();
            }
            return numUpdate;
        }

        public static bool Insert(string table, string param, string values)
        {
            bool insert = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "INSERT INTO @table (@param)";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                    numDelete = rownum;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de insercion." +
                        "\nTable: " + table +
                        "\nParam: " + param +
                        "\nValues: " + values +
                        "\nException Message: \n" + ex.Message);
                }
                connection.Close();
            }
            return insert;
        }

        #endregion
    }
}
