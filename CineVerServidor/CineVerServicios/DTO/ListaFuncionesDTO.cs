using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [ServiceContract]
    public class ListaFuncionesDTO
    {
        [DataMember]
        public List<FuncionDTO> funciones;
        [DataMember]
        public ResultDTO result;
        public ListaFuncionesDTO() { 
            funciones = new List<FuncionDTO>();
            result = new ResultDTO(true,string.Empty);
        }
    }
}
