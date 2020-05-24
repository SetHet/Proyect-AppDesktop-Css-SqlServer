using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Cliente
    {
        //Settings
        public const string table = "Cliente";
        public const int rut_length = 10;
        public const int nombre_length = 20;
        public const int apellido_length = 20;

        //Variables
        public string rut;
        public string nombre;
        public string apellido;
        public string fechaNacimiento;
        public int idSexo;
        public int idEstadoCivil;

        //FUNCIONES

        //Find static
        public static Cliente Find(string rut)
        {
            rut = Tools.StringLength(rut, rut_length);
            object[] row = Conexion.SelectFirst(table, where: $"RutCliente = '{rut}'");
            if (row != null)
            {
                Cliente c = new Cliente();
                c.rut = (string)row[0];
                c.nombre = (string)row[1];
                c.apellido = (string)row[2];
                c.fechaNacimiento = (string)row[3];
                c.idSexo = (int)row[4];
                c.idEstadoCivil = (int)row[5];
                return c;
            }
            else return null;
        }
        //Static Find all

        //Select (id, rut)

        //Delete

        //Update




        

    }
}
