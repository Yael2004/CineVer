using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class CorteCajaDTO
    {
        [DataMember]
        public int IdCorteCaja { get; set; }
        [DataMember]
        public int IdSucursal { get; set; }
        [DataMember]
        public decimal EfectivoEsperado { get; set; }
        [DataMember]
        public decimal EfectivoCaja { get; set; }
        [DataMember]
        public decimal DiferenciaEfectivo { get; set; }
        [DataMember]
        public decimal VentaTotal { get; set; }
        [DataMember]
        public decimal Gastos { get; set; }
        [DataMember]
        public decimal Ganancias { get; set; }
        [DataMember]
        public decimal InicioDia { get; set; }
        [DataMember]
        public DateTime FechaCorte { get; set; }
    }
}
