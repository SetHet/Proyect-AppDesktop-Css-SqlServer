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
        public string rut { get { return rut; } set { rut = Tools.StringLength(value, rut_length); } }
        public string nombre { get { return nombre; } set { nombre = Tools.StringLength(value, nombre_length); } }
        public string apellido { get { return apellido; } set { apellido = Tools.StringLength(value, apellido_length); } }
        public string fechaNacimiento { get { return fechaNacimiento; } set { fechaNacimiento = value; } }
        public int idSexo { get { return idSexo; } set { idSexo = (Sexo.Find(value) != null) ? value : 0; } }
        public int idEstadoCivil { get { return idEstadoCivil; } set { idEstadoCivil = (EstadoCivil.Find(value) != null) ? value : 0; } }

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
        public static List<Cliente> FindAll()
        {
            List<Cliente> list = new List<Cliente>();
            object[][] matriz = Conexion.Select(table).ToArray();
            Cliente c;
            foreach (object[] row in matriz)
            {
                c = new Cliente();
                c.rut = (string)row[0];
                c.nombre = (string)row[1];
                c.apellido = (string)row[2];
                c.fechaNacimiento = (string)row[3];
                c.idSexo = (int)row[4];
                c.idEstadoCivil = (int)row[5];
                list.Add(c);
            }
            return list;
        }

        //Select
        public void Select(string rut = null)
        {
            if (rut == null) rut = this.rut;
            rut = Tools.StringLength(rut, rut_length);
            object[] row = Conexion.SelectFirst(table, where: $"rutCliente = '{rut}'");
            if (row != null)
            {
                this.rut = (string)row[0];
                this.nombre = (string)row[1];
                this.apellido = (string)row[2];
                this.fechaNacimiento = (string)row[3];
                this.idSexo = (int)row[4];
                this.idEstadoCivil = (int)row[5];
            }
            else
            {
                Console.WriteLine("No se encontro cliente, rut: " + rut);
            }
        }

        //Delete
        public bool Delete(string rut = null)
        {
            if (rut == null) rut = this.rut;
            rut = Tools.StringLength(rut, rut_length);
            int? num = Conexion.Delete(table, where: $"rutCliente = '{rut}'");
            if (num == null || num < 1)
            {
                return false;
            }
            else return true;
        }

        //Update
        public bool Update()
        {
            if (!Exist()) return false;
            string rut = Tools.StringLength(this.rut, rut_length);
            int? i = Conexion.Update(table, 
                $"nombres = '{this.nombre}', apellidos = '{this.apellido}', fechaNacimiento = '{this.fechaNacimiento}', idSexo = {this.idSexo}, idEstadoCivil = {this.idEstadoCivil}", 
                $"rutCliente = '{rut}'");
            if (i == null || i < 1) return false;
            else return true;
        }

        //Exist
        public bool Exist(string rut = null)
        {
            if (rut == null) rut = this.rut;
            rut = Tools.StringLength(rut, rut_length);
            return Conexion.SelectValue(table, "rutCliente", $"rutCliente = '{rut}'") != null;
        }



        

    }
}
