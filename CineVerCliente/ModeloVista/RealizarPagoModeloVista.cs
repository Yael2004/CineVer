using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineVerCliente.ModeloVista
{
    public class RealizarPagoModeloVista : BaseModeloVista
    {
        private string _totalPagar;
        private string _nombrePromocion;
        private string _puntosParaPagar;
        private string _puntosRestantes;
        private Visibility _pagoConTarjeta;
        private Visibility _confirmarPagoConTarjeta;
        private Visibility _cancelarPagoConTarjeta;
        private Visibility _confirmarPagoConEfectivo;
        private Visibility _cancelarPagoConEfectivo;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public string TotalPagar
        {
            get { return _totalPagar; }
            set
            {
                _totalPagar = value;
                OnPropertyChanged(nameof(TotalPagar));
            }
        }

        public string NombrePromocion
        {
            get { return _nombrePromocion; }
            set
            {
                _nombrePromocion = value;
                OnPropertyChanged(nameof(NombrePromocion));
            }
        }

        public RealizarPagoModeloVista(MainWindowModeloVista mainWindowModeloVista) 
        { 
            _mainWindowModeloVista = mainWindowModeloVista;
        }
    }
}
