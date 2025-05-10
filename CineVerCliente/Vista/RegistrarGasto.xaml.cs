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
    /// Lógica de interacción para RegistrarGasto.xaml
    /// </summary>
    public partial class RegistrarGasto : UserControl
    {
        public RegistrarGasto()
        {
            InitializeComponent();
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

        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !EsNumero(e.Text);
        }

        private bool EsNumero(string texto)
        {
            return int.TryParse(texto, out _);
        }
    }
}
