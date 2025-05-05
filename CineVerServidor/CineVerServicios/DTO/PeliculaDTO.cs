using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace CineVerServicios.DTO
{
    [DataContract]
    public class PeliculaDTOs
    {
        
        [DataMember]
        public int idPelicula { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public Nullable<System.TimeSpan> duracion { get; set; }
        [DataMember]
        public string sinopsis { get; set; }
        [DataMember]
        public string director { get; set; }
        [DataMember]
        public Nullable<int> idSucursal { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public byte[] poster { get; set; }
    }
}
