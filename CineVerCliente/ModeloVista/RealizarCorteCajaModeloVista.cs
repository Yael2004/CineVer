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
    public class RealizarCorteCajaModeloVista : BaseModeloVista
    {
        private decimal _montoFinalDia;
        private decimal _efectivoEsperado;
        private decimal _efectivoCaja;
        private decimal _diferenciaEfectivo;
        private decimal _ventaBoletos;
        private decimal _ventaDulceria;
        private decimal _ventasTotales;
        private decimal _gastos;
        private decimal _ganancias;
        private decimal _inicioSiguienteDia;

        private string _montoFinalDiaTexto;
        private string _inicioSiguienteDiaTexto;

        private Visibility _verPrimeraVista;
        private Visibility _verSegundaVista;
        private Visibility _mostrarMensajeConfirmar;

        private Visibility _montoFinalDiaCampoVacio;
        private Visibility _inicioSiguienteDiaCampoVacio;

        MainWindowModeloVista _mainWindowModeloVista;

        public ICommand SalirComando { get; }
        public ICommand CalcularCorteComando { get; }
        public ICommand RegresarComando { get; }
        public ICommand AceptarComando { get; }
        public ICommand CancelarComando { get; }

        public string MontoFinalDiaTexto
        {
            get { return _montoFinalDiaTexto; }
            set
            {
                _montoFinalDiaTexto = value;
                decimal.TryParse(_montoFinalDiaTexto, out _montoFinalDia);
                OnPropertyChanged();
            }
        }

        public decimal EfectivoEsperado
        {
            get { return _efectivoEsperado; }
            set
            {
                _efectivoEsperado = value;
                OnPropertyChanged();
            }
        }

        public decimal EfectivoCaja
        {
            get { return _efectivoCaja; }
            set
            {
                _efectivoCaja = value;
                OnPropertyChanged();
            }
        }

        public decimal DiferenciaEfectivo
        {
            get { return _diferenciaEfectivo; }
            set
            {
                _diferenciaEfectivo = value;
                OnPropertyChanged();
            }
        }

        public decimal VentaBoletos
        {
            get { return _ventaBoletos; }
            set
            {
                _ventaBoletos = value;
                OnPropertyChanged();
            }
        }

        public decimal VentaDulceria
        {
            get { return _ventaDulceria; }
            set
            {
                _ventaDulceria = value;
                OnPropertyChanged();
            }
        }

        public decimal VentasTotales
        {
            get { return _ventasTotales; }
            set
            {
                _ventasTotales = value;
                OnPropertyChanged();
            }
        }

        public decimal Gastos
        {
            get { return _gastos; }
            set
            {
                _gastos = value;
                OnPropertyChanged();
            }
        }

        public decimal Ganancias
        {
            get { return _ganancias; }
            set
            {
                _ganancias = value;
                OnPropertyChanged();
            }
        }

        public string InicioSiguienteDiaTexto
        {
            get { return _inicioSiguienteDiaTexto; }
            set
            {
                _inicioSiguienteDiaTexto = value;
                decimal.TryParse(_inicioSiguienteDiaTexto, out _inicioSiguienteDia);
                OnPropertyChanged();
            }
        }

        public Visibility VerPrimeraVista
        {
            get { return _verPrimeraVista; }
            set
            {
                _verPrimeraVista = value;
                OnPropertyChanged();
            }
        }

        public Visibility VerSegundaVista
        {
            get { return _verSegundaVista; }
            set
            {
                _verSegundaVista = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmar
        {
            get { return _mostrarMensajeConfirmar; }
            set
            {
                _mostrarMensajeConfirmar = value;
                OnPropertyChanged();
            }
        }

        public Visibility MontoFinalDiaCampoVacio
        {
            get { return _montoFinalDiaCampoVacio; }
            set
            {
                _montoFinalDiaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility InicioSiguienteDiaCampoVacio
        {
            get { return _inicioSiguienteDiaCampoVacio; }
            set
            {
                _inicioSiguienteDiaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public RealizarCorteCajaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            SalirComando = new ComandoModeloVista(Salir);
            CalcularCorteComando = new ComandoModeloVista(CalcularCorte);
            RegresarComando = new ComandoModeloVista(Regresar);
            AceptarComando = new ComandoModeloVista(Aceptar);
            CancelarComando = new ComandoModeloVista(Cancelar);

            VerPrimeraVista = Visibility.Visible;
            MostrarMensajeConfirmar = Visibility.Collapsed;

            MontoFinalDiaCampoVacio = Visibility.Hidden;
            InicioSiguienteDiaCampoVacio = Visibility.Hidden;
        }

        public void Salir(object obj)
        {
            if (ValidarInicioSiguienteDia())
            {
                try { 
                    var clienteCorteCaja = new CorteCajaServicio.CorteCajaServicioClient();
                    var corteCaja = new CorteCajaServicio.CorteCajaDTO
                    {
                        DiferenciaEfectivo = DiferenciaEfectivo,
                        EfectivoCaja = EfectivoCaja,
                        EfectivoEsperado = EfectivoEsperado,
                        FechaCorte = DateTime.Now,
                        Ganancias = Ganancias,
                        Gastos = Gastos,
                        VentaTotal = VentasTotales,
                        InicioDia = decimal.Parse(InicioSiguienteDiaTexto),
                        IdSucursal = UsuarioEnLinea.Instancia.IdSucursal,
                    };

                    var resultado = clienteCorteCaja.GuardarCorteCaja(corteCaja);

                    if (resultado.EsExitoso)
                    {
                        Notificacion.Mostrar("Corte de caja realizado con éxito");
                    }
                }
                catch (Exception ex)
                {
                    Notificacion.MostrarExcepcion();
                }
            }
        }

        public void CalcularCorte(object obj)
        {
            if (ValidarMontoFinalDia())
            {
                MostrarMensajeConfirmar = Visibility.Visible;
            }
        }

        public void Regresar(object obj)
        {
            VerPrimeraVista = Visibility.Visible;
            VerSegundaVista = Visibility.Collapsed;
        }

        public void Aceptar(object obj)
        {
            VerPrimeraVista = Visibility.Collapsed;
            MostrarMensajeConfirmar = Visibility.Collapsed;
            VerSegundaVista = Visibility.Visible;


            CargarDatos();
        }

        public void Cancelar(object obj)
        {
            MostrarMensajeConfirmar = Visibility.Collapsed;
        }

        private void CargarDatos()
        {
            try
            {
                var clienteVenta = new VentaServicio.VentaServicioClient();
                var clienteGastos = new GastoServicio.GastoServicioClient();
                var clienteCorteCaja = new CorteCajaServicio.CorteCajaServicioClient();

                var resultadoBoletos = clienteVenta.ObtenerVentasDeBoletosDelDia(UsuarioEnLinea.Instancia.IdSucursal);
                var resultadoDulceria = clienteVenta.ObtenerVentasDeDulceriaDelDia(UsuarioEnLinea.Instancia.IdSucursal);
                var resultadoGastos = clienteGastos.ObtenerGastosDelDia(DateTime.Now, UsuarioEnLinea.Instancia.IdSucursal);
                var resultadoEfectivo = clienteVenta.ObtenerVentasEnEfectivoDelDia(UsuarioEnLinea.Instancia.IdSucursal);
                var resultadoCorteCaja = clienteCorteCaja.ObtenerMontoInicioDia(UsuarioEnLinea.Instancia.IdSucursal);
                
                if (resultadoBoletos.ResultDTO.EsExitoso && resultadoDulceria.ResultDTO.EsExitoso)
                {
                    VentaBoletos = resultadoBoletos.Total;
                    VentaDulceria = resultadoDulceria.Total;
                    VentasTotales = resultadoBoletos.Total + resultadoDulceria.Total;
                    EfectivoCaja = decimal.Parse(MontoFinalDiaTexto);
                    Gastos = resultadoGastos.Gastos.Sum(g => g.Monto);

                    if (resultadoCorteCaja.Monto == 0)
                    {
                        EfectivoEsperado = resultadoEfectivo.Total;
                    }
                    else
                    {
                        EfectivoEsperado = resultadoEfectivo.Total + resultadoCorteCaja.Monto;
                    }
                    
                    DiferenciaEfectivo = EfectivoEsperado - EfectivoCaja;

                    Ganancias = VentasTotales - Gastos;
                }   
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }

        }

        private bool ValidarMontoFinalDia()
        {
            if (string.IsNullOrEmpty(MontoFinalDiaTexto))
            {
                MontoFinalDiaCampoVacio = Visibility.Visible;
                return false;
            }

            if (!decimal.TryParse(MontoFinalDiaTexto, out _montoFinalDia))
            {
                return false;
            }

            MontoFinalDiaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarInicioSiguienteDia()
        {
            if (string.IsNullOrEmpty(InicioSiguienteDiaTexto))
            {
                InicioSiguienteDiaCampoVacio = Visibility.Visible;
                return false;
            }

            if (!decimal.TryParse(InicioSiguienteDiaTexto, out _inicioSiguienteDia))
            {
                return false;
            }

            InicioSiguienteDiaCampoVacio = Visibility.Collapsed;
            return true;
        }
    }
}
