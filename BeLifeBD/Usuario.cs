using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Usuario
    {
        public int id = 0;
        public string nombre;
        public string apellido;

        public void Find()
        {
            object[] objs = Conexion.SelectFirst("Usuario", where:"0=0");
            if (objs != null)
            {
                nombre = System.Convert.ToString(objs[1]);
                apellido = System.Convert.ToString(objs[2]);
            }
        }

        public static string GetName(int id)
        {
            string x = "";

            x = (string)Conexion.SelectValue("Usuario", "Nombre", $"id_usuario = {id}");

            return x;
        }

        public override string ToString()
        {
            return $"id: {id}, nombre: {nombre}, apellido: {apellido}";
        }
    }
}
