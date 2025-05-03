using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaPromocionesDTO
    {
        [DataMember]
        public List<PromocionDTO> Promociones { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }
    }
}
