using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaAsientosDTO
    {
        [DataMember]
        public List<AsientoDTO> Asientos { get; set; }

        [DataMember]
        public ResultDTO Result { get; set; }

        public ListaAsientosDTO()
        {
            Asientos = new List<AsientoDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
