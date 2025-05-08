using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarProductoDulceriaModeloVista : BaseModeloVista
    {
        public ObservableCollection<ProductoDulceria> Productos { get; set; }
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarCambio = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient DulceriaServicioCliente;

        public ICommand AgregarNuevoProductoComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand MostrarMensajeConfirmarCambiosComando { get; }
        public ICommand ConfirmarCambioComando { get; }
        public ICommand CancelarCambioComando { get; }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged(nameof(MostrarMensajeCancelarOperacion));
            }
        }

        public Visibility MostrarMensajeConfirmarCambio
        {
            get { return _mostrarMensajeConfirmarCambio; }
            set
            {
                _mostrarMensajeConfirmarCambio = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmarCambio));
            }
        }

        public AgregarProductoDulceriaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            ConfirmarCambioComando = new ComandoModeloVista(ConfirmarCambio);
            CancelarCambioComando = new ComandoModeloVista(CancelarCambio);
            AgregarNuevoProductoComando = new ComandoModeloVista(AgregarNuevoProducto);
            CancelarComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            MostrarMensajeConfirmarCambiosComando = new ComandoModeloVista(ConfirmarInventario);
            DulceriaServicioCliente = new DulceriaServicioClient();
            Productos = new ObservableCollection<ProductoDulceria>();
            InicializarListaProductos();
        }

        public void ConfirmarInventario(object obj)
        {
            MostrarMensajeConfirmarCambio = Visibility.Visible;
        }

        private void AgregarNuevoProducto(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new AgregarNuevoProductoDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void CancelarOperacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        private void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        private void ConfirmarCambio(object obj)
        {
            Dictionary<int, int> idProductoInventario = new Dictionary<int, int>();

            foreach (var producto in Productos)
            {
                if (int.TryParse(producto.CantidadInventario, out int cantidad))
                {
                    idProductoInventario.Add(producto.Id, cantidad);
                }
            }

            try 
            {
                var respuesta = DulceriaServicioCliente.AgregarInventario(idProductoInventario);
                if (respuesta != null && respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Inventario actualizado correctamente");
                }
                else
                {
                    Notificacion.Mostrar("Ha ocurrido un error inesperado");
                }
            } 
            catch (Exception) 
            { 
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }

            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
        }

        private void CancelarCambio(object obj)
        {
            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
        }

        private void InicializarListaProductos()
        {
            try
            {
                var productos = DulceriaServicioCliente.ObtenerProductosDulceria();
                if (productos != null)
                {
                    foreach (var producto in productos.Productos)
                    {
                        Productos.Add(new ProductoDulceria
                        {
                            Id = producto.IdProducto,
                            Nombre = producto.Nombre,
                            CostoUnitario = producto.CostoUnitario.ToString(),
                            PrecioVentaUnitario = producto.PrecioVentaUnitario.ToString(),
                            CantidadInventario = producto.CantidadInventario.ToString()
                        });
                    }
                }
            } 
            catch (Exception) 
            { 
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }
    }
}
