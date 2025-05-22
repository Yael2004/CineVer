using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.SocioServicio;
using CineVerCliente.Modelo;

namespace CineVerCliente.ModeloVista
{
    public class ModificarSocioModeloVista : BaseModeloVista
    {
        private string _nombres;
        private string _apellidos;
        private DateTime _fechaNacimiento;
        private ObservableCollection<string> listaSexos = new ObservableCollection<string>();
        private string _sexo;
        private string _numeroTelefono;
        private string _correoElectronico;
        private string _calle;
        private string _numeroCasa;
        private string _codigoPostal;

        private Visibility _nombreCampoVacio;
        private Visibility _apellidosCampoVacio;
        private Visibility _fechaNacimientoCampoVacio;
        private Visibility _sexoCampoVacio;
        private Visibility _numeroTelefonoCampoVacio;
        private Visibility _correoElectronicoCampoVacio;
        private Visibility _calleCampoVacio;
        private Visibility _numeroCasaCampoVacio;
        private Visibility _codigoPostalCampoVacio;

        private Visibility _mostrarMensajeConfirmacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeCancelacion = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand ModificarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand AceptarConfirmarcionComando { get; }
        public ICommand CancelarConfirmacionComando { get; }
        public ICommand AceptarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public string Nombres
        {
            get { return _nombres; }
            set
            {
                _nombres = value;
                OnPropertyChanged();
            }
        }

