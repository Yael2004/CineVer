using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.Modelo;

namespace CineVerCliente.ModeloVista
{
    public class EditarDetallesProductoModeloVista : BaseModeloVista
    {
        private string _nombreProducto;
        private string _cantidadInventario;
        private string _costoUnitario;
        private string _precioVentaUnitario;
        private byte[] _imagenProducto;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarProducto = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand ConfirmarCambiosComando { get; }
        public ICommand ConfirmarConfirmacionComando { get; } 
        public ICommand CancelarNuevoProductoComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

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
            NombreProducto = producto.Nombre;
            CantidadInventario = producto.CantidadInventario;
            CostoUnitario = producto.CostoUnitario;
            PrecioVentaUnitario = producto.PrecioVentaUnitario;
            ImagenProducto = producto.Imagen;
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
            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
            Notificacion.Mostrar("Pago realizado correctamente");
            _mainWindowModeloVista.CambiarModeloVista(new EditarProductoDulceriaModeloVista(_mainWindowModeloVista));
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
