using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class CantidadMermaModeloVista : BaseModeloVista
    {
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeAceptarOperacion = Visibility.Collapsed;
        public ICommand CancelarComando { get; set; }
        public ICommand ConfirmarComando { get; set; }
        public ICommand CancelarCancelacionComando { get; set; }
        public ICommand ConfirmarCancelacionComando { get; set; }
        public ICommand ConfirmarConfirmacionComando { get; set; }
        public ICommand CancelarConfirmacionComando { get; set; }
        public string Nombre { get; set; }
        public string CantidadInventario { get; set; }
        public string CostoUnitario { get; set; }
        public string PrecioVentaUnitario { get; set; }
        public string CantidadMerma { get; set; }

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

        public Visibility MostrarMensajeAceptarOperacion
        {
            get { return _mostrarMensajeAceptarOperacion; }
            set
            {
                _mostrarMensajeAceptarOperacion = value;
                OnPropertyChanged();
            }
        }

        public CantidadMermaModeloVista(MainWindowModeloVista mainWindowModeloVista, ProductoDulceria producto)
        { 
            _mainWindowModeloVista = mainWindowModeloVista;
            Nombre = producto.Nombre;
            CantidadInventario = producto.CantidadInventario;
            CostoUnitario = producto.CostoUnitario;
            PrecioVentaUnitario = producto.PrecioVentaUnitario;
            CancelarComando = new ComandoModeloVista(Cancelar);
            ConfirmarComando = new ComandoModeloVista(Confirmar);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            ConfirmarConfirmacionComando = new ComandoModeloVista(ConfirmarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);
        }

        public void Cancelar(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        public void Confirmar(object obj)
        {
            MostrarMensajeAceptarOperacion = Visibility.Visible;
        }

        public void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        public void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
        }

        public void ConfirmarConfirmacion(object obj)
        {
            MostrarMensajeAceptarOperacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
            Notificacion.Mostrar("Se ha eliminado el producto correctamente");
        }

        public void CancelarConfirmacion(object obj)
        {
            MostrarMensajeAceptarOperacion = Visibility.Collapsed;
        }
    }
}
