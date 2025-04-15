using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class ComandoGenericoModeloVista<T> : ICommand
    {
        private readonly Action<T> _ejecutar;
        private readonly Predicate<T> _puedeEjecutarse;

        public ComandoGenericoModeloVista(Action<T> ejecutar, Predicate<T> puedeEjecutarse = null)
        {
            _ejecutar = ejecutar;
            _puedeEjecutarse = puedeEjecutarse;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
