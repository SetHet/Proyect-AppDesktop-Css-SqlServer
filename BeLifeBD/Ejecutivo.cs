using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Ejecutivo
    {
        //Variables
        public int id;
        public string usuario;
        public string codigo;

        //Constantes
        public const string table = "Ejecutivo";
        public const int usuario_lenght = 30;
        public const int codigo_lenght = 30;

        //funciones
        public bool ExistInBD()
        {
            object obj = Conexion.SelectValue(table, 
                "id_ejecutivo", 
                $"usuario = '{Tools.StringLength(usuario, usuario_lenght)}' and codigo = '{Tools.StringLength(codigo, codigo_lenght)}'");
            if (obj != null)
            {
                id = (int)obj;
                return true;
            }
            else return false;
        }
    }
}
