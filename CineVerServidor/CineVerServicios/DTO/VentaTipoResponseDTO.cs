using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class VentaTipoResponseDTO
    {
        [DataMember]
        public decimal Total { get; set; }
        [DataMember]
        public ResultDTO ResultDTO { get; set; }
    }
}
