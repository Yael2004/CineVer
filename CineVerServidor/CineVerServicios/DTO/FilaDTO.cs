using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class FilaDTO
    {
        [DataMember]
        public int idFila { get; set; }
        [DataMember]
        public Nullable<int> númeroFila { get; set; }
        [DataMember]
        public Nullable<int> idSala { get; set; }
        [DataMember]
        public Nullable<int> numeroAsientos { get; set; }
        public FilaDTO() { }
    }
}
