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
                try
                {
                    connection.Open();
                    open = connection.State == System.Data.ConnectionState.Open;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("La conexion a la BD a fallado \nException Message: " + ex.Message);
                }
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
                command.CommandText = "INSERT INTO @table (@param) VALUES (@values)";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@param", param);
                command.Parameters.AddWithValue("@values", values);

                try
                {
                    connection.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                    insert = rownum > 0;
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

        public static bool Insert(string table, string values)
        {
            bool insert = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "INSERT INTO @table VALUES (@values)";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@values", values);

                try
                {
                    connection.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                    insert = rownum > 0;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de insercion." +
                        "\nTable: " + table +
                        "\nSin Parametros" +
                        "\nValues: " + values +
                        "\nException Message: \n" + ex.Message);
                }
                connection.Close();
            }
            return insert;
        }

        #endregion

        #region Query

        public static object SelectValue(string table, string column, string where = "True")
        {
            object select = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT @column FROM @table WHERE @where ROWNUM <= 1";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@column", column);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    select = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de seleccion de valor." +
                        "\nTable: " + table +
                        "\nColumn: " + column +
                        "\nWhere: " + where +
                        "\nException Message: \n" + ex.Message);
                    select = null;
                }
                connection.Close();
            }
            return select;
        }

        public static object[] SelectFirst(string table, string column = "*", string where = "True")
        {
            object[] select = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT @column FROM @table WHERE @where ROWNUM <= 1";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@column", column);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reader.GetValues(select);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de seleccion de fila." +
                        "\nTable: " + table +
                        "\nColumn: " + column +
                        "\nWhere: " + where +
                        "\nException Message: \n" + ex.Message);
                    select = null;
                }
                connection.Close();
            }
            return select;
        }

        public static List<object[]> Select(string table, string column = "*", string where = "True")
        {
            List<object[]> select = new List<object[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "SELECT @column FROM @table WHERE @where ROWNUM <= 1";
                command.Parameters.AddWithValue("@table", table);
                command.Parameters.AddWithValue("@column", column);
                command.Parameters.AddWithValue("@where", where);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        object[] row = null;
                        while (reader.Read())
                        {
                            reader.GetValues(row);
                            select.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("->Error en la peticion de seleccion de varias filas." +
                        "\nTable: " + table +
                        "\nColumn: " + column +
                        "\nWhere: " + where +
                        "\nException Message: \n" + ex.Message);
                    select = new List<object[]>();
                }
                connection.Close();
            }
            return select;
        }

        #endregion
    }
}
