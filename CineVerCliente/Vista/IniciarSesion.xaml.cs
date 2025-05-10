using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CineVerCliente.Vista
{
    /// <summary>
    /// Lógica de interacción para IniciarSesion.xaml
    /// </summary>
    public partial class IniciarSesion : UserControl
    {
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void ContraseñaValida(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidarContraseña(e.Text);
        }

        private bool ValidarContraseña(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetterOrDigit(c) && c != ' ' && !"@$!%?&#_".Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        private void CampoAlfanumerico(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !ValidarAlfanumerico(e.Text);
        }

        private bool ValidarAlfanumerico(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
