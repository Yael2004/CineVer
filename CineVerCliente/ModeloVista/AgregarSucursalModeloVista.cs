using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.Vista;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarSucursalModeloVista : BaseModeloVista
    {
        private string _nombreSucursal;
        private string _codigoPostal;
        private string _estado;
        private string _ciudad;
        private string _calle;
        private string _numero;
        private TimeSpan _horaApertura;
        private TimeSpan _horaCierre;

        private Visibility _nombreSucursalCampoVacio;
        private Visibility _cpCampoVacio;
        private Visibility _estadoCampoVacio;
        private Visibility _ciudadCampoVacio;
        private Visibility _calleCampoVacio;
        private Visibility _numeroCampoVacio;
        private Visibility _horaAperturaCampoVacio;
        private Visibility _horaCierreCampoVacio;   
        private Visibility _mostrarMensajeConfirmar;
        private Visibility _verPrimeraVista;
        private Visibility _verSegundaVista;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand SiguienteComando { get; }
        public ICommand SalirComando { get; }
        public ICommand RegresarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand ContinuarComando { get; }
        public ICommand AceptarComando { get; }

        public string NombreSucursal
        {
            get { return _nombreSucursal; }
            set
            {
                _nombreSucursal = value;
                OnPropertyChanged();
            }
        }

        public string CodigoPostal
        {
            get { return _codigoPostal; }
            set
            {
                _codigoPostal = value;
                OnPropertyChanged();
            }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
                OnPropertyChanged();
            }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set
            {
                _ciudad = value;
                OnPropertyChanged();
            }
        }

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged();
            }
        }

        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraApertura
        {
            get { return _horaApertura; }
            set
            {
                _horaApertura = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraCierre
        {
            get { return _horaCierre; }
            set
            {
                _horaCierre = value;
                OnPropertyChanged();
            }
        }

        public Visibility NombreSucursalCampoVacio
        {
            get { return _nombreSucursalCampoVacio; }
            set
            {
                _nombreSucursalCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CPCampoVacio
        {
            get { return _cpCampoVacio; }
            set
            {
                _cpCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility EstadoCampoVacio
        {
            get { return _estadoCampoVacio; }
            set
            {
                _estadoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CiudadCampoVacio
        {
            get { return _ciudadCampoVacio; }
            set
            {
                _ciudadCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CalleCampoVacio
        {
            get { return _calleCampoVacio; }
            set
            {
                _calleCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility NumeroCampoVacio
        {
            get { return _numeroCampoVacio; }
            set
            {
                _numeroCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility HoraAperturaCampoVacio
        {
            get { return _horaAperturaCampoVacio; }
            set 
            {
                _horaAperturaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility HoraCierreCampoVacio
        {
            get { return _horaCierreCampoVacio; }
            set
            {
                _horaCierreCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmar
        {
            get { return _mostrarMensajeConfirmar; }
            set
            {
                _mostrarMensajeConfirmar = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmar));
            }
        }

        public Visibility VerPrimeraVista
        {
            get { return _verPrimeraVista; }
            set
            {
                _verPrimeraVista = value;
                OnPropertyChanged(nameof(VerPrimeraVista));
            }
        }

        public Visibility VerSegundaVista
        {
            get { return _verSegundaVista; }
            set
            {
                _verSegundaVista = value;
                OnPropertyChanged(nameof(VerSegundaVista));
            }
        }

        public AgregarSucursalModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            SiguienteComando = new ComandoModeloVista(Siguiente);
            RegresarComando = new ComandoModeloVista(Regresar);
            ContinuarComando = new ComandoModeloVista(Continuar);
            CancelarComando = new ComandoModeloVista(Cancelar);
            SalirComando = new ComandoModeloVista(Salir);
            AceptarComando = new ComandoModeloVista(GuardarSucursal);

            VerPrimeraVista = Visibility.Visible;
            VerSegundaVista = Visibility.Collapsed;

            OcultarCamposVacios();
        }

        private void Siguiente(object obj)
        {
            if (ValidarCampos())
            {
                MostrarMensajeConfirmar = Visibility.Visible;
            }
        }

        private void GuardarSucursal(object obj)
        {
            try
            {
                var cliente = new SucursalServicio.SucursalServicioClient();
                var sucursal = new SucursalServicio.SucursalDTO
                {
                    Nombre = NombreSucursal,
                    CodigoPostal = CodigoPostal,
                    Estado = Estado,
                    Ciudad = Ciudad,
                    Calle = Calle,
                    NumeroEnLaCalle = Numero,
                    HoraApertura = HoraApertura,
                    HoraCierre = HoraCierre
                };

                var resultado = cliente.GuardarSucursal(sucursal);

                if (resultado.EsExitoso)
                {
                    MostrarMensajeConfirmar = Visibility.Collapsed;
                    Notificacion.Mostrar("Sucursal guardada correctamente");
                    _mainWindowModeloVista.CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
                }
                else
                {
                    MostrarMensajeConfirmar = Visibility.Collapsed;
                    Notificacion.Mostrar("Error al guardar la sucursal");
                }
            }
            catch (Exception ex)
            {
                MostrarMensajeConfirmar = Visibility.Collapsed;
                Notificacion.Mostrar("Error al conectar con el servicio: " + ex.Message);
            }
        }

        private void Regresar(object obj)
        {
            VerPrimeraVista = Visibility.Visible;
            VerSegundaVista = Visibility.Collapsed;
        }

        private void Salir(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
        }

        private void Continuar(object obj)
        {
            MostrarMensajeConfirmar = Visibility.Visible;
        }

        private void Cancelar(object obj)
        {
            MostrarMensajeConfirmar = Visibility.Collapsed;
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarNombre();
            valido &= ValidarCP();
            valido &= ValidarEstado();
            valido &= ValidarCiudad();
            valido &= ValidarCalle();
            valido &= ValidarNumero();
            valido &= ValidarHoraApertura();
            valido &= ValidarHoraCierre();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarNombre()
        {
            if (string.IsNullOrEmpty(NombreSucursal))
            {
                NombreSucursalCampoVacio = Visibility.Visible;
                return false;
            }

            NombreSucursalCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCP()
        {
            if (string.IsNullOrEmpty(CodigoPostal)) {
                CPCampoVacio = Visibility.Visible;
                return false;
            }

            CPCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarEstado()
        {
            if (string.IsNullOrEmpty(Estado))
            {
                EstadoCampoVacio = Visibility.Visible;
                return false;
            }

            EstadoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCiudad()
        {
            if (string.IsNullOrEmpty(Ciudad))
            {
                CiudadCampoVacio = Visibility.Visible;
                return false;
            }

            CiudadCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCalle()
        {
            if (string.IsNullOrEmpty(Calle))
            {
                CalleCampoVacio = Visibility.Visible;
                return false;
            }

            CalleCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarNumero()
        {
            if (string.IsNullOrEmpty(Numero))
            {
                NumeroCampoVacio = Visibility.Visible;
                return false;
            }

            NumeroCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarHoraApertura()
        {
            if (string.IsNullOrEmpty(HoraApertura.ToString()))
            {
                HoraAperturaCampoVacio = Visibility.Visible;
                return false;
            }

            HoraAperturaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarHoraCierre()
        {
            if (string.IsNullOrEmpty(HoraCierre.ToString()))
            {
                HoraCierreCampoVacio = Visibility.Visible;
                return false;
            }

            HoraCierreCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCamposVacios()
        {
            NombreSucursalCampoVacio = Visibility.Collapsed;
            CPCampoVacio = Visibility.Collapsed;
            EstadoCampoVacio = Visibility.Collapsed;
            CiudadCampoVacio = Visibility.Collapsed;
            CalleCampoVacio = Visibility.Collapsed;
            NumeroCampoVacio = Visibility.Collapsed;
            HoraAperturaCampoVacio = Visibility.Collapsed;
            HoraCierreCampoVacio = Visibility.Collapsed;
        }
    }
}
