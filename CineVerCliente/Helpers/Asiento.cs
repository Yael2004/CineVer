using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Helpers
{
    public class Asiento : INotifyPropertyChanged
    {
        public int Fila { get; set; }
        public int Numero { get; set; }
        public bool EstaSeleccionado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
