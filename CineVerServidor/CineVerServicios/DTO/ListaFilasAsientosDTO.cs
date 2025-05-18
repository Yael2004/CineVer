using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaFilasAsientosDTO
    {
        [DataMember]
        public List<FilasPruebaDTO> Filas { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
        public ListaFilasAsientosDTO()
        {
            Filas = new List<FilasPruebaDTO>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
