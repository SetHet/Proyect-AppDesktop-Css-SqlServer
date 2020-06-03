using BeLifeBD;
using ConexionBD;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
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
                    GridRegistrarCliente.Visibility = Visibility.Visible;
                    GridClientesListar.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    GridContratos.Visibility = Visibility.Visible;
                    GridGestionContrato.Visibility = Visibility.Visible;
                    GridClientes.Visibility = Visibility.Collapsed;
                    GridPrincipal.Visibility = Visibility.Collapsed;
                    GridAgregarContrato.Visibility = Visibility.Visible;
                    GridContratosListas.Visibility = Visibility.Collapsed;
                    GridActu.Visibility = Visibility.Collapsed;
                    GridAgregarContrato.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void BtnMostrarContratos_Click(object sender, RoutedEventArgs e)
        {
            CambiarAGridContratos();
            GridContratosListas.Visibility = Visibility.Visible;
            GridAgregarContrato.Visibility = Visibility.Collapsed;
            object[][] matriz = Conexion.Select("Contrato").ToArray();
            foreach (object[] row in matriz)
            {
                var data = new DatosContrato { Numero = (string)row[0], RutTitular = (string)row[3], Vigencia = ((bool)row[7]).ToString(), FechaCreacion = ((DateTime)row[1]).ToString(), PlanAsociado = (string)row[4] };
                dtgMostrarContratos.Items.Add(data);
            }
        }

        private void BtnMostrarClientes_Click(object sender, RoutedEventArgs e)
        {
            CambiarAGridClientes();
            GridClientesListar.Visibility = Visibility.Visible;
            GridRegistrarCliente.Visibility = Visibility.Collapsed;
            dtgMostrarClientes.Items.Clear();
            DatosClientes datos = new DatosClientes(); 
            object[][] matriz = Conexion.Select("Cliente").ToArray();
            foreach(object[] row in matriz)
            {
                string descg = datos.Generos((int)row[4]);
                string desce = datos.EstadoCivil((int)row[5]);
                var data = new DatosClientes { Rut = (string)row[0], Nombre = (string)row[1], Apellido = (string)row[2], Genero = descg, Estado = desce };
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

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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

        private void BtnAgregarUnCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente
            {
                rut = txtRutCliente.Text,
                nombre = txtbNombreCliente.Text,
                apellido = txtApellidoCliente.Text,
                fechaNacimiento = dtpFechNacimiento.Text,
                idSexo = txtGeneroCliente.SelectedIndex + 1,
                idEstadoCivil = txtEstadoCivilCliente.SelectedIndex + 1
            };
            if (cliente.Insert())
            {
                MessageBox.Show("Cliente Agregado Correctamente");
            }
            else
            {
                MessageBox.Show("No se ha Agregado el Cliente");
            }

        }

        private void BtnBuscarClientes_Click(object sender, RoutedEventArgs e)
        {

            List<Cliente> listClientes = new List<Cliente>();
            Cliente cliente = Cliente.Find(txtRutCliente.Text);
            if (cliente != null)
            {
                listClientes.Add(cliente);
                txtbNombreCliente.Text = listClientes[0].nombre;
                txtApellidoCliente.Text = listClientes[0].apellido;
                txtGeneroCliente.SelectedIndex = listClientes[0].idSexo - 1;
                txtEstadoCivilCliente.SelectedIndex = listClientes[0].idEstadoCivil - 1;
                dtpFechNacimiento.Text = cliente.fechaNacimiento;
            }
            else
            {
                MessageBox.Show("Cliente no encontrado");
            }

        }

        private void BtnBorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRutCliente.Text.Trim();
            if (rut.Equals("RUT") || rut.Equals(""))
            {
                MessageBox.Show("Primero Ingrese un RUT y Busque el Cliente");
            }
            else
            {
                Cliente cliente = new Cliente();
                if (cliente.Delete(rut))
                {
                    MessageBox.Show("Cliente Eliminado");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el cliente o no existe");
                }
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRutCliente.Text.Trim();
            if (rut.Equals("RUT")||rut.Equals(""))
            {
                MessageBox.Show("Primero Ingrese un RUT y Busque el Cliente");
            }
            else
            {
                Cliente cliente = new Cliente
                {
                    rut = txtRutCliente.Text,
                    nombre = txtbNombreCliente.Text,
                    apellido = txtApellidoCliente.Text,
                    fechaNacimiento = dtpFechNacimiento.Text,
                    idSexo = txtGeneroCliente.SelectedIndex + 1,
                    idEstadoCivil = txtEstadoCivilCliente.SelectedIndex + 1
                };
                if (cliente.Update())
                {
                    MessageBox.Show("Cliente Actualizado");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el cliente");
                }
            }

        }

        private void Btn_agregar_Click(object sender, RoutedEventArgs e)
        {
            DateTime DiaYHora = DateTime.Today;
            try
            {
                Contrato contrato = new Contrato
                {
                    numero = "3",
                    fechaCreacion = DiaYHora,
                    fechaTermino = DiaYHora.AddYears(5),
                    rutCliente = txtb_titular.Text,
                    codigoPlan = "VID0" + txtb_plan.SelectedIndex + 1.ToString(),
                    fechaInicioVigencia = txtb_inicioVig.SelectedDate.Value,
                    fechaFinVigencia = txtb_terminoVig.SelectedDate.Value,
                    declaracionSalud = txtb_salud.IsChecked.Value,
                    vigente = txtb_vigente.IsChecked.Value,
                    primaAnual = float.Parse(txtb_primaAnu.Text),
                    primaMensual = float.Parse(txtb_primaMen.Text),
                    observaciones = txtb_obs.Text
                };
                if (contrato.Insert())
                {
                    MessageBox.Show("Insertado Correctamente");
                }
                else
                {
                    MessageBox.Show("Algo Salió Mal...");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Ingrese todos los datos");
            }


        }

        private void BtnContraste_Click(object sender, RoutedEventArgs e)
        {
            var paleta = new PaletteHelper();
            ITheme tema = paleta.GetTheme();
            tema.PrimaryMid = new ColorPair(Colors.Red,Colors.Red);
            
            tema.TextBoxBorder = Colors.Red;
            paleta.SetTheme(tema);
        }


        private void BtnEliminarContrato_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contrato contrato = new Contrato
                {
                    numero = txtBuscarNumero.Text
                };

                if (contrato.Delete())
                {
                    MessageBox.Show("Contrato Eliminado");
                }
                else
                {
                    MessageBox.Show("Algo Salió Mal");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnActualizarContrato_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contrato contrato = new Contrato
                {
                    rutCliente = txta_titular.Text,
                    codigoPlan = "VID0" + txta_plan.SelectedIndex + 1.ToString(),
                    fechaInicioVigencia = txta_inicioVig.SelectedDate.Value,
                    fechaFinVigencia = txta_terminoVig.SelectedDate.Value,
                    vigente = CB_estado.IsChecked.Value,
                    declaracionSalud = txta_salud.IsChecked.Value,
                    primaAnual = float.Parse(txta_primaAnu.Text),
                    primaMensual = float.Parse(txta_primaMen.Text),
                    observaciones = txta_obs.Text
                };
                if (contrato.Update())
                {
                    MessageBox.Show("Contrato Actualizado");
                }
                else
                {
                    MessageBox.Show("Algo Salió Mal");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnAgregarContrato_Click(object sender, RoutedEventArgs e)
        {
            GridAgregarContrato.Visibility = Visibility.Visible;
            GridGestionContrato.Visibility = Visibility.Collapsed;
        }

        private void BtnBuscarContraro_Click(object sender, RoutedEventArgs e)
        {
            stkBuscar.Visibility = Visibility.Visible;
          
        }

        private void BtnEncontrarContra_Click(object sender, RoutedEventArgs e)
        {

            Contrato contrato = new Contrato();
            contrato = Contrato.Find(txtBuscarNumero.Text);
            if (contrato !=null)
            {
                GridGestionContrato.Visibility = Visibility.Collapsed;
                GridActu.Visibility = Visibility.Visible;
                txta_titular.Text = contrato.rutCliente;
                txta_obs.Text = contrato.observaciones;
                txta_primaAnu.Text = contrato.primaAnual.ToString();
                txta_primaMen.Text = contrato.primaMensual.ToString();
                lblNContrato.Content = "Contrato Nº" + contrato.numero;
                txta_creacion.Text = contrato.fechaCreacion.ToString();
                txta_inicioVig.Text = contrato.fechaInicioVigencia.ToString();
                txta_terminoVig.Text = contrato.fechaFinVigencia.ToString();
                txta_termino.Text = contrato.fechaTermino.ToString();
                txta_salud.IsChecked = contrato.declaracionSalud;
                CB_estado.IsChecked = contrato.declaracionSalud;
            }
            else
            {
                MessageBox.Show("El número no está asociado a ningún contrato");
            }
        }
    }
}