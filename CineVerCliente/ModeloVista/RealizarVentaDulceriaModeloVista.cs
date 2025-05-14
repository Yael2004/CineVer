using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.VentaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        public ObservableCollection<PromocionDTO> Promociones { get; set; }
        private string _promocion;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarCambio = Visibility.Collapsed;
        private Visibility _mostrarVentanaVenta = Visibility.Collapsed;
        private double _totalAPagar;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient DulceriaServicioCliente;
        private VentaServicioClient VentaServicioCliente;

        public ICommand RealizarVentaComando { get; }
        public ICommand ConfirmarVentaComando { get; }
        public ICommand CancelarVentaComando { get; }

        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public ICommand AceptarVentanaVentaComando { get; }
        public ICommand CancelarVentanaVentaComando { get; }
        public ICommand CrearCuentaComando { get; }

        private PromocionDTO _promocionSeleccionada;

        public PromocionDTO PromocionSeleccionada
        {
            get { return _promocionSeleccionada; }
            set
            {
                _promocionSeleccionada = value;
                OnPropertyChanged(nameof(PromocionSeleccionada));
                RecalcularTotal();
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
            VentaServicioCliente = new VentaServicioClient();
            Productos = new ObservableCollection<ProductoDulceria>();
            Promociones = new ObservableCollection<PromocionDTO>();

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
            InicializarListaPromociones();
        }

        private void RecalcularTotal()
        {
            double totalSinDescuento = Productos.Sum(p => p.TotalProducto);
            double totalConDescuento = totalSinDescuento;

            if (PromocionSeleccionada != null)
            {
                if (!EsDiaValidoParaPromocion(PromocionSeleccionada))
                {
                    Notificacion.Mostrar($"La promoción '{PromocionSeleccionada.Nombre}' no aplica hoy.");
                }
                else
                {
                    var productosAplicables = Productos
                        .Where(p => p.Nombre.Equals(PromocionSeleccionada.Producto, StringComparison.OrdinalIgnoreCase)
                                    && p.CantidadAVender > 0)
                        .ToList();

                    int totalCantidad = productosAplicables.Sum(p => p.CantidadAVender);

                    if (totalCantidad >= PromocionSeleccionada.NumeroProductosNecesarios)
                    {
                        int vecesPromocion = 1;

                        double precioUnitario = double.Parse(productosAplicables.First().PrecioVentaUnitario, CultureInfo.InvariantCulture);

                        int productosQueSeCobraran = PromocionSeleccionada.NumeroProductosPagar;
                        int productosQueNoSeCobraran = PromocionSeleccionada.NumeroProductosNecesarios - PromocionSeleccionada.NumeroProductosPagar;

                        double totalConPromocion = (totalCantidad - productosQueNoSeCobraran) * precioUnitario;

                        totalConDescuento = totalConPromocion;

                        Notificacion.Mostrar($"¡Aplicada promoción '{PromocionSeleccionada.Nombre}' 1 vez!");
                    }
                }
            }

            TotalAPagar = totalConDescuento;
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

        private void InicializarListaPromociones()
        {
            try
            {
                var promociones = VentaServicioCliente.ObtenerPromocionesDulceria(2);
                if (promociones != null)
                {
                    foreach (var promocion in promociones.Promociones)
                    {
                        Promociones.Add(new PromocionDTO
                        {
                            IdPromocion = promocion.IdPromocion,
                            Nombre = promocion.Nombre,
                            Tipo = promocion.Tipo,
                            Producto = promocion.Producto,
                            NumeroProductosNecesarios = promocion.NumeroProductosNecesarios,
                            NumeroProductosPagar = promocion.NumeroProductosPagar,
                            LunesAplica = promocion.LunesAplica,
                            MartesAplica = promocion.MartesAplica,
                            MiercolesAplica = promocion.MiercolesAplica,
                            JuevesAplica = promocion.JuevesAplica,
                            ViernesAplica = promocion.ViernesAplica,
                            SabadoAplica = promocion.SabadoAplica,
                            DomingoAplica = promocion.DomingoAplica,
                            IdSucursal = promocion.IdSucursal
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

        private bool EsDiaValidoParaPromocion(PromocionDTO promocion)
        {
            var diaHoy = DateTime.Now.DayOfWeek;

            return (diaHoy == DayOfWeek.Monday && promocion.LunesAplica) ||
                   (diaHoy == DayOfWeek.Tuesday && promocion.MartesAplica) ||
                   (diaHoy == DayOfWeek.Wednesday && promocion.MiercolesAplica) ||
                   (diaHoy == DayOfWeek.Thursday && promocion.JuevesAplica) ||
                   (diaHoy == DayOfWeek.Friday && promocion.ViernesAplica) ||
                   (diaHoy == DayOfWeek.Saturday && promocion.SabadoAplica) ||
                   (diaHoy == DayOfWeek.Sunday && promocion.DomingoAplica);
        }

    }
}
