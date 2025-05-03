using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class SalaDTO
    {
        [DataMember]
        public int idSala { get; set; }
        [DataMember]
        public Nullable<int> numeroFilas { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string estadoSala { get; set; }
        [DataMember]
        public Nullable<int> idSucursal { get; set; }

    }
}
