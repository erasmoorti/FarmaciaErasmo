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

namespace Farmacia
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {
            VentanaClientes Pagina = new VentanaClientes();
            Pagina.Show();
        }

        private void btnProducto_Click(object sender, RoutedEventArgs e)
        {
            VentanaProductos Pagina = new VentanaProductos();
            Pagina.Show();
        }

        private void btnEmpleado_Click(object sender, RoutedEventArgs e)
        {
            VentanaEmpleados Pagina = new VentanaEmpleados();
            Pagina.Show();

        }
    }
}
