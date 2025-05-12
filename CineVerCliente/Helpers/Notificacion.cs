using CineVerCliente.Vista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CineVerCliente.Helpers
{
    public static class Notificacion
    {
        public static void Mostrar(string mensaje, int duracionMs = 3000)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var noti = new NotificacionExito(mensaje, duracionMs);

                var mainWindow = Application.Current.MainWindow as MainWindow;
                var notiPanel = mainWindow?.FindName("NotiPanel") as Panel;

                notiPanel?.Children.Add(noti);
            });
        }

        public static void MostrarExcepcion(int duracionMs = 3000)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var noti = new NotificacionExcepcion(duracionMs);
                var mainWindow = Application.Current.MainWindow as MainWindow;
                var notiPanel = mainWindow?.FindName("NotiPanel") as Panel;
                notiPanel?.Children.Add(noti);
            });
        }
    }
}
