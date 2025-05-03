using CineVerCliente.ModeloVista;
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
    /// Lógica de interacción para CantidadMerma.xaml
    /// </summary>
    public partial class CantidadMerma : UserControl
    {
        public CantidadMerma()
        {
            InitializeComponent();
        }

        private void SoloNumeros(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox &&
                DataContext is CantidadMermaModeloVista vm)
            {
                string textoPrevio = textBox.Text;
                int caretIndex = textBox.CaretIndex;
                string textoSimulado = textoPrevio.Insert(caretIndex, e.Text);

                if (!int.TryParse(textoSimulado, out int valorNuevo))
                {
                    e.Handled = true;
                    return;
                }

                if (!int.TryParse(vm.CantidadInventario, out int cantidadMaxima))
                {
                    return;
                }

                if (valorNuevo > cantidadMaxima)
                {
                    e.Handled = true;
                }
            }
        }

    }
}
