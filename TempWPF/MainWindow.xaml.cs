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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Change address
            ConexionBD.Conexion.SetConnection(server.Text, bd.Text, Trusted_Conn.IsChecked.Value);
            Salida.Text = "Se ha cambiado la direccion de conexion";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Get string
            Salida.Text = ConexionBD.Conexion.connectionString;
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

        private void TestLength_Click(object sender, RoutedEventArgs e)
        {
            Salida.Text = Tools.StringLength("0123456789", 5);
        }

        private void TestLogin_Click(object sender, RoutedEventArgs e)
        {
            Ejecutivo ej = new Ejecutivo();
            ej.usuario = Txt_Usuario.Text;
            ej.codigo = Txt_Codigo.Text;
            bool login = ej.ExistInBD();

            if (login) Salida.Text = "Inicio de sesion: True";
            else Salida.Text = "Inicio de sesion: False";
        }

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
    }
}
