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

        public static int? Delete(string table, string where = "0 = 0")
        {
            int? numDelete = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"DELETE FROM {table} WHERE {where}", connection);

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

        public static int? Update(string table, string set, string where = "0=0")
        {
            int? numUpdate = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"UPDATE {table} SET {set} WHERE {where}", connection);

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
                SqlCommand command = new SqlCommand($"INSERT INTO {table} ({param}) VALUES ({values})", connection);

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
                SqlCommand command = new SqlCommand($"INSERT INTO {table} VALUES ({values})", connection);

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

        public static object SelectValue(string table, string column, string where = "0 = 0")
        {
            object select = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT TOP(1) {column} FROM {table} WHERE {where}", connection);

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

        public static object[] SelectFirst(string table, string column = "*", string where = "0 = 0")
        {
            object[] select = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT TOP(1) {column} FROM {table} WHERE {where}", connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            select = new object[reader.FieldCount];
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

        public static List<object[]> Select(string table, string column = "*", string where = "0 = 0")
        {
            List<object[]> select = new List<object[]>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT {column} FROM {table} WHERE {where}", connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        object[] row = null;
                        while (reader.Read())
                        {
                            row = new object[reader.FieldCount];
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
