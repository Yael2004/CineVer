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

    }
}
