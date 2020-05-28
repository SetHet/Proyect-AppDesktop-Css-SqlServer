using BeLifeBD;
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
using System.Windows.Shapes;
using ArchivosLocales;
using ConexionBD;

namespace Interfaz
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
        {
            InitializeComponent();
            StartConfiguracion();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            GridInicio.Visibility = Visibility.Collapsed;
            GridConfig.Visibility = Visibility.Visible;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            /*Ejecutivo ej = new Ejecutivo();
            ej.usuario = txtUser.Text;
            ej.codigo = txtPass.Password.ToString();
            bool login = ej.ExistInBD();
            if (login)
            {*/
                BeLife beLife = new BeLife();
                beLife.Show();
                this.Close();
            /*}
            else
            {
                MessageBox.Show("El Usuario o la Contraseña no son válidos");
            }*/
        }
        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnClose.Background = new SolidColorBrush(Color.FromRgb(247, 50, 50));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnClose.Background = new SolidColorBrush(Color.FromRgb(126, 59, 129));
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            GridConfig.Visibility = Visibility.Collapsed;
            GridInicio.Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ui2config();
            fileControl.DiccToFile(config.dicc);
            SetConnection();
            Console.WriteLine(">>>>>Coneccion correcta: " + TestConexion());
        }

        #region Configuracion

        Configuracion config;
        FileControlDicc fileControl;

        void StartConfiguracion()
        {
            config = new Configuracion();
            fileControl = new FileControlDicc("ConfiguracionBD.belifeconfiggg");

            if (fileControl.Exist)
            {
                config.dicc = fileControl.FileToDicc();
            }
            else
            {
                fileControl.DiccToFile(config.dicc);
            }

            config2ui();

            SetConnection();
        }

        void SetConnection()
        {
            Conexion.SetConnection(config.dicc["Server"], config.dicc["Database"], config.dicc["Trusted_Connection"] == "True");
        }

        void config2ui()
        {
            txtServer.Text = config.dicc["Server"];
            txtData.Text = config.dicc["Database"];
            CheckSafeCon.IsChecked = config.dicc["Trusted_Connection"] == "True";
        }

        void ui2config()
        {
            config.dicc["Server"] = txtServer.Text;
            config.dicc["Database"] = txtData.Text;
            config.dicc["Trusted_Connection"] = CheckSafeCon.IsChecked.Value?"True":"False";
        }

        bool TestConexion()
        {
            return Conexion.isConnectionPosible();
        }
        
        #endregion
    }
}
