using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CineVerCliente.ModeloVista
{
    class IniciarSesionModeloVista : BaseModeloVista
    {
        private string _matricula;
        private string _contrasenia;

        private Visibility _matriculaCampoVacio;
        private Visibility _contraseniaCampoVacio;
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

        public string Contrasenia
        {
            get { return _contrasenia; }
            set
            {
                _contrasenia = value;
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

        public Visibility ContraseniaCampoVacio
        {
            get { return _contraseniaCampoVacio; }
            set
            {
                _contraseniaCampoVacio = value;
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

        public void IniciarSesion(Object obj)
        {
            if (ValidarCampos())
            {
                _mainWindowModeloVista.CambiarModeloVista(new MainWindowModeloVista());
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
            valido &= ValidarContrasenia();

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

        private bool ValidarContrasenia()
        {
            if (string.IsNullOrEmpty(Contrasenia))
            {
                ContraseniaCampoVacio = Visibility.Visible;
                return false;
            }
            ContraseniaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCampos()
        {
            MatriculaCampoVacio = Visibility.Collapsed;
            ContraseniaCampoVacio = Visibility.Collapsed;
            DatosIncorrectos = Visibility.Collapsed;
        }
    }
}
