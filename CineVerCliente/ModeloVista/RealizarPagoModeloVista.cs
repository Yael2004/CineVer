using CineVerCliente.AsientoServicio;
using CineVerCliente.CuentaFidelidadServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.SocioServicio;
using CineVerCliente.VentaServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class RealizarPagoModeloVista : BaseModeloVista
    {
        private string _cantidadAPagar;
        private string _nombrePromocion;
        private string _puntosAUtilizar;
        private string _puntosRestantes;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarProducto = Visibility.Collapsed;
        private Visibility mostrarTerminal = Visibility.Collapsed;
        Dictionary<int, int> ProductosVendidos = new Dictionary<int, int>();
        private SocioDTO Socio;
        private CuentaFidelidadServicioClient CuentaFidelidadServicioCliente = new CuentaFidelidadServicioClient();
        private VentaServicioClient VentaServicioCliente = new VentaServicioClient();
        private double _puntosOriginales;
        private double _totalOriginal;
        private string _tipoVenta;
        private List<int> AsientosIds { get; set; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;


        public RealizarPagoModeloVista(MainWindowModeloVista mainWindowModeloVista, Dictionary<int, int> productosVendidos, string promocion, double totalAPagar, SocioDTO socio, string tipoVenta)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            AplicarPuntosComando = new ComandoModeloVista(AplicarPuntos);
            CancelarOperacionComando = new ComandoModeloVista(MostrarConfirmacionCancelar);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            AceptarNuevoProductoComando = new ComandoModeloVista(AceptarPago);
            CancelarNuevoProductoComando = new ComandoModeloVista(CancelarPago);
            ConfirmarPagoConTarjetaComando = new ComandoModeloVista(ConfirmarPagoConTarjeta);
            CancelarPagoConTarjetaComando = new ComandoModeloVista(CancelarPagoConTarjeta);
            TarjetaComando = new ComandoModeloVista(SeleccionarTarjeta);
            EfectivoComando = new ComandoModeloVista(SeleccionarEfectivo);
            NombrePromocion = promocion;
            _totalOriginal = totalAPagar;
            CantidadAPagar = totalAPagar.ToString("F2");
            ProductosVendidos = productosVendidos;
            Socio = socio;
            InicializarPuntos();
            _tipoVenta = tipoVenta;
        }

        public RealizarPagoModeloVista(MainWindowModeloVista mainWindowModeloVista, List<int> asientosIds, string promocion, double totalAPagar, SocioDTO socio, string tipoVenta)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            AplicarPuntosComando = new ComandoModeloVista(AplicarPuntos);
            CancelarOperacionComando = new ComandoModeloVista(MostrarConfirmacionCancelar);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            AceptarNuevoProductoComando = new ComandoModeloVista(AceptarPago);
            CancelarNuevoProductoComando = new ComandoModeloVista(CancelarPago);
            ConfirmarPagoConTarjetaComando = new ComandoModeloVista(ConfirmarPagoConTarjeta);
            CancelarPagoConTarjetaComando = new ComandoModeloVista(CancelarPagoConTarjeta);
            TarjetaComando = new ComandoModeloVista(SeleccionarTarjeta);
            EfectivoComando = new ComandoModeloVista(SeleccionarEfectivo);
            NombrePromocion = promocion;
            _totalOriginal = totalAPagar;
            CantidadAPagar = totalAPagar.ToString("F2");
            Socio = socio;
            InicializarPuntos();
            _tipoVenta = tipoVenta;
            AsientosIds = asientosIds;
        }

        public void InicializarPuntos()
        {
            if (Socio.IdSocio > 0)
            {
                try
                {
                    var puntos = CuentaFidelidadServicioCliente.ObtenerCuentaFidelidadPorIdSocio(Socio.IdSocio);
                    if (puntos != null && puntos.ResultDTO.EsExitoso)
                    {
                        _puntosOriginales = puntos.cuenta.Puntos;
                        PuntosRestantes = _puntosOriginales.ToString();
                    }
                    else
                    {
                        PuntosAUtilizar = "0";
                        PuntosRestantes = "0";
                    }
                }
                catch (Exception)
                {
                    Notificacion.Mostrar("Ha ocurrido un error inesperado");
                    PuntosAUtilizar = "0";
                    PuntosRestantes = "0";
                }
            }
            else
            {
                PuntosRestantes = "0";
            }
        }

        public string CantidadAPagar
        {
            get => _cantidadAPagar;
            set
            {
                _cantidadAPagar = value;
                OnPropertyChanged();
            }
        }

        public string NombrePromocion
        {
            get => _nombrePromocion;
            set
            {
                _nombrePromocion = value;
                OnPropertyChanged();
            }
        }

        public string PuntosAUtilizar
        {
            get => _puntosAUtilizar;
            set
            {
                _puntosAUtilizar = value;
                OnPropertyChanged();
            }
        }

        public string PuntosRestantes
        {
            get => _puntosRestantes;
            set
            {
                _puntosRestantes = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get => _mostrarMensajeCancelarOperacion;
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmarProducto
        {
            get => _mostrarMensajeConfirmarProducto;
            set
            {
                _mostrarMensajeConfirmarProducto = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarTerminal
        {
            get => mostrarTerminal;
            set
            {
                mostrarTerminal = value;
                OnPropertyChanged();
            }
        }

        public ICommand AplicarPuntosComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand AceptarNuevoProductoComando { get; }
        public ICommand CancelarNuevoProductoComando { get; }
        public ICommand TarjetaComando { get; }
        public ICommand EfectivoComando { get; }
        public ICommand ConfirmarPagoConTarjetaComando { get; }
        public ICommand CancelarPagoConTarjetaComando { get; }

        private void AplicarPuntos(object obj)
        {
            try
            {
                if (!double.TryParse(PuntosAUtilizar, out double puntosUsados))
                {
                    Notificacion.Mostrar("Cantidad de puntos inválida.");
                    return;
                }

                if (!double.TryParse(CantidadAPagar, out double totalActual))
                {
                    Notificacion.Mostrar("Cantidad a pagar no válida.");
                    return;
                }

                if (puntosUsados < 0)
                {
                    Notificacion.Mostrar("No puedes aplicar puntos negativos.");
                    return;
                }

                if (puntosUsados > _puntosOriginales)
                {
                    Notificacion.Mostrar("No tienes suficientes puntos.");
                    return;
                }

                if (puntosUsados > totalActual)
                {
                    Notificacion.Mostrar("No puedes usar más puntos de lo que cuesta el total.");
                    return;
                }

                double nuevoTotal = _totalOriginal - puntosUsados;
                CantidadAPagar = nuevoTotal.ToString("F2");
                PuntosRestantes = (_puntosOriginales - puntosUsados).ToString("F2");

                Notificacion.Mostrar("Puntos aplicados correctamente");
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ocurrió un error al aplicar los puntos");
            }
        }


        private void MostrarConfirmacionCancelar(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        private void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
            //_mainWindowModeloVista.RegresarModeloVistaAnterior();
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        private async void AceptarPago(object obj)
        {
            try
            {
                if (Socio.IdSocio > 0)
                {
                    var venta = new VentaDTO
                    {
                        IdEmpleado = 1, 
                        IdSocio = Socio.IdSocio, 
                        IdSucursal = 2,    
                        Total = decimal.Parse(CantidadAPagar),
                        MetodoPago = "Efectivo",    
                        TIpoVenta = _tipoVenta
                    };

                    if (_tipoVenta == "Dulcería")
                    {
                        var resultado = await VentaServicioCliente.RealizarPagoDulceriaAsync(venta, ProductosVendidos);
                        if (resultado != null && resultado.EsExitoso)
                        {
                            Notificacion.Mostrar("Pago realizado con éxito");
                        }
                        else
                        {
                            Notificacion.Mostrar("Ha ocurrido un error inesperado");
                        }
                    }
                    else if (_tipoVenta == "Taquilla")
                    {
                        var resultado = await VentaServicioCliente.RealizarPagoBoletosAsync(venta, AsientosIds.ToArray());
                        if (resultado != null && resultado.EsExitoso)
                        {
                            Notificacion.Mostrar("Pago realizado con éxito");
                        }
                        else
                        {
                            Notificacion.Mostrar("Ha ocurrido un error inesperado");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error al realizar el pago");
            }

            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
        }


        private void CancelarPago(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
        }

        private void SeleccionarTarjeta(object obj)
        {
            MostrarTerminal = Visibility.Visible;
        }

        private void SeleccionarEfectivo(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Visible;
        }

        public async void ConfirmarPagoConTarjeta(object obj)
        {
            try
            {
                var venta = new VentaDTO
                {
                    IdEmpleado = 1,
                    IdSocio = Socio.IdSocio,
                    IdSucursal = 2,
                    Total = decimal.Parse(CantidadAPagar),
                    MetodoPago = "Tarjeta",
                    TIpoVenta = _tipoVenta,
                };

                if (_tipoVenta == "Dulcería")
                {
                    var resultado = await VentaServicioCliente.RealizarPagoDulceriaAsync(venta, ProductosVendidos);
                    if (resultado != null && resultado.EsExitoso)
                    {
                        Notificacion.Mostrar("Pago realizado con éxito");
                    }
                    else
                    {
                        Notificacion.Mostrar("Ha ocurrido un error inesperado");
                    }
                }
                else if (_tipoVenta == "Taquilla")
                {
                    var resultado = await VentaServicioCliente.RealizarPagoBoletosAsync(venta, AsientosIds.ToArray());
                    if (resultado != null && resultado.EsExitoso)
                    {
                        Notificacion.Mostrar("Pago realizado con éxito");
                    }
                    else
                    {
                        Notificacion.Mostrar("Ha ocurrido un error inesperado");
                    }
                }
            }
            
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error al realizar el pago");
            }

            MostrarTerminal = Visibility.Collapsed;
        }

        public void CancelarPagoConTarjeta(object obj)
        {
            MostrarTerminal = Visibility.Collapsed;
        }
    }
}
