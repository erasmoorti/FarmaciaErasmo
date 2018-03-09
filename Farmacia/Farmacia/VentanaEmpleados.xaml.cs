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
    /// Lógica de interacción para VentanaEmpleados.xaml
    /// </summary>
    public partial class VentanaEmpleados : Window
    {
        RepositorioEmpleado repositorio;
        bool esNuevo;
        public VentanaEmpleados()
        {
            InitializeComponent();
            repositorio = new RepositorioEmpleado();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
            txbNombreDelEmpleado.Clear();
            txbTelefono.Clear();
            txbPuesto.Clear();
            txbDireccion.Clear();          
            
            txbNombreDelEmpleado.IsEnabled = habilitadas;
            txbTelefono.IsEnabled = habilitadas;
            txbPuesto.IsEnabled = habilitadas;
            txbDireccion.IsEnabled = habilitadas;
           
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
            if (string.IsNullOrEmpty(txbNombreDelEmpleado.Text) || string.IsNullOrEmpty(txbTelefono.Text) || string.IsNullOrEmpty(txbPuesto.Text))
            {
                MessageBox.Show("Faltan Datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (esNuevo)
            {
                Empleados a = new Empleados()
                {
                    Nombre = txbNombreDelEmpleado.Text,
                    Telefono = txbTelefono.Text,
                    Puesto=txbPuesto.Text,
                    Direccion = txbDireccion.Text,

                };
                if (repositorio.AgregarEmpleado(a))
                {
                    MessageBox.Show("Datos Guardados Con Exito", "Nuevo Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);

                }
                else
                {
                    MessageBox.Show("Error Al Guardar los Datos Del Empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Empleados original = dtgMateria.SelectedItem as Empleados;
                Empleados a = new Empleados();
                a.Nombre = txbNombreDelEmpleado.Text;
                a.Telefono = txbTelefono.Text;
                a.Puesto = txbPuesto.Text;
                a.Direccion = txbDireccion.Text;
                if (repositorio.modificarEmpleado(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Su Empleado A Sido Actualizado", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error Al Guardar A Empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void ActualizarTabla()
        {
            dtgMateria.ItemsSource = null;
            dtgMateria.ItemsSource = repositorio.LeerEmpleado();
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerEmpleado().Count == 0)
            {
                MessageBox.Show("Cliente No Guardado", "No Tiene Registros", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Empleados a = dtgMateria.SelectedItem as Empleados;
                    if (MessageBox.Show("Realamente Desea Eliminar a " + a.Puesto + "?", "Eliminar??", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.EliminarEmpleado(a))
                        {
                            MessageBox.Show("El Empleado A sido Eliminado", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error Al Eliminara Al Empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Empleado", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.LeerEmpleado().Count == 0)
            {
                MessageBox.Show("Error Al Eliminara Al Empleado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgMateria.SelectedItem != null)
                {
                    Empleados a = dtgMateria.SelectedItem as Empleados;
                    HabilitarCajas(true);
                    txbNombreDelEmpleado.Text = a.Nombre;
                    txbTelefono.Text = a.Telefono;
                    txbPuesto.Text = a.Puesto;
                    txbDireccion.Text = a.Direccion;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿Cual?", "Empleado", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
    }
}
