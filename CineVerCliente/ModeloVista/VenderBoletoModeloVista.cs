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

        private Visibility _mostrarMensajeAgregarSocio;
        private Visibility _mostrarNumeroCampoVacio;
        private Visibility _mostrarCuentaNoExiste;
        private Visibility _mostrarMensajeTotalPagar;

        private ObservableCollection<string> _letrasColumnas;
        private ObservableCollection<Asiento> _asientos;
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

        public ObservableCollection<Asiento> Asientos
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

        public VenderBoletoModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            ElegirAsientoComando = new ComandoModeloVista(CambiarEstado);
            AceptarAsientosComando = new ComandoModeloVista(AceptarAsientos);
            AceptarCuentaComando = new ComandoModeloVista(AceptarCuenta);
            ComprarComoInvitadoComando = new ComandoModeloVista(ComprarComoInvitado);
            ProcederPagoComando = new ComandoModeloVista(ProcederPago);
            CancelarPagoComando = new ComandoModeloVista(CancelarPago);

            Asientos = new ObservableCollection<Asiento>();

            MostrarMensajeAgregarSocio = Visibility.Collapsed;
            MostrarNumeroCampoVacio = Visibility.Collapsed;
            MostrarCuentaNoExiste = Visibility.Collapsed;
            MostrarMensajeTotalPagar = Visibility.Collapsed;

            var filas = new Dictionary<int, int>
            {
                { 0, 6 },
                { 1, 8 },
                { 2, 7 },
                { 3, 5 },
                { 4, 8 }
            };

            foreach (var fila in filas)
            {
                for (int j = 0; j < fila.Value; j++)
                {
                    EstadoAsiento estado = EstadoAsiento.Disponible;

                    if (fila.Key == 0 && j == 1) estado = EstadoAsiento.Mantenimiento;
                    if (fila.Key == 1 && j == 3) estado = EstadoAsiento.Ocupado;

                    Asientos.Add(new Asiento
                    {
                        IdFila = fila.Key,
                        LetraColumna = j,
                        Estado = estado
                    });
                }
            }

            int maxColumnas = filas.Values.Max();

            LetrasColumnas = new ObservableCollection<string>();
            for (int j = 0; j < maxColumnas; j++)
            {
                LetrasColumnas.Add(((char)('A' + j)).ToString());
            }

            _mainWindowModeloVista = mainWindowModeloVista;
        }

        private void CambiarEstado(object obj)
        {
            if (obj is Asiento asiento)
            {
                switch (asiento.Estado)
                {
                    case EstadoAsiento.Disponible:
                        asiento.Estado = EstadoAsiento.Seleccionado;
                        break;
                    case EstadoAsiento.Seleccionado:
                        asiento.Estado = EstadoAsiento.Disponible;
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
        }

        private void CalcularPrecio()
        {
            //Falta ver si hay promociones
            //Falta comprobar el precio de los boletos de la funcion
            int cantidad = ContarAsientosSeleccionados();
            decimal precio = 50;
            decimal total = cantidad * precio;
            MostrarMensajeTotalPagar = Visibility.Visible;
            MensajeTotalPagar = $"Total a pagar: {total:C}";
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
            return Asientos.Count(a => a.Estado == EstadoAsiento.Seleccionado);
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
