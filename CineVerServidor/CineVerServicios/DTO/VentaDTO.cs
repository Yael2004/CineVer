using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class VentaDTO
    {
        [DataMember]
        public int IdVenta { get; set; }

        [DataMember]
        public int IdEmpleado { get; set; }

        [DataMember]
        public int IdSocio { get; set; }

        [DataMember]
        public int IdSucursal { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public string MetodoPago { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string TipoVenta { get; set; }
    }
}
