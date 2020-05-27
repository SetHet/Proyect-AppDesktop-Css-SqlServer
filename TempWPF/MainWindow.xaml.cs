using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConexionBD;
using BeLifeBD;

namespace TempWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            user = new UsuarioTest();
        }

        #region Conexion
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Change address
            ConexionBD.Conexion.SetConnection(server.Text, bd.Text, Trusted_Conn.IsChecked.Value);
            Salida.Text = "Se ha cambiado la direccion de conexion";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Get string
            Salida.Text = ConexionBD.Conexion.connectionConfig;
        }

        private void TestConexion_Click(object sender, RoutedEventArgs e)
        {
            if (Conexion.isConnectionPosible())
            {
                Salida.Text = "ConexionBD Correcta: True";
            }
            else
            {
                Salida.Text = "ConexionBD Correcta: False";
            }
        }

        #endregion

        #region Usuario Test
        
        UsuarioTest user;

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //User toString
            Salida.Text = user.ToString();
        }

        private void Btn_SelectUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                user.id = id;
                if (user.Find())
                {
                    U_ID.Text = user.id.ToString();
                    U_Nombre.Text = user.nombre;
                    U_Apellido.Text = user.apellido;
                    Salida.Text = "Select user\n" + user.ToString();
                }
                else
                {
                    U_Nombre.Text = "";
                    U_Apellido.Text = "";
                    Salida.Text = "Usuario no encontrado";
                }
            }
        }

        private void Btn_InsertUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                user = new UsuarioTest();
                user.id = id;
                user.nombre = U_Nombre.Text;
                user.apellido = U_Apellido.Text;
                bool corr = user.Insert();
                if (corr) U_ID.Text = user.id.ToString();
                if (corr) Salida.Text = "Insercion correcta: True";
                else Salida.Text = "Insercion correcta: False";
            }
            
        }

        private void Btn_UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                user = new UsuarioTest();
                user.id = id;
                user.nombre = U_Nombre.Text;
                user.apellido = U_Apellido.Text;
                int row = user.Update();
                if (row > 0) Salida.Text = "Update correcto: True, cant: " + row;
                else Salida.Text = "Update correcto: False, cant: " + row;
            }
        }

        private void Btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                user = new UsuarioTest();
                user.id = id;
                int row = user.Delete();
                if (row > 0) Salida.Text = "Delete correcto: True, cant: " + row;
                else Salida.Text = "Delete correcto: False, cant: " + row;
            }
        }

        private void Btn_Search_Name_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                Salida.Text = UsuarioTest.GetName(id);
            }
        }

        private void Btn_Search_Group_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "";
            foreach(UsuarioTest u in UsuarioTest.GetTable())
            {
                mensaje += $"id: {u.id}, nombre: {u.nombre}, apellido: {u.apellido}\n";
            }
            Salida.Text = mensaje;
        }

        private void U_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(U_ID.Text, out int id))
            {
                
            }
            else
            {
                U_ID.Text = "0";
            }
        }

        #endregion

        #region Test Length
        
        private void TestLength_Click(object sender, RoutedEventArgs e)
        {
            Salida.Text = Tools.StringLength("0123456789", 5);
        }

        #endregion

        #region Login

        private void TestLogin_Click(object sender, RoutedEventArgs e)
        {
            Ejecutivo ej = new Ejecutivo();
            ej.usuario = Txt_Usuario.Text;
            ej.codigo = Txt_Codigo.Text;
            bool login = ej.ExistInBD();

            if (login) Salida.Text = "Inicio de sesion: True";
            else Salida.Text = "Inicio de sesion: False";
        }

        #endregion

        #region Clases Referencia

        private void Btn_FindSexo_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Txt_ID_Class_referencia.Text, out int id)){
                Sexo sexo = Sexo.Find(id);
                if (sexo != null)
                    Salida.Text = $"Sexo >> id: {sexo.id}, descripcion: {sexo.descripcion}";
                else
                    Salida.Text = $"Sexo >> no encontrado";
            }
        }

        private void Txt_ID_Class_referencia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(Txt_ID_Class_referencia.Text, out int id))
            {
                Txt_ID_Class_referencia.Text = "0";
            }
        }

        private void Btn_FindEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            EstadoCivil ec = EstadoCivil.Find(int.Parse(Txt_ID_Class_referencia.Text));

            if (ec != null)
            {
                Salida.Text = $"Estado Civil >> id: {ec.id}, desc: {ec.descripcion}";
            }
            else
            {
                Salida.Text = $"Estado Civil >> no encontrado";
            }
        }

        private void Btn_FindPlan_Click(object sender, RoutedEventArgs e)
        {
            Plan p = Plan.Find(Txt_ID_Class_referencia_String.Text);
            if (p != null)
                Salida.Text = $"Plan >> id: {p.id}, nombre: {p.nombre}, prima: {p.primaBase}, poliza: {p.polizaActual}";
            else
                Salida.Text = "Plan >> no encontrado";
        }

        #endregion

        #region Cliente

        void UpdateTxTCliente(Cliente c)
        {
            Txt_Cliente_Rut.Text = c.rut;
            Txt_Cliente_Nombres.Text = c.nombre;
            Txt_Cliente_Apellidos.Text = c.apellido;
            Txt_Cliente_FechaNacimiento.Text = c.fechaNacimiento;
            Txt_Cliente_Sexo.Text = c.idSexo.ToString();
            Txt_Cliente_EstadoCivil.Text = c.idEstadoCivil.ToString();
        }

        Cliente CreateCliente()
        {
            Cliente c = new Cliente();

            c.rut = Txt_Cliente_Rut.Text;
            c.nombre = Txt_Cliente_Nombres.Text;
            c.apellido = Txt_Cliente_Apellidos.Text;
            c.fechaNacimiento = Txt_Cliente_FechaNacimiento.Text;
            c.idSexo = int.Parse(Txt_Cliente_Sexo.Text);
            c.idEstadoCivil = int.Parse(Txt_Cliente_EstadoCivil.Text);

            return c;
        }

        private void Btn_Cliente_Find_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = Cliente.Find(Txt_Cliente_Rut.Text);
            if (c != null) Salida.Text = c.ToString();
            else Salida.Text = "Cliente > No encontrado";
        }

        private void Btn_Cliente_FindAll_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "Clientes: \n";
            foreach(Cliente c in Cliente.FindAll())
            {
                mensaje += c.ToString() + "\n";
            }
            Salida.Text = mensaje;
        }

        private void Btn_Cliente_Select_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = new Cliente();
            if (Txt_Cliente_Rut.Text != "")
                c.Select(Txt_Cliente_Rut.Text);
            else
                c.Select();
            UpdateTxTCliente(c);
            Salida.Text = "Select Cliente";
        }

        private void Btn_Cliente_Delete_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = CreateCliente();
            if (c.Delete()) Salida.Text = "Delete correcto: True";
            else Salida.Text = "Delete correcto: False";
        }

        private void Btn_Cliente_Update_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = CreateCliente();
            if (c.Update()) Salida.Text = "Update correcto: True";
            else Salida.Text = "Update correcto: False";
        }

        private void Btn_Cliente_Exist_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = CreateCliente();
            if (c.Exist()) Salida.Text = "Existe Cliente: True";
            else Salida.Text = "Existe Cliente: False";
        }
        
        private void Txt_Cliente_Sexo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(Txt_Cliente_Sexo.Text, out int i))
            {
                Txt_Cliente_Sexo.Text = "0";
            }
        }

        private void Txt_Cliente_EstadoCivil_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(Txt_Cliente_EstadoCivil.Text, out int i))
            {
                Txt_Cliente_EstadoCivil.Text = "0";
            }
        }

        private void Btn_Cliente_Insert_Click(object sender, RoutedEventArgs e)
        {
            Cliente c = CreateCliente();
            if (c.Insert()) Salida.Text = "Cliente Insert: True";
            else Salida.Text = "Cliente Insert: False";

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Date Time
            Cliente c = new Cliente();
            try
            {
                Salida.Text = "";
                c.fechaNacimiento_DateTime = DateTime.Parse(Txt_Cliente_FechaNacimiento.Text);
                Salida.Text += c.fechaNacimiento_DateTime.ToString() + "\n";
                Salida.Text += "Test Date Time correcto: True";
            }
            catch (Exception ex)
            {
                Salida.Text = "Test Date Time correcto: False\n" + ex.Message;
            }
        }

        #endregion

        #region Contrato

        Contrato CreateContrato()
        {
            Contrato c = new Contrato();
            try
            {
                c.numero = UI_Contrato_Numero.Text;
                c.fechaCreacion = UI_Contrato_FechaCreacion.SelectedDate.Value;
                c.fechaTermino = UI_Contrato_FechaTermino.SelectedDate.Value;
                c.rutCliente = UI_Contrato_RutCliente.Text;
                c.codigoPlan = UI_Contrato_CodigoPlan.Text;
                c.fechaInicioVigencia = UI_Contrato_FechaInicioVigencia.SelectedDate.Value;
                c.fechaFinVigencia = UI_Contrato_FechaFinVigencia.SelectedDate.Value;
                c.vigente = UI_Contrato_Vigente.IsChecked.Value;
                c.declaracionSalud = UI_Contrato_DeclaracionSalud.IsChecked.Value;
                c.primaAnual = float.Parse(UI_Contrato_PrimaAnual.Text);
                c.primaMensual = float.Parse(UI_Contrato_PrimaMensual.Text);
                c.observaciones = UI_Contrato_Observaciones.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n>>>>ERROR de create contrato: \n" + ex.Message);
            }
            return c;
        }

        void LoadContrato(Contrato c)
        {
            UI_Contrato_Numero.Text = c.numero;
            UI_Contrato_FechaCreacion.SelectedDate = c.fechaCreacion;
            UI_Contrato_FechaTermino.SelectedDate = c.fechaTermino;
            UI_Contrato_RutCliente.Text = c.rutCliente;
            UI_Contrato_CodigoPlan.Text = c.codigoPlan;
            UI_Contrato_FechaInicioVigencia.SelectedDate = c.fechaInicioVigencia;
            UI_Contrato_FechaFinVigencia.SelectedDate = c.fechaFinVigencia;
            UI_Contrato_Vigente.IsChecked = c.vigente;
            UI_Contrato_DeclaracionSalud.IsChecked = c.declaracionSalud;
            UI_Contrato_PrimaAnual.Text = c.primaAnual.ToString();
            UI_Contrato_PrimaMensual.Text = c.primaMensual.ToString();
            UI_Contrato_Observaciones.Text = c.observaciones;
        }

        private void Btn_Find_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            c = Contrato.Find(c.numero);
            if (c != null) Salida.Text = c.ToString();
            else Salida.Text = "Contrato no encontrado";
        }

        private void Btn_FindAll_Click(object sender, RoutedEventArgs e)
        {
            Salida.Text = "Lista Contratos: \n";
            foreach(Contrato c in Contrato.FindAll())
            {
                Salida.Text += c.ToString() + "\n";
            }
        }

        private void Btn_Exist_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            if (c.Exist()) Salida.Text = "Contrato Exist: True";
            else Salida.Text = "Contrato Exist: False";

            Salida.Text += "\n" + c.ToString();
        }

        private void Btn_Select_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            if (c.Select()) Salida.Text = "Contrato Select: True";
            else Salida.Text = "Contrato Select: False";
            LoadContrato(c);
        }

        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            if (c.Insert()) Salida.Text = "Contrato Insert: True";
            else Salida.Text = "Contrato Insert: False";
        }

        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            if (c.Update()) Salida.Text = "Contrato Update: True";
            else Salida.Text = "Contrato Update: False";
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = CreateContrato();
            if (c.Delete()) Salida.Text = "Contrato Delete: True";
            else Salida.Text = "Contrato Delete: False";
        }
    }
        #endregion
}
