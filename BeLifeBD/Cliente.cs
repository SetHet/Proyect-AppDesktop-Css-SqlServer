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
        #region Parametros
        //Settings
        public const string table = "Cliente";
        public const int rut_length = 10;
        public const int nombre_length = 20;
        public const int apellido_length = 20;

        //Variables
        string _rut;
        string _nombre;
        string _apellido;
        string _fechaNacimiento;
        int _idSexo;
        int _idEstadoCivil;

        public string rut { get { return _rut; } set { _rut = Tools.StringLength(value, rut_length); } }
        public string nombre { get { return _nombre; } set { _nombre = Tools.StringLength(value, nombre_length); } }
        public string apellido { get { return _apellido; } set { _apellido = Tools.StringLength(value, apellido_length); } }
        public string fechaNacimiento { get { return _fechaNacimiento; } set { _fechaNacimiento = value; } }
        public DateTime fechaNacimiento_DateTime
        {
            get { return DateTime.Parse(fechaNacimiento); }
            set { fechaNacimiento = value.ToString(); }
        }
        public int idSexo { get { return _idSexo; } set { _idSexo = (Sexo.Find(value) != null) ? value : 0; } }
        public int idEstadoCivil { get { return _idEstadoCivil; } set { _idEstadoCivil = (EstadoCivil.Find(value) != null) ? value : 0; } }

        #endregion

        #region Constructores

        //Constructor
        public Cliente()
        {
            rut = "";
            nombre = "";
            apellido = "";
            fechaNacimiento = "";
            idSexo = 0;
            idEstadoCivil = 0;
        }

        #endregion

        #region Querys y no Querys
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
                c.fechaNacimiento_DateTime = (DateTime)row[3];
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
                c.fechaNacimiento_DateTime = (DateTime)row[3];
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
                this.fechaNacimiento_DateTime = (DateTime)row[3];
                this.idSexo = (int)row[4];
                this.idEstadoCivil = (int)row[5];
            }
            else
            {
                Console.WriteLine("No se encontro cliente, rut: " + rut);
            }
        }

        //Insert
        public bool Insert()
        {
            if (Exist()) return false;

            bool correct = Conexion.Insert(table, $"'{rut}', '{nombre}', '{apellido}', CONVERT(DATETIME,'{fechaNacimiento}', 105), {idSexo}, {idEstadoCivil}");
            return correct;
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
                $"nombres = '{this.nombre}', apellidos = '{this.apellido}', fechaNacimiento = CONVERT(DATETIME,'{this.fechaNacimiento}', 105), idSexo = {this.idSexo}, idEstadoCivil = {this.idEstadoCivil}", 
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

        #endregion

        //Other

        public override string ToString()
        {
            return $"Cliente > rut: {rut}, nombres: {nombre}, apellidos: {apellido}, fechaNacimiento: {fechaNacimiento}, idSexo: {idSexo}, idEstadoCivil: {idEstadoCivil}";
        }



    }
}
