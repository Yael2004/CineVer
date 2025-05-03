using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaEmpleadosDTO
    {
        [DataMember]
        public List<EmpleadoDTO> Empleados { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaEmpleadosDTO()
        {
            Empleados = new List<EmpleadoDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
