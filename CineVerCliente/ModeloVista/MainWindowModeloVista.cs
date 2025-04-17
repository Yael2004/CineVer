using CineVerCliente.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class MainWindowModeloVista : BaseModeloVista
    {
        private object _vistaActual;
        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand SucursalComando {  get; }

        public object VistaActualModelo
        {
            get { return _vistaActual; }
            set
            {
                _vistaActual = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModeloVista()
        {
            _mainWindowModeloVista = this;
            SucursalComando = new ComandoModeloVista(EditarSucursal);
        }

        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
        }

        private void AgregarSucursal(object obj)
        {
            CambiarModeloVista(new AgregarSucursalDatosModeloVista(_mainWindowModeloVista));
        }

        private void EditarSucursal(object obj)
        {
            CambiarModeloVista(new EditarSucursalModeloVista(_mainWindowModeloVista));
        }
    }
}
