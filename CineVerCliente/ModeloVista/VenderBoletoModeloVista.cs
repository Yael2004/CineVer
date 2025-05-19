using CineVerCliente.FuncionServicio;
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
    public class VenderBoletoModeloVista : BaseModeloVista
    {
        public int Filas { get; } = 5;
        public int Columnas { get; } = 9;


        private string _nombrePelicula;
        private DateTime _fechaFuncion;
        private int _numeroSala;
        private string _hora;
        private string _numeroCuenta;
        private string _mensajeTotalPagar;
        private Byte[] _poster;
        private decimal _precio;

        private Visibility _mostrarMensajeAgregarSocio;
        private Visibility _mostrarNumeroCampoVacio;
        private Visibility _mostrarCuentaNoExiste;
        private Visibility _mostrarMensajeTotalPagar;

        private ObservableCollection<string> _letrasColumnas;
        private ObservableCollection<Modelo.Asiento> _asientos;
        public ObservableCollection<ObservableCollection<Modelo.Asiento>> AsientosAgrupados { get; set; }

        private MainWindowModeloVista _mainWindowModeloVista;

        public ICommand ElegirAsientoComando { get; }
        public ICommand AceptarAsientosComando { get; }
        public ICommand RegresarComando { get; }
        public ICommand AceptarCuentaComando { get; }
        public ICommand ComprarComoInvitadoComando { get; }
        public ICommand ProcederPagoComando { get; }
        public ICommand CancelarPagoComando { get; }

        public string NombrePelicula
        {
            get { return _nombrePelicula; }
            set
            {
                _nombrePelicula = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaFuncion
        {
            get { return _fechaFuncion; }
            set
            {
                _fechaFuncion = value;
                OnPropertyChanged();
            }
        }

        public int NumeroSala
        {
            get { return _numeroSala; }
            set
            {
                _numeroSala = value;
                OnPropertyChanged();
            }
        }

        public string Hora
        {
            get { return _hora; }
            set
            {
                _hora = value;
                OnPropertyChanged();
            }
        }

        public string NumeroCuenta
        {
            get { return _numeroCuenta; }
            set
            {
                _numeroCuenta = value;
                OnPropertyChanged();
            }
        }

        public Byte[] Poster
        {
            get { return _poster; }
            set
            {
                _poster = value;
                OnPropertyChanged();
            }
        }

        public string MensajeTotalPagar
        {
            get { return _mensajeTotalPagar; }
            set
            {
                _mensajeTotalPagar = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeAgregarSocio
        {
            get { return _mostrarMensajeAgregarSocio; }
            set
            {
                _mostrarMensajeAgregarSocio = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarNumeroCampoVacio
        {
            get { return _mostrarNumeroCampoVacio; }
            set
            {
                _mostrarNumeroCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarCuentaNoExiste
        {
            get { return _mostrarCuentaNoExiste; }
            set
            {
                _mostrarCuentaNoExiste = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeTotalPagar
        {
            get { return _mostrarMensajeTotalPagar; }
            set
            {
                _mostrarMensajeTotalPagar = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Modelo.Asiento> Asientos
        {
            get { return _asientos; }
            set
            {
                _asientos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> LetrasColumnas
        {
            get { return _letrasColumnas; }
            set
            {
                _letrasColumnas = value;
                OnPropertyChanged();
            }
        }

        public VenderBoletoModeloVista(MainWindowModeloVista mainWindowModeloVista, Pelicula pelicula, Funcion funcion)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            ElegirAsientoComando = new ComandoModeloVista(CambiarEstado);
            AceptarAsientosComando = new ComandoModeloVista(AceptarAsientos);
            AceptarCuentaComando = new ComandoModeloVista(AceptarCuenta);
            ComprarComoInvitadoComando = new ComandoModeloVista(ComprarComoInvitado);
            ProcederPagoComando = new ComandoModeloVista(ProcederPago);
            CancelarPagoComando = new ComandoModeloVista(CancelarPago);

            Asientos = new ObservableCollection<Modelo.Asiento>();
            AsientosAgrupados = new ObservableCollection<ObservableCollection<Modelo.Asiento>>();

            MostrarMensajeAgregarSocio = Visibility.Collapsed;
            MostrarNumeroCampoVacio = Visibility.Collapsed;
            MostrarCuentaNoExiste = Visibility.Collapsed;
            MostrarMensajeTotalPagar = Visibility.Collapsed;

            _precio = funcion.Precio;

            CargarDatos(pelicula, funcion);
            CargarFilas(funcion.IdSala);

            _mainWindowModeloVista = mainWindowModeloVista;
        }

        private void CargarFilas(int idSala)
        {
            try {
                var cliente = new SucursalServicio.SucursalServicioClient();
                var resultado = cliente.ObtenerAsientosPorFila(idSala);

                Asientos.Clear();

                int maxColumnas = 0;

                foreach (var fila in resultado.Filas)
                {
                    var filaAsientos = new ObservableCollection<Modelo.Asiento>();
                    int idFila = fila.NumeroFila;
                    int numeroAsientos = fila.CantidadAsientos;

                    if (numeroAsientos > maxColumnas)
                    {
                        maxColumnas = numeroAsientos;
                    }

                    foreach (var asientoDTO in fila.Asientos)
                    {
                        var asiento = new Modelo.Asiento
                        {
                            IdAsiento = asientoDTO.idAsiento,
                            IdFila = idFila,
                            LetraColumna = asientoDTO.letraColumna,
                            Estado = (EstadoAsiento)Enum.Parse(typeof(EstadoAsiento), asientoDTO.estado)
                        };

                        filaAsientos.Add(asiento);
                        Asientos.Add(asiento);
                    }

                    AsientosAgrupados.Add(filaAsientos);
                }

                LetrasColumnas = new ObservableCollection<string>();
                for (int j = 0; j < maxColumnas; j++)
                {
                    LetrasColumnas.Add(((char)('A' + j)).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Notificacion.MostrarExcepcion();
            }
        }           


        private void CambiarEstado(object obj)
        {
            if (obj is Modelo.Asiento asiento)
            {
                switch (asiento.Estado)
                {
                    case EstadoAsiento.DISPONIBLE:
                        asiento.Estado = EstadoAsiento.SELECCIONADO;
                        break;
                    case EstadoAsiento.SELECCIONADO:
                        asiento.Estado = EstadoAsiento.DISPONIBLE;
                        break;
                }
            }
        }   

        private void AceptarAsientos(object obj)
        {
            MostrarMensajeAgregarSocio = Visibility.Visible;
        }

        private void AceptarCuenta(object obj)
        {
            if (string.IsNullOrEmpty(NumeroCuenta))
            {
                MostrarNumeroCampoVacio = Visibility.Visible;
                return;
            }

            MostrarNumeroCampoVacio = Visibility.Collapsed;

            try
            {
                var clienteSocio = new SocioServicio.SocioServicioClient();
                var resultadoSocio = clienteSocio.ExisteSocio(NumeroCuenta);

                if (!resultadoSocio.EsExitoso)
                {
                    MostrarCuentaNoExiste = Visibility.Visible;
                }
                else
                {
                    MostrarMensajeAgregarSocio = Visibility.Collapsed;
                    MostrarNumeroCampoVacio = Visibility.Collapsed;
                    MostrarCuentaNoExiste = Visibility.Collapsed;

                    CalcularPrecio();
                    MostrarMensajeTotalPagar = Visibility.Visible;
                }
            
            } catch (Exception ex) {
                Notificacion.MostrarExcepcion();
            }
        }

        private void CalcularPrecio()
        {
            int cantidad = ContarAsientosSeleccionados();
            decimal precio = _precio;
            decimal total = cantidad * precio;
            MostrarMensajeTotalPagar = Visibility.Visible;
            MensajeTotalPagar = $"Total a pagar: {total:C}";
        }

        private void CargarDatos(Pelicula pelicula, Funcion funcion)
        {
            NombrePelicula = pelicula.Nombre;
            FechaFuncion = funcion.Fecha;
            Hora = funcion.HoraInicio.ToString(@"hh\:mm");
            Poster = pelicula.Poster;
        }

        private void ComprarComoInvitado(object obj)
        {
            MostrarMensajeAgregarSocio = Visibility.Collapsed;
            MostrarNumeroCampoVacio = Visibility.Collapsed;
            MostrarCuentaNoExiste = Visibility.Collapsed;
            CalcularPrecio();
            MostrarMensajeTotalPagar = Visibility.Visible;
        }

        private int ContarAsientosSeleccionados()
        {
            return AsientosAgrupados
                .SelectMany(fila => fila)
                .Count(a => a.Estado == EstadoAsiento.SELECCIONADO);
        }

        private void ProcederPago(object obj)
        {
            // Aquí puedes implementar la lógica para proceder con el pago
            // Por ejemplo, abrir una ventana de pago o realizar una transacción
        }

        private void CancelarPago(object obj)
        {
            MostrarMensajeTotalPagar = Visibility.Collapsed;
        }
    }
}
