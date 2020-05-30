using BeLifeBD;
using ConexionBD;
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
    /// Lógica de interacción para BeLife.xaml
    /// </summary>
    public partial class BeLife : Window
    {
        public BeLife()
        {
            InitializeComponent();
        }

        private void ListMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListMenu.SelectedIndex;
            MoverCursor(index);
            switch (index)
            {
                case 0:
                    GridClientes.Visibility = Visibility.Collapsed;
                    GridPrincipal.Visibility = Visibility.Visible;
                    GridContratos.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    GridClientes.Visibility = Visibility.Visible;
                    GridPrincipal.Visibility = Visibility.Collapsed;
                    GridContratos.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    GridContratos.Visibility = Visibility.Visible;
                    GridClientes.Visibility = Visibility.Collapsed;
                    GridPrincipal.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void BtnMostrarContratos_Click(object sender, RoutedEventArgs e)
        {
            CambiarAGridContratos();
            GridContratosListas.Visibility = Visibility.Visible;
            object[][] matriz = Conexion.Select("Contrato").ToArray();
            foreach (object[] row in matriz)
            {
                var data = new Contrato { numero = (string)row[0], rutCliente = (string)row[2], vigente = (bool)row[6], fechaCreacion = (DateTime)row[1], codigoPlan = (string)row[3] };
                dtgMostrarContratos.Items.Add(data);
            }
        }

        private void BtnMostrarClientes_Click(object sender, RoutedEventArgs e)
        {
            CambiarAGridClientes();
            GridClientesListar.Visibility = Visibility.Visible;
            Cliente cliente = new Cliente();
            List<Cliente> listaCli = new List<Cliente>();
            object[][] matriz = Conexion.Select("Cliente").ToArray();
            foreach(object[] row in matriz)
            {
                var data = new Cliente { rut = (string)row[0], nombre = (string)row[1], apellido = (string)row[2], idSexo = (int)row[4], idEstadoCivil = (int)row[5] };
                dtgMostrarClientes.Items.Add(data);
            }
        }

        private void BtnAbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnAbrirMenu.Visibility = Visibility.Collapsed;
            BtnCerrarMenu.Visibility = Visibility.Visible;
        }

        private void BtnCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnAbrirMenu.Visibility = Visibility.Visible;
            BtnCerrarMenu.Visibility = Visibility.Collapsed;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnClose.Background = new SolidColorBrush(Color.FromRgb(247, 50, 50));
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnClose.Background = new SolidColorBrush(Color.FromRgb(126, 59, 129));
        }

        private void CambiarAGridClientes()
        {
            ListMenu.SelectedIndex = 1;
            GridContratos.Visibility = Visibility.Collapsed;
            GridClientes.Visibility = Visibility.Visible;
            GridPrincipal.Visibility = Visibility.Collapsed;
        }

        private void CambiarAGridContratos()
        {
            ListMenu.SelectedIndex = 2;
            GridContratos.Visibility = Visibility.Visible;
            GridClientes.Visibility = Visibility.Collapsed;
            GridPrincipal.Visibility = Visibility.Collapsed;
        }

        private void MoverCursor(int index)
        {
            Transi.OnApplyTemplate();
            BarraTran.Margin = new Thickness(0, 150 + (70 * index), 0, 0);
        }

        private void BtnAgregarUnCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente
            {
                rut = txtbNombreCliente.Text,
                nombre = txtbNombreCliente.Text,
                apellido = txtApellidoCliente.Text,
                fechaNacimiento = "31/03/2000",
                idSexo = txtGeneroCliente.SelectedIndex + 1,
                idEstadoCivil = txtEstadoCivilCliente.SelectedIndex + 1
            };
            MessageBox.Show(dtpFechNacimiento.Text);
            /*
            cliente.Insert();*
            /*if ()
            {
                MessageBox.Show("Cliente Agregado Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha Agregado el Cliente");
            }*/

        }


        private void TxtRutCliente_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtRutCliente.Text.Equals(""))
            {
                txtRutCliente.Text = "RUT";
            }
        }

        private void TxtRutCliente_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtRutCliente.Text.Equals("RUT"))
            {
                txtRutCliente.Text = "";
            }
        }

        private void TxtbNombreCliente_GotFocus(object sender, RoutedEventArgs e)
        {
            string nombre = txtbNombreCliente.Text.Trim();
            if (nombre.Equals("Nombre"))
            {
                txtbNombreCliente.Text = "";
            }
        }

        private void TxtbNombreCliente_LostFocus(object sender, RoutedEventArgs e)
        {
            string nombre = txtbNombreCliente.Text.Trim();
            if (nombre.Equals(""))
            {
                txtbNombreCliente.Text = "Nombre";
            }
        }

        private void TxtApellidoCliente_GotFocus(object sender, RoutedEventArgs e)
        {
            string apellido = txtApellidoCliente.Text.Trim();
            if (apellido.Equals("Apellido"))
            {
                txtApellidoCliente.Text = "";
            }
        }

        private void TxtApellidoCliente_LostFocus(object sender, RoutedEventArgs e)
        {
            string apelldo = txtApellidoCliente.Text.Trim();
            if (apelldo.Equals(""))
            {
                txtApellidoCliente.Text = "Apellido";
            }
        }


    }
}
