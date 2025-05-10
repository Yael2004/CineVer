using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class SocioConsultado
    {
        private int _idSocio;
        private string _folio;
        private string _nombres;
        private string _apellidos;
        private DateTime _fechaNacimiento;
        private string _sexo;
        private string _numeroTelefono;
        private string _correo;
        private string _calle;
        private string _numeroCasa;
        private string _codigoPostal;
        private int _puntosSocio;

        public int IdSocio { get; set; }
        public string Folio { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string NumeroTelefono { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
        public string CodigoPostal { get; set; }
        public int PuntosSocio { get; set; }
    }
}
