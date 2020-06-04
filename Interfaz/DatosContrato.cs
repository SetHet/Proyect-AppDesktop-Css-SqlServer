using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    class DatosContrato
    {
        public string Numero { get; set; }
        public string RutTitular { get; set; }
        public string Vigencia { get; set; }
        public string FechaCreacion { get; set; }
        public string PlanAsociado { get; set; }
        public string Poliza { get; set; }
        string[,] planes = new string[5, 2] { { "VID01", "POL120113229" }, { "VID02", "POL120648575" }, { "VID03", "POL125235079" }, {"VID04", "POL120100054" }, { "VID05", "POL120500489" } };

        public int ObtenerPlan(string Plan)
        {
            int indice = 0;
            for (int i = 0; i < 5; i++)
            {
                if (planes[i, 0].Equals(Plan))
                {
                    indice = i;
                    break;
                };
                indice = i;
            }
            return indice;
        }

        public string ObtenerPoliza(string Plan)
        {
            string poliza = "";
            for (int i = 0; i < 5; i++)
            {
                if (planes[i, 0].Equals(Plan))
                {
                    poliza = planes[i, 1];
                    break;
                }
            }
            return poliza;
        }
    }
}
