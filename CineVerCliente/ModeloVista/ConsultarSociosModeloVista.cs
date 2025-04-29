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
            byte[] foto = new byte[0];
            byte[] byteItems = new byte[] { 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10 };
            _todosLosElementos = new ObservableCollection<SocioConsultado>
            {
                new SocioConsultado { Nombre = "Gabriel", Apellidos = "Armas Viveros", Folio = "SCNVX298563", Foto = foto, PuntosSocio = 20},
                new SocioConsultado { Nombre = "Yael Alfredo", Apellidos = "Salazar Aguilar", Folio = "SCNVX287650", Foto = foto, PuntosSocio = 109},
                new SocioConsultado { Nombre = "Daniela", Apellidos = "Luna Landa", Folio = "SCNVX294652", Foto = foto, PuntosSocio = 67},
                new SocioConsultado { Nombre = "Maria Antonieta", Apellidos = "Hernandez Torres", Folio = "SCNVX274652", Foto = foto, PuntosSocio = 12},
                new SocioConsultado { Nombre = "Sofia", Apellidos = "Suarez Juan", Folio = "SCNVX274652", Foto = foto, PuntosSocio = 87},
            };

            ElementosFiltrados = new ObservableCollection<SocioConsultado>(_todosLosElementos);
        }

        private void VerDetalles(object obj)
        {
            if (obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un socio", 4000);
            }
            else
            {
                TextoDetallesSocio = "Nombre: " + ((SocioConsultado)obj).Nombre + "\nApellidos: " + ((SocioConsultado)obj).Apellidos
                    + "\nFolio de socio: " + ((SocioConsultado)obj).Folio + "\nPuntos de socio: " + ((SocioConsultado)obj).PuntosSocio;
                MostrarDetallesSocio = Visibility.Visible;
            }
        }

        private void Editar(object obj)
        {
            //Preguntar a Yael
            //_mainWindowModeloVista.CambiarModeloVista(new ModificarEmpleadoModeloVista());
        }

        private void InhabilitarCuenta(object obj)
        {
            if (obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un socio", 4000);
            }
            else
            {
                string mensaje = "¿Está seguro de que desea inhabilitar la cuenta del socio " +
                    ((SocioConsultado)obj).Nombre + " " + ((SocioConsultado)obj).Apellidos + "?";
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
                    .Where(empleado => empleado.Nombre.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
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
                    .Where(empleado => empleado.Nombre.ToLower().Contains(TextoBusqueda.ToLower()) ||
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