        public string Apellidos
        {
            get { return _apellidos; }
            set
            {
                _apellidos = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set
            {
                _fechaNacimiento = value;
                OnPropertyChanged();
            }
        }

        public string Sexo
        {
            get { return _sexo; }
            set
            {
                _sexo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ListaSexos
        {
            get { return listaSexos; }
            set
            {
                listaSexos = value;
                OnPropertyChanged();
            }
        }

        public string NumeroTelefono
        {
            get { return _numeroTelefono; }
            set
            {
                _numeroTelefono = value;
                OnPropertyChanged();
            }
        }

        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set
            {
                _correoElectronico = value;
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

        public string NumeroCasa
        {
            get { return _numeroCasa; }
            set
            {
                _numeroCasa = value;
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

        public Visibility NombreCampoVacio
        {
            get { return _nombreCampoVacio; }
            set
            {
                _nombreCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility ApellidosCampoVacio
        {
            get { return _apellidosCampoVacio; }
            set
            {
                _apellidosCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility FechaNacimientoCampoVacio
        {
            get { return _fechaNacimientoCampoVacio; }
            set
            {
                _fechaNacimientoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility SexoCampoVacio
        {
            get { return _sexoCampoVacio; }
            set
            {
                _sexoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility NumeroTelefonoCampoVacio
        {
            get { return _numeroTelefonoCampoVacio; }
            set
            {
                _numeroTelefonoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CorreoElectronicoCampoVacio
        {
            get { return _correoElectronicoCampoVacio; }
            set
            {
                _correoElectronicoCampoVacio = value;
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

        public Visibility NumeroCasaCampoVacio
        {
            get { return _numeroCasaCampoVacio; }
            set
            {
                _numeroCasaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CodigoPostalCampoVacio
        {
            get { return _codigoPostalCampoVacio; }
            set
            {
                _codigoPostalCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmacion
        {
            get { return _mostrarMensajeConfirmacion; }
            set
            {
                _mostrarMensajeConfirmacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeCancelacion
        {
            get { return _mostrarMensajeCancelacion; }
            set
            {
                _mostrarMensajeCancelacion = value;
                OnPropertyChanged();
            }
        }

        public ModificarSocioModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            listaSexos.Add("Masculino");
            listaSexos.Add("Femenino");
            ModificarComando = new ComandoModeloVista(Registrar);
            CancelarComando = new ComandoModeloVista(Cancelar);
            AceptarConfirmarcionComando = new ComandoModeloVista(AceptarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);
            AceptarCancelacionComando = new ComandoModeloVista(AceptarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);

            OcultarCampos();
        }

        public void CargarSocio(SocioConsultado socio)
        {
            Nombres = socio.Nombres;
            Apellidos = socio.Apellidos;
            FechaNacimiento = socio.FechaNacimiento;
            Sexo = socio.Sexo;
            NumeroTelefono = socio.NumeroTelefono;
            CorreoElectronico = socio.Correo;
            Calle = socio.Calle;
            NumeroCasa = socio.NumeroCasa;
            CodigoPostal = socio.CodigoPostal;
            OcultarCampos();
        }

        private void Registrar(object obj)
        {
            if (ValidarCampos())
            {
                MostrarMensajeConfirmacion = Visibility.Visible;
            }
        }

        private void Cancelar(object obj)
        {
            MostrarMensajeCancelacion = Visibility.Visible;
        }

        private void AceptarConfirmacion(object obj)
        {
            var cliente = new SocioServicioClient();

            try
            {
                if (Sexo == "Masculino")
                {
                    _sexo = "M";
                }
                else if (Sexo == "Femenino")
                {
                    _sexo = "F";
                }

                var socio = new SocioDTO
                {
                    Nombres = _nombres,
                    Apellidos = _apellidos,
                    FechaNacimiento = _fechaNacimiento,
                    Sexo = _sexo,
                    NumeroTelefono = _numeroTelefono,
                    Correo = _correoElectronico,
                    Calle = _calle,
                    NumeroCasa = _numeroCasa,
                    CodigoPostal = _codigoPostal,
                    Afiliado = true
                };

                var respuesta = cliente.ModificarSocio(socio);

                if (respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Socio modificado con éxito");
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                    _mainWindowModeloVista.CambiarModeloVista(new ConsultarSociosModeloVista(_mainWindowModeloVista));
                }
                else
                {
                    Notificacion.Mostrar("Error al modificar al socio");
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                Notificacion.MostrarExcepcion();
                MostrarMensajeConfirmacion = Visibility.Collapsed;
            }
        }

        private void CancelarConfirmacion(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }

        private void AceptarCancelacion(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new ConsultarSociosModeloVista(_mainWindowModeloVista));
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelacion = Visibility.Collapsed;
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarNombres();
            valido &= ValidarApellidos();
            valido &= ValidarFechaNacimiento();
            valido &= ValidarSexo();
            valido &= ValidarNumeroTelefono();
            valido &= ValidarCorreoElectronico();
            valido &= ValidarCalle();
            valido &= ValidarNumeroCasa();
            valido &= ValidarCodigoPostal();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarNombres()
        {
            if (string.IsNullOrEmpty(Nombres))
            {
                NombreCampoVacio = Visibility.Visible;
                return false;
            }

            NombreCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarApellidos()
        {
            if (string.IsNullOrEmpty(Apellidos))
            {
                ApellidosCampoVacio = Visibility.Visible;
                return false;
            }

            ApellidosCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarFechaNacimiento()
        {
            if (FechaNacimiento == null)
            {
                FechaNacimientoCampoVacio = Visibility.Visible;
                return false;
            }

            FechaNacimientoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarSexo()
        {
            if (string.IsNullOrEmpty(Sexo))
            {
                SexoCampoVacio = Visibility.Visible;
                return false;
            }

            SexoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarNumeroTelefono()
        {
            if (string.IsNullOrEmpty(NumeroTelefono))
            {
                NumeroTelefonoCampoVacio = Visibility.Visible;
                return false;
            }

            NumeroTelefonoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCorreoElectronico()
        {
            if (string.IsNullOrEmpty(CorreoElectronico))
            {
                CorreoElectronicoCampoVacio = Visibility.Visible;
                return false;
            }

            CorreoElectronicoCampoVacio = Visibility.Collapsed;
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

        private bool ValidarNumeroCasa()
        {
            if (string.IsNullOrEmpty(NumeroCasa))
            {
                NumeroCasaCampoVacio = Visibility.Visible;
                return false;
            }

            NumeroCasaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCodigoPostal()
        {
            if (string.IsNullOrEmpty(CodigoPostal))
            {
                CodigoPostalCampoVacio = Visibility.Visible;
                return false;
            }

            CodigoPostalCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCampos()
        {
            NombreCampoVacio = Visibility.Collapsed;
            ApellidosCampoVacio = Visibility.Collapsed;
            FechaNacimientoCampoVacio = Visibility.Collapsed;
            SexoCampoVacio = Visibility.Collapsed;
            NumeroTelefonoCampoVacio = Visibility.Collapsed;
            CorreoElectronicoCampoVacio = Visibility.Collapsed;
            CalleCampoVacio = Visibility.Collapsed;
            NumeroCasaCampoVacio = Visibility.Collapsed;
            CodigoPostalCampoVacio = Visibility.Collapsed;
        }
    }
}
