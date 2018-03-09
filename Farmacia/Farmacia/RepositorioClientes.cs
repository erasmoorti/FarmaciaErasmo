using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class RepositorioClientes
    {
        ManejadorDeArchivos archivosCliente;
            List<Clientes> Clientes;
        public RepositorioClientes()
        {
            archivosCliente = new ManejadorDeArchivos("Clientes.txt");
            Clientes = new List<Clientes>();
        }

        public bool AgregarCliente(Clientes cliente)
        {
            Clientes.Add(cliente);
            bool resultado = ActualizarArchivo();
            Clientes = LeerCliente();
            return resultado;
        }
        
        public bool EliminarClientes(Clientes Cliente)
        {
            Clientes Temporal = new Clientes();
            foreach (var item in Clientes)
            {
                if (item.Nombre == Cliente.Nombre)
                {
                    Temporal = item;
                }
            }
            Clientes.Remove(Temporal);
            bool resultado = ActualizarArchivo();
            Clientes = LeerCliente();
            return resultado;
        }
        public bool modificarCliente(Clientes original, Clientes modificado)
        {
            Clientes temporal = new Clientes();
            foreach (var item in Clientes)
            {
                if (original.Telefono == item.Telefono)
                {
                    temporal = item;
                }
            }
            temporal.Nombre = modificado.Nombre;
            temporal.Direccion = modificado.Direccion;
            temporal.RFC = modificado.RFC;
            temporal.Telefono = modificado.Telefono;
            temporal.Correo = modificado.Correo;
            bool resultado = ActualizarArchivo();
            Clientes = LeerCliente();
            return resultado;

        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Clientes item in Clientes )
            {
                datos += string.Format("{0}|{1}|{2}|{3}|{4}\n", item.Nombre, item.Direccion, item.RFC, item.Telefono, item.Correo);

            }
            return archivosCliente.Guardar(datos);
        }
        public List<Clientes> LeerCliente()
        {
            string datos = archivosCliente.Leer();
            if(datos != null)
            {
                List<Clientes> Client = new List<Clientes>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i<lineas.Length-1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Clientes a = new Clientes()
                    {
                        Nombre = campos[0],
                        Direccion = campos[1],
                        RFC = campos[2],
                        Telefono = campos[3],
                        Correo = campos[4],

                    };
                    Client.Add(a);
                }
                Clientes = Client;
                return Client;
            }
            else
            {
                return null;
            }
        }
    }
}
