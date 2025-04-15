using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarSucursalModeloVista : BaseModeloVista
    {
        private string _nombreSucursal;
        private char _codigoPostal;
        private string _estado;
        private string _ciudad;
        private string _calle;
        private int _numero;
        private TimeSpan _horaApertura;
        private TimeSpan _horaCierre;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand SiguienteComando { get; }
        public ICommand RegresarComand { get; }

        public string NombreSucursal
        {
            get { return _nombreSucursal; }
            set
            {
                _nombreSucursal = value;
                OnPropertyChanged();
            }
        }

        public char CodigoPostal
        {
            get { return _codigoPostal; }
            set
            {
                _codigoPostal = value;
                OnPropertyChanged();
            }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
                OnPropertyChanged();
            }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set
            {
                _ciudad = value;
                OnPropertyChanged();
            }
        }

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged();
            }
        }

        public int Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraApertura
        {
            get { return _horaApertura; }
            set
            {
                _horaApertura = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraCierre
        {
            get { return _horaCierre; }
            set
            {
                _horaCierre = value;
                OnPropertyChanged();
            }
        }

        public AgregarSucursalModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
        }
    }
}
