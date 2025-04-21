using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarProductoDulceriaModeloVista : BaseModeloVista
    {
        private string _nombreProducto;
        private int _cantidadInventario;
        private float _costoUnitario;
        private float _precioVentaUnitario;
        private byte[] _imagenProducto;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarCambio = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand AgregarNuevoProductoComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand MostrarMensajeConfirmarCambiosComando { get; }
        public ICommand ConfirmarCambioComando { get; }
        public ICommand CancelarCambioComando { get; }

        public string NombreProducto
        {
            get { return _nombreProducto; }
            set
            {
                _nombreProducto = value;
                OnPropertyChanged();
            }
        }

        public int CantidadInventario
        {
            get { return _cantidadInventario; }
            set
            {
                _cantidadInventario = value;
                OnPropertyChanged();
            }
        }

        public float CostoUnitario
        {
            get { return _costoUnitario; }
            set
            {
                _costoUnitario = value;
                OnPropertyChanged();
            }
        }

        public float PrecioVentaUnitario
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
        }

        public void ConfirmarInventario(object obj)
        {
            MostrarMensajeConfirmarCambio = Visibility.Visible;
        }

        private void AgregarNuevoProducto(object obj)
        {
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
            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
            Notificacion.Mostrar("Cantidad registrada correctamente en el inventario");
        }

        private void CancelarCambio(object obj)
        {
            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
        }


    }
}
