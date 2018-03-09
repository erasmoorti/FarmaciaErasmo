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
    /// Lógica de interacción para VentanaClientes.xaml
    /// </summary>
    public partial class VentanaClientes : Window
    {
        RepositorioClientes repositorio;
        bool esNuevo;
        public VentanaClientes()
        {
            InitializeComponent();
            repositorio = new RepositorioClientes();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbCorreo.Clear();
            txbDireccion.Clear();
            txbNombreDelCliente.Clear();
            txbRFC.Clear();
            txbTelefono.Clear();

            txbCorreo.IsEnabled = habilitadas;
            txbDireccion.IsEnabled = habilitadas;
            txbNombreDelCliente.IsEnabled = habilitadas;
            txbRFC.IsEnabled = habilitadas;
            txbTelefono.IsEnabled = habilitadas;
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
            if (string.IsNullOrEmpty(txbNombreDelCliente.Text) || string.IsNullOrEmpty(txbDireccion.Text) || string.IsNullOrEmpty(txbRFC.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (esNuevo)
            {
                Clientes a = new Clientes()
                {
                    Nombre = txbNombreDelCliente.Text,
                    Direccion = txbDireccion.Text,
                    RFC = txbRFC.Text,
                    Telefono = txbTelefono.Text,
                    Correo = txbCorreo.Text
                };
                if (repositorio.AgregarCliente(a))
                {
                    MessageBox.Show("Datos Guardados Con Exito", "Nuevo Ciente", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);

                }
                else
                {
                    MessageBox.Show("Error Al Guardar los Datos Del Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clientes original = dtgMateria.SelectedItem as Clientes;
                Clientes a = new Clientes();
                a.Nombre = txbNombreDelCliente.Text;
                a.Direccion = txbDireccion.Text;
                a.RFC = txbRFC.Text;
                a.Telefono = txbTelefono.Text;
                a.Correo = txbCorreo.Text;
                if (repositorio.modificarCliente(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Su Cliente A Sido Actualizado", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error Al Guardar Al Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgMateria.ItemsSource = null;
            dtgMateria.ItemsSource = repositorio.LeerCliente();
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerCliente().Count == 0)
            {
                MessageBox.Show("Cliente No Guardado", "No Tiene Registros", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Clientes a = dtgMateria.SelectedItem as Clientes;
                    if (MessageBox.Show("Realamente desea eliminarr a " + a.Correo + "?", "Eliminar??", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarClientes(a))
                        {
                            MessageBox.Show("El cliente A sido Eliminado", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error Al Eliminar Al Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerCliente().Count==0)
            {
                MessageBox.Show("Error Al Eliminara Al Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Clientes a = dtgMateria.SelectedItem as Clientes;
                    HabilitarCajas(true);
                    txbNombreDelCliente.Text = a.Nombre;
                    txbDireccion.Text = a.Direccion;
                    txbRFC.Text = a.RFC;
                    txbCorreo.Text = a.Correo;
                    txbTelefono.Text = a.Telefono;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
    }
}
