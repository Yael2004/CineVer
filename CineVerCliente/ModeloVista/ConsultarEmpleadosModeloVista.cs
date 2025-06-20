﻿using CineVerCliente.Helpers;
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
        private EmpleadoServicio.EmpleadoServicioClient cliente;

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
            cliente = new EmpleadoServicio.EmpleadoServicioClient();
            _todosLosEmpleados = new ObservableCollection<EmpleadoConsultado>();
            CargarEmpleados();
            VerDetallesComando = new ComandoModeloVista(VerDetalles);
            EditarComando = new ComandoModeloVista(Editar);
            InhabilitarCuentaComando = new ComandoModeloVista(InhabilitarCuenta);
            AceptarInhabilitarComando = new ComandoModeloVista(AceptarInhabilitar);
            CancelarInhabilitarComando = new ComandoModeloVista(CancelarInhabilitar);
            CerrarDetallesComando = new ComandoModeloVista(CerrarDetalles);
        }

        private void VerDetalles(object obj)
        {
            if(obj == null)
            {
                Notificacion.Mostrar("No se ha seleccionado un empleado");
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
            try
            {
                var respuesta = await cliente.ObtenerEmpleadosAsync();

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
                        FechaNacimiento = empleado.FechaNacimiento,
                        Foto = empleado.Foto
                    });
                }

                EmpleadosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosEmpleados);
            }
            catch (Exception)
            {
                Notificacion.MostrarExcepcion();
            }
            finally
            {
                if (TodosLosEmpleados.Count == 0)
                {
                    SinResultados = Visibility.Visible;
                    MostrarEmpleados = Visibility.Collapsed;
                }
                else
                {
                    SinResultados = Visibility.Collapsed;
                    MostrarEmpleados = Visibility.Visible;
                }
            }
        }

        private void Editar(object obj)
        {
            if (obj is EmpleadoConsultado empleado)
            {
                var modificarEmpleado = new ModificarEmpleadoModeloVista(_mainWindowModeloVista);
                modificarEmpleado.CargarEmpleado(empleado);
                _mainWindowModeloVista.CambiarModeloVista(modificarEmpleado);
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
            var empleado = (EmpleadoConsultado)obj;

            try
            {
                var respuesta = cliente.InhabilitarEmpleado(empleado.IdEmpleado);

                if (respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Cuenta inhabilitada exitosamente");
                    CargarEmpleados();
                    MostrarMensajeInhabilitar = Visibility.Collapsed;
                }
                else
                {
                    Notificacion.Mostrar("Error al inhabilitar la cuenta");
                    MostrarMensajeInhabilitar = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                Notificacion.MostrarExcepcion(); 
                MostrarMensajeInhabilitar = Visibility.Collapsed;
            }
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
