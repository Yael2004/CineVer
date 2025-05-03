using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.Modelo;
using CineVerCliente.DulceriaServicio;

namespace CineVerCliente.ModeloVista
{
    public class EditarDetallesProductoModeloVista : BaseModeloVista
    {
        private int _idProducto;
        private string _nombreProducto;
        private string _cantidadInventario;
        private string _costoUnitario;
        private string _precioVentaUnitario;
        private byte[] _imagenProducto;
        private int _idSucursal;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarProducto = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient _dulceriaServicioCliente;

        public ICommand ConfirmarCambiosComando { get; }
        public ICommand ConfirmarConfirmacionComando { get; } 
        public ICommand CancelarNuevoProductoComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public int IdProducto
        {
            get { return _idProducto; }
            set
            {
                _idProducto = value;
                OnPropertyChanged();
            }
        }

        public string NombreProducto
        {
            get { return _nombreProducto; }
            set
            {
                _nombreProducto = value;
                OnPropertyChanged();
            }
        }

        public string CantidadInventario
        {
            get { return _cantidadInventario; }
            set
            {
                _cantidadInventario = value;
                OnPropertyChanged();
            }
        }

        public string CostoUnitario
        {
            get { return _costoUnitario; }
            set
            {
                _costoUnitario = value;
                OnPropertyChanged();
            }
        }

        public string PrecioVentaUnitario
        {
            get { return _precioVentaUnitario; }
            set
            {
                _precioVentaUnitario = value;
                OnPropertyChanged();
            }
        }

        public byte[] ImagenProducto
        {
            get { return _imagenProducto; }
            set
            {
                _imagenProducto = value;
                OnPropertyChanged();
            }
        }

        public int IdSucursal
        {
            get { return _idSucursal; }
            set
            {
                _idSucursal = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmarProducto
        {
            get { return _mostrarMensajeConfirmarProducto; }
            set
            {
                _mostrarMensajeConfirmarProducto = value;
                OnPropertyChanged();
            }
        }

        public EditarDetallesProductoModeloVista(MainWindowModeloVista mainWindowModeloVista, ProductoDulceria producto)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            IdProducto = producto.Id;
            NombreProducto = producto.Nombre;
            CantidadInventario = producto.CantidadInventario;
            CostoUnitario = producto.CostoUnitario;
            PrecioVentaUnitario = producto.PrecioVentaUnitario;
            ImagenProducto = producto.Imagen;
            IdSucursal = producto.IdSucursal;
            _dulceriaServicioCliente = new DulceriaServicioClient();
            ConfirmarCambiosComando = new ComandoModeloVista(ConfirmarCambios);
            ConfirmarConfirmacionComando = new ComandoModeloVista(ConfirmarConfirmacion);
            CancelarNuevoProductoComando = new ComandoModeloVista(CancelarNuevoProducto);
            CancelarOperacionComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
        }

        private void ConfirmarCambios(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Visible;
        }

        private void ConfirmarConfirmacion(object obj)
        {
            try
            {
                var respuesta = _dulceriaServicioCliente.ActualizarProductoDulceria(new ProductoDulceriaDTO
                {
                    IdProducto = IdProducto,
                    Nombre = NombreProducto,
                    CostoUnitario = decimal.Parse(CostoUnitario),
                    PrecioVentaUnitario = decimal.Parse(PrecioVentaUnitario),
                    CantidadInventario = int.Parse(CantidadInventario),
                    Imagen = ImagenProducto,
                    IdSucursal = IdSucursal
                });

                if (respuesta.EsExitoso)
                {
                    MostrarMensajeConfirmarProducto = Visibility.Collapsed;
                    Notificacion.Mostrar("Pago realizado correctamente");
                    _mainWindowModeloVista.CambiarModeloVista(new EditarProductoDulceriaModeloVista(_mainWindowModeloVista));
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
        }

        private void CancelarNuevoProducto(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
        }

        private void CancelarOperacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        private void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new EditarProductoDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }
    }
}
