using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class ReportarMermaProductoModeloVista : BaseModeloVista
    {
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        public ObservableCollection<ProductoDulceria> Productos { get; set; } = new ObservableCollection<ProductoDulceria>();
        private DulceriaServicioClient _dulceriaServicioCliente;
        public ICommand CancelarComando { get; set; }
        public ICommand ConfirmarCancelacionComando { get; set; }
        public ICommand CancelarCancelacionComando { get; set; }
        public ICommand SeleccionarProductoComando { get; }


        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged();
            }
        }

        public ReportarMermaProductoModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            _dulceriaServicioCliente = new DulceriaServicioClient();
            InicializarListaProductos();
            CancelarComando = new ComandoModeloVista(Cancelar);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            SeleccionarProductoComando = new ComandoModeloVista(SeleccionarProducto);
        }

        public void Cancelar(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        public void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        public void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        public void SeleccionarProducto(object obj)
        {
            if (obj is ProductoDulceria productoSeleccionado)
            {
                CantidadMermaModeloVista cantidadMermaModeloVista = new CantidadMermaModeloVista(_mainWindowModeloVista, productoSeleccionado);
                _mainWindowModeloVista.CambiarModeloVista(cantidadMermaModeloVista);
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
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }
    }
}
