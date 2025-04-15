using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class ComandoModeloVista : ICommand
    {
        private readonly Action<object> _ejecutar;
        private readonly Predicate<object> _puedeEjecutarse;

        public ComandoModeloVista(Action<object> ejecutar, Predicate<object> puedeEjecutarse)
        {
            _ejecutar = ejecutar;
            _puedeEjecutarse = puedeEjecutarse;
        }

        public ComandoModeloVista(Action<object> ejecutar)
        {
            _ejecutar = ejecutar;
            _puedeEjecutarse = null;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _puedeEjecutarse == null || _puedeEjecutarse(parameter);
        }

        public void Execute(object parameter)
        {
            _ejecutar(parameter);
        }
    }
}
