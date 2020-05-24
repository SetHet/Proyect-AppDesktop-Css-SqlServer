using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class EstadoCivil
    {
        public const string table = "EstadoCivil";
        public int id;
        public string descripcion;

        public static EstadoCivil Find(int id)
        {
            object[] obj = Conexion.SelectFirst(table, where: $"idEstadoCivil = {id}");
            if (obj != null)
            {
                EstadoCivil ec = new EstadoCivil();
                ec.id = (int)obj[0];
                ec.descripcion = (string)obj[1];
                return ec;
            }
            else return null;
        }
    }
}
