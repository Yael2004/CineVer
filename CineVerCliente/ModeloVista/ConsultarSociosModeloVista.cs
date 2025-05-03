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

namespace CineVerCliente.ModeloVista
{
    class ConsultarSociosModeloVista : BaseModeloVista
    {
        private string _textoBusqueda;
        private string _textoInhabilitar;
        private string _textoDetallesSocio;

        private Visibility _sinResultados = Visibility.Collapsed;
        private Visibility _mostrarSocios = Visibility.Visible;
        private Visibility _mostrarMensajeInhabilitar = Visibility.Collapsed;
        private Visibility _mostrarDetallesSocio = Visibility.Collapsed;

        private ObservableCollection<SocioConsultado> _todosLosElementos;
        private ObservableCollection<SocioConsultado> _elementosFiltrados;

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

        public ObservableCollection<SocioConsultado> TodosLosElementos
        {
            get { return _todosLosElementos; }
            set
            {
                _todosLosElementos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SocioConsultado> ElementosFiltrados
        {
            get { return _elementosFiltrados; }
            set
            {
                _elementosFiltrados = value;
                OnPropertyChanged();
            }
        }

        public ConsultarSociosModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            VerDetallesComando = new ComandoModeloVista(VerDetalles);
            EditarComando = new ComandoModeloVista(Editar);
            InhabilitarCuentaComando = new ComandoModeloVista(InhabilitarCuenta);
            AceptarInhabilitarComando = new ComandoModeloVista(AceptarInhabilitar);
            CancelarInhabilitarComando = new ComandoModeloVista(CancelarInhabilitar);
            CerrarDetallesComando = new ComandoModeloVista(CerrarDetalles);
            _todosLosElementos = new ObservableCollection<SocioConsultado>
            {
                new SocioConsultado
                {
                    Nombres = "Gabriel",
                    Apellidos = "Armas Viveros",
                    Folio = "SCNVX298563",
                    FechaNacimiento = new DateTime(1995, 4, 12),
                    Sexo = "Masculino",
                    NumeroTelefono = "5551234567",
                    Correo = "gabriel.armas@example.com",
                    Calle = "Av. Reforma",
                    NumeroCasa = "123",
                    CodigoPostal = "06000",
                    PuntosSocio = 20
                },
                new SocioConsultado
                {
                    Nombres = "Yael Alfredo",
                    Apellidos = "Salazar Aguilar",
                    Folio = "SCNVX287650",
                    FechaNacimiento = new DateTime(1992, 7, 30),
                    Sexo = "Masculino",
                    NumeroTelefono = "5512345678",
                    Correo = "yael.salazar@example.com",
                    Calle = "Calle Hidalgo",
                    NumeroCasa = "45-B",
                    CodigoPostal = "06100",
                    PuntosSocio = 109
                },
                new SocioConsultado
                {
                    Nombres = "Daniela",
                    Apellidos = "Luna Landa",
                    Folio = "SCNVX294652",
                    FechaNacimiento = new DateTime(1998, 3, 15),
                    Sexo = "Femenino",
                    NumeroTelefono = "5523456789",
                    Correo = "daniela.luna@example.com",
                    Calle = "Insurgentes Sur",
                    NumeroCasa = "789",
                    CodigoPostal = "06700",
                    PuntosSocio = 67
                },
                new SocioConsultado
                {
                    Nombres = "Maria Antonieta",
                    Apellidos = "Hernandez Torres",
                    Folio = "SCNVX274652",
                    FechaNacimiento = new DateTime(1985, 11, 5),
                    Sexo = "Femenino",
                    NumeroTelefono = "5545678910",
                    Correo = "maria.hernandez@example.com",
                    Calle = "Av. Juárez",
                    NumeroCasa = "321",
                    CodigoPostal = "06600",
                    PuntosSocio = 12
                },
                new SocioConsultado
                {
                    Nombres = "Sofia",
                    Apellidos = "Suarez Juan",
                    Folio = "SCNVX274652",
                    FechaNacimiento = new DateTime(1990, 9, 22),
                    Sexo = "Femenino",
                    NumeroTelefono = "5567890123",
                    Correo = "sofia.suarez@example.com",
                    Calle = "Calle Morelos",
                    NumeroCasa = "56",
                    CodigoPostal = "06800",
                    PuntosSocio = 87
                }
            };

            ElementosFiltrados = new ObservableCollection<SocioConsultado>(_todosLosElementos);
        }

        private async void CargarSocios()
        {
            //var socio = 
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
            _mainWindowModeloVista.CambiarModeloVista(new ModificarSocioModeloVista(_mainWindowModeloVista));
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
            Notificacion.Mostrar("Cuenta inhabilitada exitosamente", 4000);
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
                ElementosFiltrados = new ObservableCollection<SocioConsultado>(_todosLosElementos);
                if (MostrarSocios == Visibility.Collapsed)
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarSocios = Visibility.Visible;
                }
            }
            else if (TextoBusqueda.Length < 4)
            {
                var filtrados = _todosLosElementos
                    .Where(empleado => empleado.Nombres.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Folio.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                ElementosFiltrados = new ObservableCollection<SocioConsultado>(filtrados);

                if (ElementosFiltrados.Count == 0)
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
                var filtrados = _todosLosElementos
                    .Where(empleado => empleado.Nombres.ToLower().Contains(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Folio.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                ElementosFiltrados = new ObservableCollection<SocioConsultado>(filtrados);

                if (ElementosFiltrados.Count == 0)
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
