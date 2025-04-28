using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class SucursalDTO
    {
        [DataMember]
        public int IdSucursal { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public string Ciudad { get; set; }

        [DataMember]
        public string Calle { get; set; }

        [DataMember]
        public string NumeroEnLaCalle { get; set; }

        [DataMember]
        public string CodigoPostal { get; set; }

        [DataMember]
        public TimeSpan HoraApertura { get; set; }

        [DataMember]
        public TimeSpan HoraCierre { get; set; }
    }
}
