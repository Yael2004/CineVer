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
                    case EstadoAsiento.Disponible:
                        return Brushes.LightGreen;
                    case EstadoAsiento.Ocupado:
                        return Brushes.Red;
                    case EstadoAsiento.Seleccionado:
                        return Brushes.Blue;
                    case EstadoAsiento.Mantenimiento:
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
