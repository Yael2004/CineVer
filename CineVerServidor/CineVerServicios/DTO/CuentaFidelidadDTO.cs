using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class CuentaFidelidadDTO
    {
        [DataMember]
        public int IdCuenta { get; set; }
        [DataMember]
        public int IdSocio { get; set; }
        [DataMember]
        public int Puntos { get; set; }
    }
}
