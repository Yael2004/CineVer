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
    public class EditarProductoDulceriaModeloVista : BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ObservableCollection<ProductoDulceria> Productos { get; set; }
        private DulceriaServicioClient _dulceriaServicioCliente { get; set; }

        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand SeleccionarProductoComando { get; }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged(nameof(MostrarMensajeCancelarOperacion));
            }
        }

        public EditarProductoDulceriaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            Productos = new ObservableCollection<ProductoDulceria>();
            _dulceriaServicioCliente = new DulceriaServicioClient();
            InicializarListaProductos();
            CancelarComando = new ComandoModeloVista(Cancelar);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            SeleccionarProductoComando = new ComandoModeloVista(SeleccionarProducto);
        }

        private void Cancelar(object obj)
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

        private void SeleccionarProducto(object obj)
        {
            if (obj is ProductoDulceria productoSeleccionado)
            {
                var editarProductoDulceria = new EditarDetallesProductoModeloVista(_mainWindowModeloVista, productoSeleccionado);
                _mainWindowModeloVista.CambiarModeloVista(editarProductoDulceria);
            }
        }

        private void InicializarListaProductos()
        {
            try
            {
                var productos = _dulceriaServicioCliente.ObtenerProductosDulceria();
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
                            CantidadInventario = producto.CantidadInventario.ToString(),
                            Imagen = producto.Imagen,
                            IdSucursal = producto.IdSucursal
                        });
                    }
                }
            }
            catch (Exception)
            {
                Notificacion.MostrarExcepcion();
            }
        }

    }
}
