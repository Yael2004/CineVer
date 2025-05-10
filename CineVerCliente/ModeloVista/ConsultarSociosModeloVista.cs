using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.CuentaFidelidadServicio;

namespace CineVerCliente.ModeloVista
{
    class ConsultarSociosModeloVista : BaseModeloVista
    {
        private SocioServicio.SocioServicioClient clienteSocio;
        private CuentaFidelidadServicio.CuentaFidelidadServicioClient clienteCuenta;

        private string _textoBusqueda;
        private string _textoInhabilitar;
        private string _textoDetallesSocio;

        private Visibility _sinResultados = Visibility.Collapsed;
        private Visibility _mostrarSocios = Visibility.Visible;
        private Visibility _mostrarMensajeInhabilitar = Visibility.Collapsed;
        private Visibility _mostrarDetallesSocio = Visibility.Collapsed;

        private ObservableCollection<SocioConsultado> _todosLosSocios;
        private ObservableCollection<SocioConsultado> _sociosFiltrados;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand VerDetallesComando { get; }
        public ICommand EditarComando { get; }
        public ICommand InhabilitarCuentaComando { get; }
        public ICommand AceptarInhabilitarComando { get; }
        public ICommand CancelarInhabilitarComando { get; }
        public ICommand CerrarDetallesComando { get; }

        public string TextoBusqueda
        {
            get { return _textoBusqueda; }
            set
            {
                _textoBusqueda = value;
                OnPropertyChanged();
                FiltrarElementos();
            }
        }

        public string TextoInhabilitar
        {
            get { return _textoInhabilitar; }
            set
            {
                _textoInhabilitar = value;
                OnPropertyChanged();
            }
        }

        public string TextoDetallesSocio
        {
            get { return _textoDetallesSocio; }
            set
            {
                _textoDetallesSocio = value;
                OnPropertyChanged();
            }
        }

