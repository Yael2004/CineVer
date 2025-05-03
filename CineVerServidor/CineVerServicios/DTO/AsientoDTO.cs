using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class AsientoDTO
    {
        [DataMember]
        public Nullable<int> idFila { get; set; }
        [DataMember]
        public string letraColumna { get; set; }
        [DataMember]
        public int idAsiento { get; set; }
        [DataMember]
        public string estado { get; set; }
        public AsientoDTO() { }
    }
}
