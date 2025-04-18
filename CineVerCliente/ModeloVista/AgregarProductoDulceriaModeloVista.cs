using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarProductoDulceriaModeloVista : BaseModeloVista
    {
        private string _nombreProducto;
        private int _cantidadInventario;
        private float _costoUnitario;
        private float _precioVentaUnitario;
        private byte[] _imagenProducto;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand ConfirmarCambiosComando { get; }
        public ICommand AgregarNuevoProducto { get; }
        public ICommand CancelarComando { get; }

        public string NombreProducto
        {
            get { return _nombreProducto; }
            set
            {
                _nombreProducto = value;
                OnPropertyChanged();
            }
        }

        public int CantidadInventario
        {
            get { return _cantidadInventario; }
            set
            {
                _cantidadInventario = value;
                OnPropertyChanged();
            }
        }

        public float CostoUnitario
        {
            get { return _costoUnitario; }
            set
            {
                _costoUnitario = value;
                OnPropertyChanged();
            }
        }

        public float PrecioVentaUnitario
        {
            get { return _precioVentaUnitario; }
            set
            {
                _precioVentaUnitario = value;
                OnPropertyChanged();
            }
        }

        public byte[] ImagenProducto
        {
            get { return _imagenProducto; }
            set
            {
                _imagenProducto = value;
                OnPropertyChanged();
            }
        }
    }
}
