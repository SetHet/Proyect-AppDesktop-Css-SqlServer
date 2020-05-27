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
            Ejecutivo ej = new Ejecutivo();
            ej.usuario = txtUser.Text;
            ej.codigo = txtPass.Password.ToString();
            bool login = ej.ExistInBD();
            if (login)
            {
                BeLife beLife = new BeLife();
                beLife.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El Usuario o la Contraseña no son válidos");
            }
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
            
        }
    }
}
