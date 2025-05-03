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
        public int IdSocio { get; set; }
        [DataMember]
        public string Folio { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public DateTime FechaNacimiento { get; set; }
        [DataMember]
        public string Sexo { get; set; }
        [DataMember]
        public string NumeroTelefono { get; set; }
        [DataMember]
        public string Correo { get; set; }
        [DataMember]
        public string Calle { get; set; }
        [DataMember]
        public string NumeroCasa { get; set; }
        [DataMember]
        public string CodigoPostal { get; set; }
    }
}
