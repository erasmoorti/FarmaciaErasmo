using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class RepositorioEmpleado
    {
        ManejadorDeArchivos archivosEmpleado;
        List<Empleados> LixEmpleado;
        public RepositorioEmpleado()
        {
            archivosEmpleado = new ManejadorDeArchivos("Empleados.txt");
            LixEmpleado = new List<Empleados>();
        }

        public bool AgregarEmpleado(Empleados Empleado)
        {
            LixEmpleado.Add(Empleado);
            bool resultado = ActualizarArchivo();
            LixEmpleado = LeerEmpleado();
            return resultado;
        }

        public bool EliminarEmpleado(Empleados Empleado)
        {
            Empleados Temporal = new Empleados();
            foreach (var item in LixEmpleado)
            {
                if (item.Nombre == Empleado.Nombre)
                {
                    Temporal = item;
                }
            }
            LixEmpleado.Remove(Temporal);
            bool resultado = ActualizarArchivo();
            LixEmpleado = LeerEmpleado();
            return resultado;
        }
        public bool modificarEmpleado(Empleados original, Empleados modificado)
        {
            Empleados temporal = new Empleados();
            foreach (var item in LixEmpleado)
            {
                if (original.Telefono == item.Telefono)
                {
                    temporal = item;
                }
            }
            temporal.Nombre = modificado.Nombre;
            temporal.Telefono = modificado.Telefono;
            temporal.Puesto = modificado.Puesto;
            temporal.Direccion = modificado.Direccion;
            bool resultado = ActualizarArchivo();
            LixEmpleado = LeerEmpleado();
            return resultado;

        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Empleados item in LixEmpleado)
            {
                datos += string.Format("{0}|{1}|{2}|{3}\n", item.Nombre, item.Telefono, item.Puesto, item.Direccion );

            }
            return archivosEmpleado.Guardar(datos);
        }
        public List<Empleados> LeerEmpleado()
        {
            string datos = archivosEmpleado.Leer();
            if (datos != null)
            {
                List<Empleados> Emple = new List<Empleados>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Empleados a = new Empleados()
                    {
                        Nombre = campos[0],
                        Telefono = campos[1],
                        Puesto = campos[2],
                        Direccion = campos[3],
                    };
                    Emple.Add(a);
                }
                LixEmpleado = Emple;
                return Emple;
            }
            else
            {
                return null;
            }
        }
    }
}
