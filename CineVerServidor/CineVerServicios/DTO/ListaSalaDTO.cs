using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaSalaDTO
    {
        [DataMember]
        public List<SalaDTO> Salas { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaSalaDTO()
        {
            Salas = new List<SalaDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
