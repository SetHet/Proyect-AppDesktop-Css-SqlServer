using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Sexo
    {
        public int id;
        public string descripcion;
        public const string table = "Sexo";

        public static Sexo Search(int id)
        {
            object[] obj = Conexion.SelectFirst(table, where: $"idsexo = {id}");
            if (obj != null)
            {
                Sexo s = new Sexo();
                s.id = (int)obj[0];
                s.descripcion = (string)obj[1];
                return s;
            }
            else
            {
                return null;
            }
        }
    }
}
