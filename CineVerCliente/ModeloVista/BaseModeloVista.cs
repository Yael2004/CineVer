using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.ModeloVista
{
    public abstract class BaseModeloVista
    {
        public event PropertyChangedEventHandler CambiarPropiedad;

        protected void OnPropertyChanged([CallerMemberName] string nombrePropiedad = null)
        {
            CambiarPropiedad?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        }
    }
}
