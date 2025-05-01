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
        public ResultDTO ResultDTO { get; set; }

        public ListaProductosDulceriaDTO()
        {
            Productos = new List<ProductoDulceriaDTO>();
        }

        public ListaProductosDulceriaDTO(List<ProductoDulceriaDTO> productos, ResultDTO resultDTO)
        {
            Productos = productos;
            ResultDTO = resultDTO;
        }
    }
}
