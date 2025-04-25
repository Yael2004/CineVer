using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace CineVerServicios.DTO
{
    [DataContract]
    public class ResultDTO
    {
        [DataMember]
        public bool EsExitoso { get; private set; }
        [DataMember]
        public string Error { get; private set; }

        public ResultDTO(bool esExitoso, string error)
        {
            EsExitoso = esExitoso;
            Error = esExitoso ? string.Empty : error;
        }


        public static ResultDTO Exito()
        {
            return new ResultDTO( true, string.Empty);
        }

        public static ResultDTO Fallo(string error)
        {
            return new ResultDTO( false, error);
        }
    }
}
