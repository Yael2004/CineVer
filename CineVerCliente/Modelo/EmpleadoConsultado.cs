using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class EmpleadoConsultado
    {
        private int _idEmpleado;
        private string _nombres;
        private string _apellidos;
        private string _nss;
        private string _rol;
        private DateTime _fechaNacimiento;
        private string _sexo;
        private string _numeroTelefono;
        private string _correo;
        private string _calle;
        private string _numeroCasa;
        private string _codigoPostal;
        private string _rfc;
        private string _matricula;
        private byte[] _foto;
        private int _idSucursal;

        public int IdEmpleado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Nss { get; set; }
        public string Rol { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string NumeroTelefono { get; set; }
        public string Correo { get; set; }
        public string Calle { get; set; }
        public string NumeroCasa { get; set; }
        public string CodigoPostal { get; set; }
        public string RFC { get; set; }
        public string Matricula { get; set; }
        public byte[] Foto { get; set; }
        public int IdSucursal { get; set; }
    }
}
