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
        }

        private void BtnMostrarClientes_Click(object sender, RoutedEventArgs e)
        {
            CambiarAGridClientes();
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

   
    }
}
