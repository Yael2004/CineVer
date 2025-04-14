using CineVerCliente.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.ModeloVista
{
    public class MainWindowModeloVista : BaseModeloVista
    {
        private object _vistaActual;

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
            VistaActualModelo = new AgregarSucursalDatos();
        }

        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
        }
    }
}
