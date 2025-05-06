using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaGastosDTO
    {
        [DataMember]
        public List<GastoDTO> Gastos { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaGastosDTO()
        {
            Gastos = new List<GastoDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
