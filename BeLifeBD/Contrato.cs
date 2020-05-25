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
        public string _numero = "";
        public DateTime fechaCreacion = new DateTime();
        public DateTime fechaTermino = new DateTime();
        public string _rutCliente = "";
        public string _codigoPlan = "";
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

    }
}
