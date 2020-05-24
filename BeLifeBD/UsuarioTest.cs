using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class UsuarioTest
    {
        public int id = 0;
        public string nombre;
        public string apellido;
        public const string table = "Usuario";

        public bool Find()
        {
            object[] objs = Conexion.SelectFirst("Usuario", where:$"id_usuario = {id}");
            if (objs != null)
            {
                id = System.Convert.ToInt32(objs[0]);
                nombre = System.Convert.ToString(objs[1]);
                apellido = System.Convert.ToString(objs[2]);
                return true;
            }
            return false;
        }

        public int Delete()
        {
            int salida = 0;
            int? i = Conexion.Delete(table, $"id_usuario = {id}");
            if (i == null)
            {
                Console.Write("\nError de Delete\n");
            }
            else salida = (int)i;
            return salida;
        }

        public bool Insert()
        {
            bool x = Conexion.Insert(table, $"'{nombre}', '{apellido}'");
            try
            {
                id = (int)Conexion.SelectValue(table, "id_usuario", $"nombre = '{nombre}' and apellido = '{apellido}'");
            }
            catch (Exception)
            {

            }
            return x;
        }

        public int Update()
        {
            int salida = 0;
            int? i =Conexion.Update(table, $"nombre = '{nombre}', apellido = '{apellido}'", $"id_usuario = {id}");
            if (i == null) Console.WriteLine("\nError de Update\n");
            else salida = (int)i;
            return salida;
        }

        public static string GetName(int id)
        {
            string x = "";

            x = (string)Conexion.SelectValue("Usuario", "Nombre", $"id_usuario = {id}");

            return x;
        }

        public static List<UsuarioTest> GetTable()
        {
            List<UsuarioTest> usuarios = new List<UsuarioTest>();
            UsuarioTest u;
            foreach(object[] colum in Conexion.Select("Usuario"))
            {
                u = new UsuarioTest();
                u.id = (int)colum[0];
                u.nombre = (string)colum[1];
                u.apellido = (string)colum[2];
                usuarios.Add(u);
            }

            return usuarios;
        }

        public override string ToString()
        {
            return $"id: {id}, nombre: {nombre}, apellido: {apellido}";
        }
    }
}
