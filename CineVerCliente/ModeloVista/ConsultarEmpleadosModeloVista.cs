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

        private ObservableCollection<EmpleadoConsultado> _todosLosEmpleados;
        private ObservableCollection<EmpleadoConsultado> _empleadosFiltrados;

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

        public ObservableCollection<EmpleadoConsultado> TodosLosEmpleados
        {
            get { return _todosLosEmpleados; }
            set
            {
                _todosLosEmpleados = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmpleadoConsultado> EmpleadosFiltrados
        {
            get { return _empleadosFiltrados; }
            set
            {
                _empleadosFiltrados = value;
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
            _todosLosEmpleados = new ObservableCollection<EmpleadoConsultado>
            {
                new EmpleadoConsultado
                {
                    Nombres = "Gabriel",
                    Apellidos = "Armas Viveros",
                    Matricula = "CNVX2985637",
                    Rol = "Gerente",
                    Sexo = "Masculino",
                    Correo = "gabriel.armas@example.com",
                    NumeroTelefono = "5551234567",
                    Calle = "Av. Reforma",
                    NumeroCasa = "120",
                    CodigoPostal = "01234",
                    RFC = "GAVG800101HDF",
                    Nss = "12345678901",
                    FechaNacimiento = new DateTime(1980, 5, 14),
                    Foto = foto
                },
                new EmpleadoConsultado
                {
                    Nombres = "Yael Alfredo",
                    Apellidos = "Salazar Aguilar",
                    Matricula = "CNVX2876509",
                    Rol = "Gerente",
                    Sexo = "Masculino",
                    Correo = "yael.salazar@example.com",
                    NumeroTelefono = "5559876543",
                    Calle = "Calle Morelos",
                    NumeroCasa = "55",
                    CodigoPostal = "06700",
                    RFC = "YASA900202MDF",
                    Nss = "10987654321",
                    FechaNacimiento = new DateTime(1990, 2, 2),
                    Foto = foto
                },
                new EmpleadoConsultado
                {
                    Nombres = "Daniela",
                    Apellidos = "Luna Landa",
                    Matricula = "CNVX2946527",
                    Rol = "Empleado administrativo",
                    Sexo = "Femenino",
                    Correo = "daniela.luna@example.com",
                    NumeroTelefono = "5556543210",
                    Calle = "Insurgentes Sur",
                    NumeroCasa = "201",
                    CodigoPostal = "03100",
                    RFC = "DALL920303MDF",
                    Nss = "11223344556",
                    FechaNacimiento = new DateTime(1992, 3, 3),
                    Foto = foto
                },
                new EmpleadoConsultado
                {
                    Nombres = "Maria Antonieta",
                    Apellidos = "Hernandez Torres",
                    Matricula = "CNVX2746527",
                    Rol = "Empleado administrativo",
                    Sexo = "Femenino",
                    Correo = "maria.hernandez@example.com",
                    NumeroTelefono = "5553219876",
                    Calle = "Av. Juárez",
                    NumeroCasa = "77",
                    CodigoPostal = "06000",
                    RFC = "MAHT850707MDF",
                    Nss = "22334455667",
                    FechaNacimiento = new DateTime(1985, 7, 7),
                    Foto = foto
                },
                new EmpleadoConsultado
                {
                    Nombres = "Sofia",
                    Apellidos = "Suarez Juan",
                    Matricula = "CNVX2746527",
                    Rol = "Empleado operativo",
                    Sexo = "Femenino",
                    Correo = "sofia.suarez@example.com",
                    NumeroTelefono = "5557654321",
                    Calle = "Av. Universidad",
                    NumeroCasa = "88",
                    CodigoPostal = "04510",
                    RFC = "SOSJ950505MDF",
                    Nss = "33445566778",
                    FechaNacimiento = new DateTime(1995, 5, 5),
                    Foto = foto
                }
            };

            EmpleadosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosEmpleados);
        }

        private void VerDetalles(object obj)
        {
            if(obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un empleado", 4000);
            }
            else
            {
                EmpleadoConsultado empleado = (EmpleadoConsultado)obj;
                TextoDetallesEmpleado = string.Format("Nombre(s): {0}\nApellidos: {1}\nMatrícula: {2}\nRol: {3}\nSexo: {4}\nCorreo electónico: {5}" +
                    "\nNúmero telefónico: {6}\nCalle: {7}\nNúmero de casa: {8}\nCódigo postal: {9}\nRFC: {10}\nNSS: {11}\nFecha de nacimiento: {12}",
                    empleado.Nombres,
                    empleado.Apellidos,
                    empleado.Matricula,
                    empleado.Rol,
                    empleado.Sexo,
                    empleado.Correo,
                    empleado.NumeroTelefono,
                    empleado.Calle,
                    empleado.NumeroCasa,
                    empleado.CodigoPostal,
                    empleado.RFC,
                    empleado.Nss,
                    empleado.FechaNacimiento.ToShortDateString());
                MostrarDetallesEmpleado = Visibility.Visible;
            }
        }

        private async void CargarEmpleados()
        {
            var cliente = new EmpleadoServicio.EmpleadoServicioClient();
            var respuesta = await cliente.ObtenerEmpleadosAsync();

            TodosLosEmpleados = new ObservableCollection<EmpleadoConsultado>();
            foreach (var empleado in respuesta.Empleados)
            {
                TodosLosEmpleados.Add(new EmpleadoConsultado
                {
                    IdEmpleado = empleado.IdEmpleado,
                    Nombres = empleado.Nombres,
                    Apellidos = empleado.Apellidos,
                    Matricula = empleado.Matricula,
                    Rol = empleado.Rol,
                    Sexo = empleado.Sexo,
                    Correo = empleado.Correo,
                    NumeroTelefono = empleado.NumeroTelefono,
                    Calle = empleado.Calle,
                    NumeroCasa = empleado.NumeroCasa,
                    CodigoPostal = empleado.CodigoPostal,
                    RFC = empleado.RFC,
                    Nss = empleado.Nss,
                    FechaNacimiento = empleado.FechaNacimiento
                });
            }
        }

        private void Editar(object obj)
        {
            if(obj is EmpleadoConsultado empleadoConsultado)
            {
                _mainWindowModeloVista.CambiarModeloVista(new ModificarEmpleadoModeloVista(_mainWindowModeloVista));
            }
        }

        private void InhabilitarCuenta(object obj)
        {
            if (obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un empleado", 4000);
            }
            else
            {
                EmpleadoConsultado empleado = (EmpleadoConsultado)obj;
                string mensaje = "¿Está seguro de que desea inhabilitar la cuenta del empleado " +
                empleado.Nombres + " " + empleado.Apellidos + "?";
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
                EmpleadosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosEmpleados);
                if (MostrarEmpleados == Visibility.Collapsed)
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarEmpleados = Visibility.Visible;
                }
            }
            else if (TextoBusqueda.Length < 4)
            {
                var filtrados = _todosLosEmpleados
                    .Where(empleado => empleado.Nombres.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Matricula.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                EmpleadosFiltrados = new ObservableCollection<EmpleadoConsultado>(filtrados);

                if (EmpleadosFiltrados.Count == 0)
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
                var filtrados = _todosLosEmpleados
                    .Where(empleado => empleado.Nombres.ToLower().Contains(TextoBusqueda.ToLower()) ||
                        empleado.Apellidos.ToLower().StartsWith(TextoBusqueda.ToLower()) ||
                        empleado.Matricula.ToLower().StartsWith(TextoBusqueda.ToLower())).ToList();

                EmpleadosFiltrados = new ObservableCollection<EmpleadoConsultado>(filtrados);

                if (EmpleadosFiltrados.Count == 0)
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
