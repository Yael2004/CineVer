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
    public class AgregarPromocionModeloVista : BaseModeloVista
    {
        private string _nombre { get; set; }
        private int _productosNecesarios { get; set; }
        private int _productosAPagar { get; set; }
        private List<string> _tipoPromocion { get; set; }
        private List<string> _producto { get; set; }
        private string _tipoPromocionSeleccionado { get; set; }
        private string _productoSeleccionado { get; set; }
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _MostrarMensajeConfirmarPromocion = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand AceptarPromocionComando { get; }
        public ICommand ConfirmarAceptarPromocionComando { get; }
        public ICommand CancelarAceptarPromocionComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        public int ProductosNecesarios
        {
            get { return _productosNecesarios; }
            set
            {
                _productosNecesarios = value;
                OnPropertyChanged();
            }
        }

        public int ProductosAPagar
        {
            get { return _productosAPagar; }
            set
            {
                _productosAPagar = value;
                OnPropertyChanged();
            }
        }

        public List<string> TipoPromocion
        {
            get { return _tipoPromocion; }
            set
            {
                _tipoPromocion = value;
                OnPropertyChanged();
            }
        }

        public List<string> Producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
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

        public Visibility MostrarMensajeConfirmarPromocion
        {
            get { return _MostrarMensajeConfirmarPromocion; }
            set
            {
                _MostrarMensajeConfirmarPromocion = value;
                OnPropertyChanged();
            }
        }

        public string TipoPromocionSeleccionado
        {
            get { return _tipoPromocionSeleccionado; }
            set
            {
                _tipoPromocionSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public string ProductoSeleccionado
        {
            get { return _productoSeleccionado; }
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public AgregarPromocionModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            AceptarPromocionComando = new ComandoModeloVista(AceptarPromocion);
            ConfirmarAceptarPromocionComando = new ComandoModeloVista(ConfirmarAceptarPromocion);
            CancelarAceptarPromocionComando = new ComandoModeloVista(CancelarAceptarPromocion);
            CancelarOperacionComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            TipoPromocion = new List<string> { "Taquilla", "Dulcería" };
        }

        private void AceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Visible;
        }

        private void ConfirmarAceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Collapsed;
            Notificacion.Mostrar("Se ha registrado la promoción correctamente");
        }

        private void CancelarAceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Collapsed;
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
    }
}
