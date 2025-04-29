using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    class RegistrarGastoModeloVista : BaseModeloVista
    {
        private string _nombreGasto;
        private string _totalGasto;

        private Visibility _nombreGastoCampoVacio;
        private Visibility _totalGastoCampoVacio;

        private Visibility _mostrarMensajeConfirmacion = Visibility.Collapsed;

        public ICommand RegistrarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand AceptarConfirmacionComando { get; }
        public ICommand CancelarConfirmacionComando { get; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public string NombreGasto
        {
            get { return _nombreGasto; }
            set
            {
                _nombreGasto = value;
                OnPropertyChanged();
            }
        }

        public string TotalGasto
        {
            get { return _totalGasto; }
            set
            {
                _totalGasto = value;
                OnPropertyChanged();
            }
        }

        public Visibility NombreGastoCampoVacio
        {
            get { return _nombreGastoCampoVacio; }
            set
            {
                _nombreGastoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility TotalGastoCampoVacio
        {
            get { return _totalGastoCampoVacio; }
            set
            {
                _totalGastoCampoVacio = value;
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

        public RegistrarGastoModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            RegistrarComando = new ComandoModeloVista(RegistrarGasto);
            CancelarComando = new ComandoModeloVista(Cancelar);
            AceptarConfirmacionComando = new ComandoModeloVista(AceptarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);
            
            OcultarCampos();
        }

        public void RegistrarGasto(Object obj)
        {
            if (ValidarCampos())
            {
                MostrarMensajeConfirmacion = Visibility.Visible;
            }
        }

        public void Cancelar(Object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new MainWindowModeloVista());
        }

        public void AceptarConfirmacion(Object obj)
        {
            Notificacion.Mostrar("Gasto registrado correctamente", 4000);
        }

        public void CancelarConfirmacion(Object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarNombreGasto();
            valido &= ValidarTotalGasto();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarNombreGasto()
        {
            if (string.IsNullOrEmpty(NombreGasto))
            {
                NombreGastoCampoVacio = Visibility.Visible;
                return false;
            }

            NombreGastoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarTotalGasto()
        {
            if (string.IsNullOrEmpty(TotalGasto))
            {
                TotalGastoCampoVacio = Visibility.Visible;
                return false;
            }
            TotalGastoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCampos()
        {
            NombreGastoCampoVacio = Visibility.Collapsed;
            TotalGastoCampoVacio = Visibility.Collapsed;
        }
    }
}
