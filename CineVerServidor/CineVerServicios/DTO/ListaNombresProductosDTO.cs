using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaNombresProductosDTO
    {
        [DataMember]
        public List<string> NombresProductos { get; set; }
        [DataMember]
        public ResultDTO Resultado { get; set; }

        public ListaNombresProductosDTO()
        {
            NombresProductos = new List<string>();
        }
    }
}
