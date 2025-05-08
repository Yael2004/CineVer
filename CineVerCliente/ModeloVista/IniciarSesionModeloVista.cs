using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Security.Cryptography;
using CineVerCliente.Modelo;

namespace CineVerCliente.ModeloVista
{
    class IniciarSesionModeloVista : BaseModeloVista
    {
        private string _matricula;
        private string _contraseña;

        private Visibility _matriculaCampoVacio;
        private Visibility _contraseñaCampoVacio;
        private Visibility _datosIncorrectos;

        public ICommand IniciarSesionComando { get; }
        public ICommand RegistrarseComando { get; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public string Matricula
        {
            get { return _matricula; }
            set
            {
                _matricula = value;
                OnPropertyChanged();
            }
        }

        public string Contraseña
        {
            get { return _contraseña; }
            set
            {
                _contraseña = value;
                OnPropertyChanged();
            }
        }

        public Visibility MatriculaCampoVacio
        {
            get { return _matriculaCampoVacio; }
            set
            {
                _matriculaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility ContraseñaCampoVacio
        {
            get { return _contraseñaCampoVacio; }
            set
            {
                _contraseñaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility DatosIncorrectos
        {
            get { return _datosIncorrectos; }
            set
            {
                _datosIncorrectos = value;
                OnPropertyChanged();
            }
        }

        public IniciarSesionModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            IniciarSesionComando = new ComandoModeloVista(IniciarSesion);
            RegistrarseComando = new ComandoModeloVista(Registrarse);

            OcultarCampos();
        }

        private string HashContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] contraseñaBytes = Encoding.UTF8.GetBytes(contraseña);

                byte[] hashBytes = sha256.ComputeHash(contraseñaBytes);

                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        public void IniciarSesion(Object obj)
        {
            if (ValidarCampos())
            {
                string hashContrasenia = HashContraseña(Contraseña);

                var cliente = new EmpleadoServicio.EmpleadoServicioClient();

                var empleado = cliente.VerificarInicioSesion(Matricula, hashContrasenia);

                if (empleado.EsExitoso)
                {
                    var empleadoLogueado = cliente.BuscarEmpleadoPorMatricula(Matricula);

                    var empleadoConsultado = new EmpleadoConsultado
                    {
                        IdEmpleado = empleadoLogueado.empleado.IdEmpleado,
                        Nombres = empleadoLogueado.empleado.Nombres,
                        Apellidos = empleadoLogueado.empleado.Apellidos,
                        Nss = empleadoLogueado.empleado.Nss,
                        Rol = empleadoLogueado.empleado.Rol,
                        FechaNacimiento = empleadoLogueado.empleado.FechaNacimiento,
                        Sexo = empleadoLogueado.empleado.Sexo,
                        NumeroTelefono = empleadoLogueado.empleado.NumeroTelefono,
                        Correo = empleadoLogueado.empleado.Correo,
                        Calle = empleadoLogueado.empleado.Calle,
                        NumeroCasa = empleadoLogueado.empleado.NumeroCasa,
                        CodigoPostal = empleadoLogueado.empleado.CodigoPostal,
                        RFC = empleadoLogueado.empleado.RFC,
                        Matricula = empleadoLogueado.empleado.Matricula,
                        IdSucursal = empleadoLogueado.empleado.IdSucursal
                    };

                    UsuarioEnLinea.Instancia.EstablecerDatosUsuarioEnSesion(empleadoConsultado);

                    _mainWindowModeloVista.CambiarModeloVista(new MainWindowModeloVista());
                }
            }
        }

        public void Registrarse(Object obj)
        {
            //Preguntar Yael
            //_mainWindowModeloVista.CambiarModeloVista(new RegistrarEmpleadoModeloVista());
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarMatricula();
            valido &= ValidarContraseña();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarMatricula()
        {
            if (string.IsNullOrEmpty(Matricula))
            {
                MatriculaCampoVacio = Visibility.Visible;
                return false;
            }

            MatriculaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarContraseña()
        {
            if (string.IsNullOrEmpty(Contraseña))
            {
                ContraseñaCampoVacio = Visibility.Visible;
                return false;
            }
            ContraseñaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCampos()
        {
            MatriculaCampoVacio = Visibility.Collapsed;
            ContraseñaCampoVacio = Visibility.Collapsed;
            DatosIncorrectos = Visibility.Collapsed;
        }
    }
}
