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
            user = new Usuario();
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

        Usuario user;

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
                user.Find();

                U_Nombre.Text = user.nombre;
                U_Apellido.Text = user.apellido;
                Salida.Text = "Select user\n" + user.ToString();
            }
        }

        private void Btn_InsertUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_UpdateUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
