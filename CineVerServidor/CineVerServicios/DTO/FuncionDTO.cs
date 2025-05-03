using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [ServiceContract]
    public class FuncionDTO
    {
        [DataMember]
        public int idFuncion { get; set; }
        [DataMember]
        public Nullable<int> idSala { get; set; }
        [DataMember]
        public Nullable<int> idPelicula { get; set; }
        [DataMember]
        public Nullable<System.DateTime> fecha { get; set; }
        [DataMember]
        public Nullable<System.TimeSpan> horaInicio { get; set; }
        [DataMember]
        public Nullable<decimal> precioBoleto { get; set; }
        [DataMember]
        public virtual Sala Sala { get; set; }
        [DataMember]
        public virtual Película Película { get; set; }
    }
}
