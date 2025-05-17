using CineVerCliente.Helpers;
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
    /// Lógica de interacción para RegistrarEmpleado.xaml
    /// </summary>
    public partial class RegistrarEmpleado : UserControl
    {
        public RegistrarEmpleado()
        {
            InitializeComponent();
        }

        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsNumero(e.Text);
        }

        private bool EsNumero(string texto)
        {
            return int.TryParse(texto, out _);
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

        private void SoloLetras(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsLetra(e.Text);
        }

        private bool EsLetra(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetter(c))
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

        private void CorreoValido(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsCorreoValido(e.Text);
        }

        private bool EsCorreoValido(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetter(c) && c != ' ' && !"@_.".Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
