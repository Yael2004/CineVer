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
            InicializarListaProductos();
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
            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
            Notificacion.Mostrar("Cantidad registrada correctamente en el inventario");
        }

        private void CancelarCambio(object obj)
        {
            MostrarMensajeConfirmarCambio = Visibility.Collapsed;
        }

        private void InicializarListaProductos()
        {
            Productos = new ObservableCollection<ProductoDulceria>
            {
                new ProductoDulceria
                {
                    Nombre = "Palomitas",
                    CostoUnitario = "20",
                    PrecioVentaUnitario = "35",
                    CantidadInventario = "15"
                },
                new ProductoDulceria
                {
                    Nombre = "Refresco",
                    CostoUnitario = "10",
                    PrecioVentaUnitario = "25",
                    CantidadInventario = "20"
                },
                new ProductoDulceria
                {
                    Nombre = "Dulces",
                    CostoUnitario = "5",
                    PrecioVentaUnitario = "15",
                    CantidadInventario = "30"
                },
            };
        }

        private void MostrarMensajeServicio()
        {
            try
            {
                var cliente = new DulceriaServicio.DulceriaServicioClient();
                var mensaje = cliente.AgregarProductoDulceria();
                Notificacion.Mostrar(mensaje);
            }
            catch (Exception ex)
            {
                Notificacion.Mostrar(ex.Message);
            }
        }
    }
}
