using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class ProductoDulceria
    {
        private string _costoUnitario;
        public string CostoUnitario
        {
            get { return _costoUnitario; }
            set
            {
                _costoUnitario = value.Replace("$", "");
                _costoUnitario = $"$ {_costoUnitario}";
            }
        }
        private string _precioVentaUnitario;
        public string PrecioVentaUnitario
        {
            get { return _precioVentaUnitario; }
            set
            {
                _precioVentaUnitario = value.Replace("$", "");
                _precioVentaUnitario = $"$ {_precioVentaUnitario}";
            }
        }
        public byte[] Imagen { get; set; }
        public string Nombre { get; set; }
        public string CantidadInventario { get; set; }
    }
}
