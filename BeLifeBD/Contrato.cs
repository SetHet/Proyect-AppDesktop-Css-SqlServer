using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionBD;

namespace BeLifeBD
{
    public class Contrato
    {
        #region Variables

        //Constants
        public const string table = "Contrato";

        public const int numero_length = 14;
        public const int rut_length = 10;
        public const int codigo_length = 5;

        //Variables
        string _numero = "";
        public DateTime fechaCreacion = new DateTime();
        public DateTime fechaTermino = new DateTime();
        string _rutCliente = "";
        string _codigoPlan = "";
        public DateTime fechaInicioVigencia = new DateTime();
        public DateTime fechaFinVigencia = new DateTime();
        public bool vigente = false;
        public bool declaracionSalud = false;
        public float primaAnual = 0f;
        public float primaMensual = 0f;
        public string observaciones = "";

        //Propiedades
        public string numero {
            get { return _numero; }
            set { _numero = Tools.StringLength(value, numero_length); }
        }
        public string fechaCreacion_string {
            get { return fechaCreacion.ToString(); }
            set { DateTime.TryParse(value, out fechaCreacion); }
        }
        public string fechaTermino_string {
            get { return fechaTermino.ToString(); }
            set { DateTime.TryParse(value, out fechaTermino); }
        }
        public string rutCliente {
            get { return _rutCliente; }
            set { _rutCliente = Tools.StringLength(value, rut_length); }
        }
        public string codigoPlan {
            get { return _codigoPlan; }
            set { _codigoPlan = Tools.StringLength(value, codigo_length); }
        }
        public string fechaInicioVigencia_string {
            get { return fechaInicioVigencia.ToString(); }
            set { DateTime.TryParse(value, out fechaInicioVigencia); }
        }
        public string fechaFinVigencia_string {
            get { return fechaFinVigencia.ToString(); }
            set { DateTime.TryParse(value, out fechaFinVigencia); }
        }

        #endregion

        #region BD

        //Find
        public static Contrato Find(string numero)
        {
            Contrato c = new Contrato();
            c.numero = numero;
            if (c.Select()) return c;
            else return null;
        }

        //Find All
        public static List<Contrato> FindAll()
        {
            List<Contrato> list = new List<Contrato>();
            Contrato contrato;
            foreach (object[] obj in Conexion.Select(table))
            {
                contrato = new Contrato();
                contrato.numero = (string)obj[0];
                contrato.fechaCreacion = (DateTime)obj[1];
                contrato.fechaTermino = (DateTime)obj[2];
                contrato.rutCliente = (string)obj[3];
                contrato.codigoPlan = (string)obj[4];
                contrato.fechaInicioVigencia = (DateTime)obj[5];
                contrato.fechaFinVigencia = (DateTime)obj[6];
                contrato.vigente = (bool)obj[7];
                contrato.declaracionSalud = (bool)obj[8];
                contrato.primaAnual = (float)(double)obj[9];
                contrato.primaMensual = (float)(double)obj[10];
                contrato.observaciones = (string)obj[11];
                list.Add(contrato);
            }
            
            return list;
        }

        //Select
        public bool Select()
        {
            object[] obj = Conexion.SelectFirst(table, where: $"Numero = '{numero}'");

            if (obj != null)
            {
                numero = (string)obj[0];
                fechaCreacion = (DateTime)obj[1];
                fechaTermino = (DateTime)obj[2];
                rutCliente = (string)obj[3];
                codigoPlan = (string)obj[4];
                fechaInicioVigencia = (DateTime)obj[5];
                fechaFinVigencia = (DateTime)obj[6];
                vigente = (bool)obj[7];
                declaracionSalud = (bool)obj[8];
                primaAnual = (float)(double)obj[9];
                primaMensual = (float)(double)obj[10];
                observaciones = (string)obj[11];
                return true;
            }
            return false;
        }

        //Insert
        public bool Insert()
        {
            if (Exist()) return false;
            Console.WriteLine("intentando insertar");
            bool corr = Conexion.Insert(table,
                $"'{numero}', " +
                $"'{fechaCreacion_string}', " +
                $"'{fechaTermino_string}', " +
                $"'{rutCliente}', " +
                $"'{codigoPlan}', " +
                $"'{fechaInicioVigencia_string}', " +
                $"'{fechaFinVigencia_string}', " +
                $"{(vigente?1:0)}, " +
                $"{(declaracionSalud?1:0)}, " +
                $"'{Tools.float2SQL(primaAnual)}', " +
                $"'{Tools.float2SQL(primaMensual)}', " +
                $"'{observaciones}'");

            return corr;
        }

        //Update
        public bool Update()
        {
            if (!Exist()) return false;

            int? i = Conexion.Update(table, 
                $"FechaCreacion = '{fechaCreacion_string}', " +
                $"FechaTermino = '{fechaTermino_string}', " +
                $"RutCliente = '{rutCliente}', " +
                $"CodigoPlan = '{codigoPlan}', " +
                $"FechaInicioVigencia = '{fechaInicioVigencia_string}', " +
                $"FechaFinVigencia = '{fechaFinVigencia_string}', " +
                $"Vigente = '{vigente.ToString()}', " +
                $"DeclaracionSalud = '{declaracionSalud.ToString()}', " +
                $"PrimaAnual = '{Tools.float2SQL(primaAnual)}', " +
                $"PrimaMensual = '{Tools.float2SQL(primaMensual)}', " +
                $"Observaciones = '{observaciones}'",
                $"Numero = '{numero}'");
            return i != null && i > 0;
        }

        //Delete
        public bool Delete()
        {
            if (!Exist()) return false;
            int? i = Conexion.Delete(table, $"Numero = '{numero}'");
            return i != null && i > 0;
        }

        //Exist
        public bool Exist()
        {
            return Conexion.SelectValue(table, "numero", $"Numero = '{numero}'") != null;
        }

        #endregion

        #region Other
        public override string ToString()
        {
            return $"Contrato > " +
                $"Num: {numero}, " +
                $"FC: {fechaCreacion.Date.ToString()}, " +
                $"FT: {fechaTermino.Date.ToString()}, " +
                $"RutCliente: {rutCliente}, " +
                $"CodigoPlan: {codigoPlan}, " +
                $"FIV: {fechaInicioVigencia.Date.ToString()}, " +
                $"FFV: {fechaFinVigencia.Date.ToString()}, " +
                $"Vigencia: {vigente.ToString()}, " +
                $"DecSalud: {declaracionSalud.ToString()}, " +
                $"PrimaAnual: {primaAnual}, " +
                $"PrimaMensual: {primaMensual}, " +
                $"Observaciones: {observaciones}";
        }
        #endregion
    }
}
