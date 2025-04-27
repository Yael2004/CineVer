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
        public ObservableCollection<ProductoDulceria> Productos { get; set; }
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
