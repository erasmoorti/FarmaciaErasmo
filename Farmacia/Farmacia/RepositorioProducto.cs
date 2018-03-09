using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia
{
    public class RepositorioProducto
    {
        ManejadorDeArchivos archivosProductos;
        List<Productos> Producto;
        public RepositorioProducto()
        {
            archivosProductos = new ManejadorDeArchivos("Productos.txt");
            Producto = new List<Productos>();
        }

        public bool AgregarProducto(Productos producto)
        {
            Producto.Add(producto);
            bool resultado = ActualizarArchivo();
            Producto = LeerProducto();
            return resultado;
        }

        public bool EliminarProducto(Productos producto)
        {
            Productos Temporal = new Productos();
            foreach (var item in Producto)
            {
                if (item.Categoria == producto.Categoria)
                {
                    Temporal = item;
                }
            }
            Producto.Remove(Temporal);
            bool resultado = ActualizarArchivo();
            Producto= LeerProducto();
            return resultado;
        }
        public bool modificarProducto(Productos original, Productos modificado)
        {
            Productos temporal = new Productos();
            foreach (var item in Producto)
            {
                if (original.Categoria == item.Categoria)
                {
                    temporal = item;
                }
            }
            temporal.Nombre = modificado.Nombre;
            temporal.Categoria = modificado.Categoria;
            temporal.Descripcion = modificado.Descripcion;
            temporal.PrecioDeCompra = modificado.PrecioDeCompra;
            temporal.PrecioDeVenta = modificado.PrecioDeVenta;
            temporal.Presentacion = modificado.Presentacion;
            bool resultado = ActualizarArchivo();
            Producto = LeerProducto();
            return resultado;

        }

        private bool ActualizarArchivo()
        {
            string datos = "";
            foreach (Productos item in Producto)
            {
                datos += string.Format("{0}|{1}|{2}|{3}|{4}|{5}\n", item.Nombre, item.Categoria, item.Descripcion, item.PrecioDeCompra, item.PrecioDeVenta, item.Presentacion);

            }
            return archivosProductos.Guardar(datos);
        }
        public List<Productos> LeerProducto()
        {
            string datos = archivosProductos.Leer();
            if (datos != null)
            {
                List<Productos> Produ = new List<Productos>();
                string[] lineas = datos.Split('\n');
                for (int i = 0; i < lineas.Length - 1; i++)
                {
                    string[] campos = lineas[i].Split('|');
                    Productos a = new Productos()
                    {
                        Nombre = campos[0],
                        Categoria = campos[1],
                        Descripcion = campos[2],
                        PrecioDeCompra = campos[3],
                        PrecioDeVenta = campos[4],
                        Presentacion=campos[5],
                    };
                    Produ.Add(a);
                }
                Producto = Produ;
                return Produ;
            }
            else
            {
                return null;
            }
        }
    }
}
