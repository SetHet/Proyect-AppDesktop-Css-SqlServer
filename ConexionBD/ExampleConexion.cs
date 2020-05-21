using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public static class ExampleConexion
    {
        public static string connectionString = @"Server = DESKTOP-F8TM6K2\SQLEXPRESS; Database=Usuarios;Trusted_Connection=True;";

        #region Insert
        public static void InsetarUsuario(string nombre, string apellido = "")
        {
            //Crea la conneccion
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Prepara la Query antes de ejecutarla
                SqlCommand command = new SqlCommand(null, connection);

                //prepara la query
                command.CommandText = "INSERT INTO usuario (nombre, apellido) values (@nombre, @apellido)";
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);

                //Abrir coneccion y ejecutar la query
                try
                {
                    connection.Open();
                    Int32 rowAffected = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    //throw;
                }

                //Cierra la coneccion
                connection.Close();
            }
        }
        #endregion
        #region Delete
        public static void DeleteUsuario(string nombre, string apellido)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, conexion);
                command.CommandText = "DELETE FROM usuario WHERE nombre = @nombre and apellido = @apellido";
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@apellido", apellido);

                try
                {
                    conexion.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    //throw;
                }


                conexion.Close();
            }
        }
        public static void DeleteUsuarioByName(string nombre)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, conexion);
                command.CommandText = "DELETE FROM usuario WHERE nombre = @nombre";
                command.Parameters.AddWithValue("@nombre", nombre);

                try
                {
                    conexion.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    //throw;
                }


                conexion.Close();
            }
        }
        public static void DeleteUsuarioByLastName(string apellido)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, conexion);
                command.CommandText = "DELETE FROM usuario WHERE apellido = @apellido";
                command.Parameters.AddWithValue("@apellido", apellido);

                try
                {
                    conexion.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    //throw;
                }


                conexion.Close();
            }
        }
        public static void DeleteUsuario(int id)
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(null, conexion);
                command.CommandText = "DELETE FROM usuario WHERE id_usuario = @id";
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    conexion.Open();
                    Int32 rownum = command.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    //throw;
                }

                conexion.Close();
            }
        }
        #endregion
        #region Update
        public static void UpdateUsuario(int id, string nombre = "", string apellido = "")
        {
            SqlConnection conexion = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(null, conexion);
            command.CommandText = "UPDATE usuario SET nombre = @nombre, apellido = @apellido WHERE id_usuario = @id_usuario";
            command.Parameters.AddWithValue("@id_usuario", id);
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@apellido", apellido);

            try
            {
                conexion.Open();
                Int32 rownum = command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                //throw;
            }

            conexion.Close();

        }
        #endregion
        #region Select
        public static string SelectName(int id)
        {
            string salida = "...";

            SqlConnection conexion = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(null, conexion);
            command.CommandText = "SELECT nombre FROM usuario WHERE id_usuario = @id";
            command.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                salida = System.Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception)
            {
                salida = "";
                //throw;
            }

            return salida;
        }

        public static string SelectLastName(int id)
        {
            string salida = "...";

            SqlConnection conexion = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(null, conexion);
            command.CommandText = "SELECT apellido FROM usuario WHERE id_usuario = @id";
            command.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                salida = System.Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception)
            {
                salida = "";
                //throw;
            }

            return salida;
        }

        public static string SelectId(string nombre, string apellido)
        {
            string salida = "...";

            SqlConnection conexion = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(null, conexion);
            command.CommandText = "SELECT id FROM usuario WHERE nombre = @nombre AND apellido = @apellido";
            command.Parameters.AddWithValue("@nombre", nombre);
            command.Parameters.AddWithValue("@apellido", apellido);

            try
            {
                conexion.Open();
                salida = System.Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception)
            {
                salida = "";
                //throw;
            }

            return salida;
        }
        #endregion
    }
}
