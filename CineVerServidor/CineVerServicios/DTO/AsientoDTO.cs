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
        public int IdAsiento { get; set; }

        [DataMember]
        public int IdFila { get; set; }

        [DataMember]
        public string LetraColumna { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
