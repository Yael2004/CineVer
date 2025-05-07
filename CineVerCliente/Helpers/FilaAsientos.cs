using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Helpers
{
    public class FilaAsientos : INotifyPropertyChanged
    {
        public int NumeroFila { get; set; }

        private int _cantidadAsientos;
        public int CantidadAsientos
        {
            get => _cantidadAsientos;
            set
            {
                _cantidadAsientos = value;
                OnPropertyChanged(nameof(CantidadAsientos));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

}
