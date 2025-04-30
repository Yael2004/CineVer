using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaProductosDulceriaDTO
    {
        [DataMember]
        public List<ProductoDulceriaDTO> Productos { get; set; }
        [DataMember]
        public int TotalProductos { get; set; }

        public ListaProductosDulceriaDTO()
        {
            Productos = new List<ProductoDulceriaDTO>();
            TotalProductos = 0;
        }

        public ListaProductosDulceriaDTO(List<ProductoDulceriaDTO> productos)
        {
            Productos = productos;
            TotalProductos = productos.Count;
        }
    }
}
