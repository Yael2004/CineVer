using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class ListaPeliculasDTO
    {
        [DataMember]
        public List<PeliculaDTOs> Peliculas { get; set; }
        [DataMember]
        public ResultDTO Result { get; set; }

        public ListaPeliculasDTO()
        {
            Peliculas = new List<PeliculaDTOs>();
            Result = new ResultDTO(true, string.Empty);
        }
    }
}
