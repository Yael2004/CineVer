using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.ModeloVista
{
    public class CantidadMermaModeloVista : BaseModeloVista
    {
        public ProductoDulceria Producto { get; set; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public CantidadMermaModeloVista(MainWindowModeloVista mainWindowModeloVista, ProductoDulceria producto)
        { 
            _mainWindowModeloVista = mainWindowModeloVista;
            Producto = producto;
        }
    }
}
