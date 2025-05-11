using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.ModeloVista
{
    public class MenuPrincipalModeloVista:BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public MenuPrincipalModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
        }
    }
}
