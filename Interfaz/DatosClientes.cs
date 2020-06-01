using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaz
{
    class DatosClientes
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Genero { get; set; }
        public string Estado { get; set; }

        public string Generos(int id)
        {
            string[] generos = new string[2] { "Hombre", "Mujer" };
            return generos[id - 1];
        }

        public string EstadoCivil(int id)
        {
            string[] estados = new string[4] { "Soltero", "Casado", "Divorciado", "Viudo" };
            return estados[id - 1];
        }
    }
}
