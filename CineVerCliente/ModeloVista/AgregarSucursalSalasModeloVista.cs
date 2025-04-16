using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarSucursalSalasModeloVista : BaseModeloVista
    {
        private Visibility _mostrarMensajeConfirmar = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand RegresarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand ContinuarComando { get; }

        public Visibility MostrarMensajeConfirmar
        {
            get { return _mostrarMensajeConfirmar; }
            set
            {
                _mostrarMensajeConfirmar = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmar));
            }
        }

        public AgregarSucursalSalasModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            RegresarComando = new ComandoModeloVista(Regresar);
            ContinuarComando = new ComandoModeloVista(Continuar);
            CancelarComando = new ComandoModeloVista(Cancelar);
        }

        private void Regresar(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new AgregarSucursalDatosModeloVista(_mainWindowModeloVista));
        }

        private void Continuar(object obj)
        {
            MostrarMensajeConfirmar = Visibility.Visible;
        }

        private void Cancelar(object obj)
        {
            MostrarMensajeConfirmar = Visibility.Collapsed;
        }
    }
}
