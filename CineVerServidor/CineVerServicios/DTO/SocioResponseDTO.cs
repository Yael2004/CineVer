using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class SocioResponseDTO
    {
        [DataMember]
        public SocioDTO socio { get; set; }
        [DataMember]
        public ResultDTO ResultDTO { get; set; }
    }
}
