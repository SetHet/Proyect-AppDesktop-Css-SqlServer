using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Plan
    {
        public const string table = "[plan]";
        
        public string id;
        public string nombre;
        public float primaBase;
        public string polizaActual;

        public const int id_lenght = 5;
        public const int nombre_length = 20;
        public const int poliza_length = 15;

        public static Plan Find(string id)
        {
            id = Tools.StringLength(id, id_lenght);
            object[] obj = Conexion.SelectFirst(table, where: $"IdPlan = '{id}'");
            if (obj != null)
            {
                Plan p = new Plan();
                p.id = (string)obj[0];
                p.nombre = (string)obj[1];
                p.primaBase = (float)System.Convert.ToDouble(obj[2]);
                p.polizaActual = (string)obj[3];
                return p;
            }
            else
            {
                return null;
            }
        }

    }
}
