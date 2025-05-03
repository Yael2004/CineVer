using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaVentasDTO
    {
        [DataMember]
        public List<VentaDTO> Ventas { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaVentasDTO()
        {
            Ventas = new List<VentaDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
