using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class GastoDTO
    {
        [DataMember]
        public int IdGasto { get; set; }
        [DataMember]
        public string Motivo { get; set; }
        [DataMember]
        public decimal Monto { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public int IdSucursal { get; set; }
        [DataMember]
        public int IdEmpleado { get; set; }
    }
}
