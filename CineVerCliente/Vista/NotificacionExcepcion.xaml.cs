using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CineVerCliente.Vista
{
    /// <summary>
    /// Lógica de interacción para NotificacionExcepcion.xaml
    /// </summary>
    public partial class NotificacionExcepcion : UserControl
    {
        public NotificacionExcepcion(int duracionMs = 3000)
        {
            InitializeComponent();
            MensajeText.Text = "Ha ocurrido un error inesperado, inténtelo de nuevo más tarde";

            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            Root.BeginAnimation(OpacityProperty, fadeIn);

            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(duracionMs) };
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
                fadeOut.Completed += (s2, e2) =>
                {
                    var parent = this.Parent as Panel;
                    parent?.Children.Remove(this);
                };
                Root.BeginAnimation(OpacityProperty, fadeOut);
            };
            timer.Start();
        }
    }
}
