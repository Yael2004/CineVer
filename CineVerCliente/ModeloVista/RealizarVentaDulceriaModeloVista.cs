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
    public class RealizarVentaDulceriaModeloVista : BaseModeloVista
    {
        public ObservableCollection<ProductoDulceria> Productos { get; set; }
        private string _promocion;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarCambio = Visibility.Collapsed;
        private Visibility _mostrarVentanaVenta = Visibility.Collapsed;
        private double _totalAPagar;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient DulceriaServicioCliente;

        public ICommand RealizarVentaComando { get; }
        public ICommand ConfirmarVentaComando { get; }
        public ICommand CancelarVentaComando { get; }

        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public ICommand AceptarVentanaVentaComando { get; }
        public ICommand CancelarVentanaVentaComando { get; }
        public ICommand CrearCuentaComando { get; }

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

        public Visibility MostrarVentanaVenta
        {
            get { return _mostrarVentanaVenta; }
            set
            {
                _mostrarVentanaVenta = value;
                OnPropertyChanged(nameof(MostrarVentanaVenta));
            }
        }

        public string Promocion
        {
            get { return _promocion; }
            set
            {
                _promocion = value;
                OnPropertyChanged(nameof(Promocion));
            }
        }

        public RealizarVentaDulceriaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            DulceriaServicioCliente = new DulceriaServicioClient();
            Productos = new ObservableCollection<ProductoDulceria>();
            Productos.CollectionChanged += (s, e) =>
            {
                foreach (var item in Productos)
                {
                    item.PropertyChanged += (s2, e2) =>
                    {
                        if (e2.PropertyName == nameof(ProductoDulceria.TotalProducto))
                        {
                            RecalcularTotal();
                        }
                    };
                }
            };
            RealizarVentaComando = new ComandoModeloVista(RealizarVenta);
            ConfirmarVentaComando = new ComandoModeloVista(ConfirmarVenta);
            CancelarVentaComando = new ComandoModeloVista(CancelarVenta);
            CancelarComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            AceptarVentanaVentaComando = new ComandoModeloVista(AceptarVentanaVenta);
            CancelarVentanaVentaComando = new ComandoModeloVista(CancelarVentanaVenta);
            CrearCuentaComando = new ComandoModeloVista(CrearCuenta);
            InicializarListaProductos();
        }

        private void RecalcularTotal()
        {
            TotalAPagar = Productos.Sum(p => p.TotalProducto);
        }

        private void InicializarListaProductos()
        {
            try
            {
                var productos = DulceriaServicioCliente.ObtenerProductosDulceria();
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
                            CantidadInventario = producto.CantidadInventario.ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }

        public double TotalAPagar
        {
            get { return _totalAPagar; }
            set
            {
                _totalAPagar = value;
                OnPropertyChanged(nameof(TotalAPagar));
            }
        }

        public void RealizarVenta(object obj)
        {
            MostrarVentanaVenta = Visibility.Visible;
        }

        private void ConfirmarVenta(object obj)
        {
            Dictionary<int, int> productosVendidos = new Dictionary<int, int>();

            foreach (var producto in Productos)
            {
                if (producto.CantidadAVender > 0 && int.TryParse(producto.CantidadInventario, out int inventarioActual))
                {
                    if (producto.CantidadAVender <= inventarioActual)
                    {
                        productosVendidos.Add(producto.Id, producto.CantidadAVender);
                    }
                }

            }

            /*
            try
            {
                var respuesta = DulceriaServicioCliente.RealizarVenta(productosVendidos);
                if (respuesta != null && respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Venta realizada correctamente");
                }
                else
                {
                    Notificacion.Mostrar("No se pudo completar la venta");
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Error al realizar la venta");
            }
            */

            Notificacion.Mostrar("Venta realizada correctamente");
        }


        public void CancelarVenta(object obj)
        {
            MostrarVentanaVenta = Visibility.Collapsed;
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

        public void AceptarVentanaVenta(object obj)
        {
            MostrarVentanaVenta = Visibility.Collapsed;
        }

        public void CancelarVentanaVenta(object obj)
        {
            MostrarVentanaVenta = Visibility.Collapsed;
        }

        private void CrearCuenta(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new RegistrarSocioModeloVista(_mainWindowModeloVista));
        }
    }
}
