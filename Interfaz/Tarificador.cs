using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    class Tarificador
    {
        public float primaBase = 200000;
        
        public float ObtenerPrima(string plan)
        {
            float prima = 0;
            if (plan.Equals("VID01"))
            {
                prima =( primaBase * 50)/100;
            }
            else if (plan.Equals("VID02"))
            {
                prima = (primaBase * 350) / 100;
            }
            else if (plan.Equals("VID03"))
            {
                prima = (primaBase * 120) / 100;
            }
            else if (plan.Equals("VID04"))
            {
                prima = (primaBase * 200) / 100;
            }
            else if (plan.Equals("VID05"))
            {
                prima = (primaBase * 350) / 100;
            } 
            return prima;
        }
    }
}
