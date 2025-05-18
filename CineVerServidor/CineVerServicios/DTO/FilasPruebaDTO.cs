using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class FilasPruebaDTO
    {
        [DataMember]
        public int NumeroFila { get; set; }
        [DataMember]
        public int CantidadAsientos { get; set; }
        [DataMember]
        public List<AsientoDTO> Asientos { get; set; } = new List<AsientoDTO>();
    }
}
