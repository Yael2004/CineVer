using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CineVerCliente.Helpers
{
    public class EstadoAColorConvertidor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EstadoAsiento estado)
            {
                switch (estado)
                {
                    case EstadoAsiento.DISPONIBLE:
                        return Brushes.LightGreen;
                    case EstadoAsiento.OCUPADO:
                        return Brushes.Red;
                    case EstadoAsiento.SELECCIONADO:
                        return Brushes.Blue;
                    case EstadoAsiento.MANTENIMIENTO:
                        return Brushes.Gray;
                    default:
                        return Brushes.White;
                }
            }

            return Brushes.White;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
