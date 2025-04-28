using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaSucursalesDTO
    {
        [DataMember]
        public List<SucursalDTO> Sucursales { get; set; }

        [DataMember]
        public ResultDTO Result { get; set; }

        public ListaSucursalesDTO()
        {
            Sucursales = new List<SucursalDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
