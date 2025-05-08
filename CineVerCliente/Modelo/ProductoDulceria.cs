using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class ProductoDulceria
    {
        public int Id { get; set; }
        public string CostoUnitario { get; set; }
        public string PrecioVentaUnitario { get; set; }
        public byte[] Imagen { get; set; }
        public string Nombre { get; set; }
        public string CantidadInventario { get; set; }
        public int IdSucursal { get; set; }
    }
}