        public Visibility SinResultados
        {
            get { return _sinResultados; }
            set
            {
                _sinResultados = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarSocios
        {
            get { return _mostrarSocios; }
            set
            {
                _mostrarSocios = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeInhabilitar
        {
            get { return _mostrarMensajeInhabilitar; }
            set
            {
                _mostrarMensajeInhabilitar = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarDetallesSocio
        {
            get { return _mostrarDetallesSocio; }
            set
            {
                _mostrarDetallesSocio = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SocioConsultado> TodosLosSocios
        {
            get { return _todosLosSocios; }
            set
            {
                _todosLosSocios = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SocioConsultado> SociosFiltrados
        {
            get { return _sociosFiltrados; }
            set
            {
                _sociosFiltrados = value;
                OnPropertyChanged();
            }
        }

        public ConsultarSociosModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            clienteSocio = new SocioServicio.SocioServicioClient();
            clienteCuenta = new CuentaFidelidadServicio.CuentaFidelidadServicioClient();
            _todosLosSocios = new ObservableCollection<SocioConsultado>();
            CargarSocios();
            VerDetallesComando = new ComandoModeloVista(VerDetalles);
            EditarComando = new ComandoModeloVista(Editar);
            InhabilitarCuentaComando = new ComandoModeloVista(InhabilitarCuenta);
            AceptarInhabilitarComando = new ComandoModeloVista(AceptarInhabilitar);
            CancelarInhabilitarComando = new ComandoModeloVista(CancelarInhabilitar);
            CerrarDetallesComando = new ComandoModeloVista(CerrarDetalles);
        }

        private async void CargarSocios()
        {
            try
            {
                var respuesta = await clienteSocio.ObtenerSociosAsync();

                TodosLosSocios = new ObservableCollection<SocioConsultado>();

                foreach (var socio in respuesta.Socios)
                {
                    var cuenta = await clienteCuenta.ObtenerCuentaFidelidadPorIdSocioAsync(socio.IdSocio);

                    _todosLosSocios.Add(new SocioConsultado
                    {
                        IdSocio = socio.IdSocio,
                        Nombres = socio.Nombres,
                        Apellidos = socio.Apellidos,
                        Folio = socio.Folio,
                        FechaNacimiento = socio.FechaNacimiento,
                        Sexo = socio.Sexo,
                        NumeroTelefono = socio.NumeroTelefono,
                        Correo = socio.Correo,
                        Calle = socio.Calle,
                        NumeroCasa = socio.NumeroCasa,
                        CodigoPostal = socio.CodigoPostal,
                        PuntosSocio = cuenta.cuenta.Puntos
                    });
                }

                SociosFiltrados = new ObservableCollection<SocioConsultado>(_todosLosSocios);
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Error al cargar los socios", 4000);
            }
            finally
            {
                if (TodosLosSocios.Count == 0)
                {
                    MostrarSocios = Visibility.Collapsed;
                    SinResultados = Visibility.Visible;
                }
                else
                {
                    MostrarSocios = Visibility.Visible;
                    SinResultados = Visibility.Collapsed;
                }
            }
        }

        private void VerDetalles(object obj)
        {
            if (obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un socio", 4000);
            }
            else
            {
                SocioConsultado socio = (SocioConsultado)obj;
                TextoDetallesSocio = string.Format(
                    "Nombre(s): {0}\nApellidos: {1}\nFolio: {2}\nSexo: {3}\nCorreo electrónico: {4}" +
                    "\nNúmero telefónico: {5}\nCalle: {6}\nNúmero de casa: {7}\nCódigo postal: {8}" +
                    "\nFecha de nacimiento: {9}\nPuntos acumulados: {10}",
                    socio.Nombres,
                    socio.Apellidos,
                    socio.Folio,
                    socio.Sexo,
                    socio.Correo,
                    socio.NumeroTelefono,
                    socio.Calle,
                    socio.NumeroCasa,
                    socio.CodigoPostal,
                    socio.FechaNacimiento.ToShortDateString(),
                    socio.PuntosSocio
                );

                MostrarDetallesSocio = Visibility.Visible;
            }
        }

        private void Editar(object obj)
        {
            if (obj is SocioConsultado socio)
            {
                var modificarSocio = new ModificarSocioModeloVista(_mainWindowModeloVista);
                modificarSocio.CargarSocio(socio);
                _mainWindowModeloVista.CambiarModeloVista(modificarSocio);
            }
        }

        private void InhabilitarCuenta(object obj)
        {
            if (obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un socio", 4000);
            }
            else
            {
                SocioConsultado socio = (SocioConsultado)obj;
                string mensaje = "¿Está seguro de que desea inhabilitar la cuenta del socio " +
                socio.Nombres + " " + socio.Apellidos + "?";
                TextoInhabilitar = mensaje;
                MostrarMensajeInhabilitar = Visibility.Visible;
            }
        }

        private void AceptarInhabilitar(object obj)
        {
            try
            {
                var socio = (SocioConsultado)obj;
                int puntos = socio.PuntosSocio;

                var respuestaCuenta = clienteCuenta.InhabilitarCuentaFidelidad(socio.IdSocio);

                if (respuestaCuenta.EsExitoso)
                {
                    var respuestaSocio = clienteSocio.InhabilitarCuentaSocio(socio.IdSocio);

                    if (respuestaSocio.EsExitoso)
                    {
                        Notificacion.Mostrar("Cuenta inhabilitada exitosamente", 4000);
                        CargarSocios();
                        MostrarMensajeInhabilitar = Visibility.Collapsed;
                    }
                    else
                    {
                        var respuestaCuentaNueva = clienteCuenta.RegistrarCuentaFidelidad(new CuentaFidelidadDTO
                        {
                            IdSocio = socio.IdSocio,
                            Puntos = puntos
                        });

                        Notificacion.Mostrar("Error al inhabilitar la cuenta del socio", 4000);
                        MostrarMensajeInhabilitar = Visibility.Collapsed;
                    }
                }
                else
                {
                    Notificacion.Mostrar("Error al inhabilitar la cuenta", 4000);
                    MostrarMensajeInhabilitar = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Error al inhabilitar la cuenta", 4000);
                MostrarMensajeInhabilitar = Visibility.Collapsed;
            }
        }

        private void CancelarInhabilitar(object obj)
        {
            MostrarMensajeInhabilitar = Visibility.Collapsed;
        }

        private void CerrarDetalles(object obj)
        {
            MostrarDetallesSocio = Visibility.Collapsed;
        }

        private void FiltrarElementos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                SociosFiltrados = new ObservableCollection<SocioConsultado>(_todosLosSocios);
                if (MostrarSocios == Visibility.Collapsed)
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarSocios = Visibility.Visible;
                }
            }
            else if (TextoBusqueda.Length < 4)
            {
                var filtrados = _todosLosSocios
                    .Where(socio => socio.Nombres.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        socio.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        socio.Folio.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                SociosFiltrados = new ObservableCollection<SocioConsultado>(filtrados);

                if (SociosFiltrados.Count == 0)
                {
                    MostrarSocios = Visibility.Collapsed;
                    SinResultados = Visibility.Visible;
                }
                else
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarSocios = Visibility.Visible;
                }
            }
            else
            {
                var filtrados = _todosLosSocios
                    .Where(socio => socio.Nombres.ToLower().Contains(TextoBusqueda.ToLower()) ||
                        socio.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        socio.Folio.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                SociosFiltrados = new ObservableCollection<SocioConsultado>(filtrados);

                if (SociosFiltrados.Count == 0)
                {
                    MostrarSocios = Visibility.Collapsed;
                    SinResultados = Visibility.Visible;
                }
                else
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarSocios = Visibility.Visible;
                }
            }
        }
    }
}
