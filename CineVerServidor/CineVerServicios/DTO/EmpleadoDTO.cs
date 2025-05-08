using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    [DataContract]
    public class EmpleadoDTO
    {
        [DataMember]
        public int IdEmpleado { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string Nss { get; set; }
        [DataMember]
        public string Rol { get; set; }
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
        [DataMember]
        public string RFC { get; set; }
        [DataMember]
        public string Matricula { get; set; }
        [DataMember]
        public byte[] Foto { get; set; }
        [DataMember]
        public bool Contratado { get; set; }
        [DataMember]
        public int IdSucursal { get; set; }
    }
}
