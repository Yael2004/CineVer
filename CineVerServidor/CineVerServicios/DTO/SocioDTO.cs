using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class SocioDTO
    {
        [DataMember]
        public int idSocio { get; set; }
        [DataMember]
        public string folio { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string numeroTelefono { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string calle { get; set; }
        [DataMember]
        public string numeroCasa { get; set; }
        [DataMember]
        public string codigoPostal { get; set; }
    }
}
