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

namespace Farmacia
{
    /// <summary>
    /// Lógica de interacción para VentanaProductos.xaml
    /// </summary>
    public partial class VentanaProductos : Window
    {
        RepositorioProducto repositorio;
        bool esNuevo;
        public VentanaProductos()
        {
            InitializeComponent();
            repositorio = new RepositorioProducto();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombreDelProducto.Clear();
            txbCategoria.Clear();
            txbDescripcion.Clear();
            txbPrecioDeCompra.Clear();
            txbPrecioDeVenta.Clear();
            txbPrecentacion.Clear();

            txbNombreDelProducto.IsEnabled = habilitadas;
            txbCategoria.IsEnabled = habilitadas;
            txbDescripcion.IsEnabled = habilitadas;
            txbPrecioDeCompra.IsEnabled = habilitadas;
            txbPrecioDeVenta.IsEnabled = habilitadas;
            txbPrecentacion.IsEnabled = habilitadas;
        }
        private void HabilitarBotones(bool habilitar)
        {
            btnNuevo.IsEnabled = habilitar;
            btnGuardar.IsEnabled = !habilitar;
            btnEditar.IsEnabled = habilitar;
            btnCancelar.IsEnabled = !habilitar;
            btnEliminar.IsEnabled = habilitar;
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            esNuevo = true;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreDelProducto.Text) || string.IsNullOrEmpty(txbCategoria.Text) || string.IsNullOrEmpty(txbDescripcion.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (esNuevo)
            {
                Productos a = new Productos()
                {
                    Nombre = txbNombreDelProducto.Text,
                    Categoria=txbCategoria.Text,
                    Descripcion = txbDescripcion.Text,
                    PrecioDeCompra = txbPrecioDeCompra.Text,
                    PrecioDeVenta = txbPrecioDeVenta.Text,
                    Presentacion = txbPrecentacion.Text
                };
                if (repositorio.AgregarProducto(a))
                {
                    MessageBox.Show("Datos Guardados Con Exito", "Nuevo Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);

                }
                else
                {
                    MessageBox.Show("Error Al Guardar los Datos Del Nuevo Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Productos original = dtgMateria.SelectedItem as Productos;
                Productos a = new Productos();
                a.Nombre = txbNombreDelProducto.Text;
                a.Categoria = txbCategoria.Text;
                a.Descripcion = txbDescripcion.Text;
                a.PrecioDeCompra = txbPrecioDeCompra.Text;
                a.PrecioDeVenta = txbPrecioDeVenta.Text;
                a.Presentacion = txbPrecentacion.Text;
                if (repositorio.modificarProducto(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Su Producto A Sido Actualizado", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error Al Guardar Al Nuevo Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgMateria.ItemsSource = null;
            dtgMateria.ItemsSource = repositorio.LeerProducto();
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerProducto().Count == 0)
            {
                MessageBox.Show("Producto No Guardado", "No Tiene Registros", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Productos a = dtgMateria.SelectedItem as Productos;
                    if (MessageBox.Show("Realamente desea eliminarr a " + a.Descripcion + "?", "Eliminar??", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarProducto(a))
                        {
                            MessageBox.Show("El Producto A sido Eliminado", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error Al Eliminar Al Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Producto", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerProducto().Count == 0)
            {
                MessageBox.Show("Error Al Eliminara Al Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Productos a = dtgMateria.SelectedItem as Productos;
                    HabilitarCajas(true);
                    txbNombreDelProducto.Text = a.Nombre;
                    txbCategoria.Text = a.Categoria;
                    txbDescripcion.Text = a.Descripcion;
                    txbPrecioDeCompra.Text = a.PrecioDeCompra;
                    txbPrecioDeVenta.Text = a.PrecioDeVenta;
                    txbPrecentacion.Text = a.Presentacion;

                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Producto", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
    }
}
