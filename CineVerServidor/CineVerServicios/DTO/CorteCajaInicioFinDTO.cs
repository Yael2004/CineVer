using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class CorteCajaInicioFinDTO
    {
        [DataMember]
        public decimal InicioDia { get; set; }
        [DataMember]
        public decimal VentaTotal { get; set; }
        [DataMember]
        public ResultDTO ResultDTO { get; set; }

        public CorteCajaInicioFinDTO() { }
    }
}
