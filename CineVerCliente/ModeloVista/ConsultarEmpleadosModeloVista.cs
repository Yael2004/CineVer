using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.Vista;
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
    public class ConsultarEmpleadosModeloVista : BaseModeloVista
    {
        private string _textoBusqueda;
        private string _textoInhabilitar;
        private string _textoDetallesEmpleado;

        private Visibility _sinResultados = Visibility.Collapsed;
        private Visibility _mostrarEmpleados = Visibility.Visible;
        private Visibility _mostrarMensajeInhabilitar = Visibility.Collapsed;
        private Visibility _mostrarDetallesEmpleado = Visibility.Collapsed;

        private ObservableCollection<EmpleadoConsultado> _todosLosElementos;
        private ObservableCollection<EmpleadoConsultado> _elementosFiltrados;

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

        public string TextoDetallesEmpleado
        {
            get { return _textoDetallesEmpleado; }
            set
            {
                _textoDetallesEmpleado = value;
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

        public Visibility MostrarEmpleados
        {
            get { return _mostrarEmpleados; }
            set
            {
                _mostrarEmpleados = value;
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

        public Visibility MostrarDetallesEmpleado
        {
            get { return _mostrarDetallesEmpleado; }
            set
            {
                _mostrarDetallesEmpleado = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmpleadoConsultado> TodosLosElementos
        {
            get { return _todosLosElementos; }
            set
            {
                _todosLosElementos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmpleadoConsultado> ElementosFiltrados
        {
            get { return _elementosFiltrados; }
            set
            {
                _elementosFiltrados = value;
                OnPropertyChanged();
            }
        }

        public ConsultarEmpleadosModeloVista(MainWindowModeloVista mainWindowModeloVista)
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
            _todosLosElementos = new ObservableCollection<EmpleadoConsultado>
            {
                new EmpleadoConsultado { Nombre = "Gabriel", Apellidos = "Armas Viveros", Matricula = "CNVX2985637", Foto = foto},
                new EmpleadoConsultado { Nombre = "Yael Alfredo", Apellidos = "Salazar Aguilar", Matricula = "CNVX2876509", Foto = foto},
                new EmpleadoConsultado { Nombre = "Daniela", Apellidos = "Luna Landa", Matricula = "CNVX2946527", Foto = foto},
                new EmpleadoConsultado { Nombre = "Maria Antonieta", Apellidos = "Hernandez Torres", Matricula = "CNVX2746527", Foto = foto},
                new EmpleadoConsultado { Nombre = "Sofia", Apellidos = "Suarez Juan", Matricula = "CNVX2746527", Foto = foto},
            };

            ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosElementos);
        }

        private void VerDetalles(object obj)
        {
            if(obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un empleado", 4000);
            }
            else
            {
                TextoDetallesEmpleado = "Nombre: " + ((EmpleadoConsultado)obj).Nombre + "\nApellidos: " + ((EmpleadoConsultado)obj).Apellidos 
                    + "\nMatricula: " + ((EmpleadoConsultado)obj).Matricula;
                MostrarDetallesEmpleado = Visibility.Visible;
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
                Notificacion.Mostrar("No se ha seleccionado un empleado", 4000);
            }
            else
            {
                string mensaje = "¿Está seguro de que desea inhabilitar la cuenta del empleado " +
                    ((EmpleadoConsultado)obj).Nombre + " " + ((EmpleadoConsultado)obj).Apellidos + "?";
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
            MostrarDetallesEmpleado = Visibility.Collapsed;
        }

        private void FiltrarElementos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosElementos);
                if (MostrarEmpleados == Visibility.Collapsed)
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarEmpleados = Visibility.Visible;
                }
            }
            else if (TextoBusqueda.Length < 4)
            {
                var filtrados = _todosLosElementos
                    .Where(empleado => empleado.Nombre.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Matricula.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(filtrados);

                if (ElementosFiltrados.Count == 0)
                {
                    MostrarEmpleados = Visibility.Collapsed;
                    SinResultados = Visibility.Visible;
                }
                else
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarEmpleados = Visibility.Visible;
                }
            }
            else
            {
                var filtrados = _todosLosElementos
                    .Where(empleado => empleado.Nombre.ToLower().Contains(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Matricula.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(filtrados);

                if (ElementosFiltrados.Count == 0)
                {
                    MostrarEmpleados = Visibility.Collapsed;
                    SinResultados = Visibility.Visible;
                }
                else
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarEmpleados = Visibility.Visible;
                }
            }
        }
    }
}
