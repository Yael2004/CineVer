using CineVerCliente.CuentaFidelidadServicio;
using CineVerCliente.Helpers;
using CineVerCliente.SocioServicio;
using System;
using System.Collections.Generic;
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
        Dictionary<int, int> productosVendidos = new Dictionary<int, int>();
        private SocioDTO Socio;
        CuentaFidelidadServicioClient CuentaFidelidadServicioCliente = new CuentaFidelidadServicioClient();
        private double _puntosOriginales;

        private readonly MainWindowModeloVista _mainWindowModeloVista;


        public RealizarPagoModeloVista(MainWindowModeloVista mainWindowModeloVista, Dictionary<int, int> productosVendidos, string promocion, double totalAPagar, SocioDTO socio)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            AplicarPuntosComando = new ComandoModeloVista(AplicarPuntos);
            CancelarOperacionComando = new ComandoModeloVista(MostrarConfirmacionCancelar);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            AceptarNuevoProductoComando = new ComandoModeloVista(AceptarPago);
            CancelarNuevoProductoComando = new ComandoModeloVista(CancelarPago);
            TarjetaComando = new ComandoModeloVista(SeleccionarTarjeta);
            EfectivoComando = new ComandoModeloVista(SeleccionarEfectivo);
            NombrePromocion = promocion;
            CantidadAPagar = totalAPagar.ToString("F2");
            Socio = socio;
            InicializarPuntos();
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

        public ICommand AplicarPuntosComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand AceptarNuevoProductoComando { get; }
        public ICommand CancelarNuevoProductoComando { get; }
        public ICommand TarjetaComando { get; }
        public ICommand EfectivoComando { get; }

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

                double nuevoTotal = totalActual - puntosUsados;
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

        private void AceptarPago(object obj)
        {
            try
            {
                Notificacion.Mostrar("Pago realizado con éxito");
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
            MostrarMensajeConfirmarProducto = Visibility.Visible;
        }

        private void SeleccionarEfectivo(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Visible;
        }
    }
}
