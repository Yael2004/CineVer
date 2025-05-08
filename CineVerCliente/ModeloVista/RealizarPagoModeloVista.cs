using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineVerCliente.ModeloVista
{
    public class RealizarPagoModeloVista
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
    }
}
