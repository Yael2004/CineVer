using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaSociosDTO
    {
        [DataMember]
        public List<SocioDTO> Socios { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaSociosDTO()
        {
            Socios = new List<SocioDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
