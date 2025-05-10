using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class UsuarioEnLinea
    {
        private static UsuarioEnLinea _instancia;

        public int IdEmpleado { get; private set; }
        public string Nombres { get; private set; }
        public string Apellidos { get; private set; }
        public string Nss { get; private set; }
        public string Rol { get; private set; }
        public DateTime Fechanacimiento { get; private set; }
        public string Sexo { get; private set; }
        public string NumeroTelefono { get; private set; }
        public string Correo { get; private set; }
        public string Calle { get; private set; }
        public string NumeroCasa { get; private set; }
        public string CodigoPostal { get; private set; }
        public string RFC { get; private set; }
        public string Matricula { get; private set; }
        public byte[] Foto { get; private set; }
        public bool Contratado { get; private set; }
        public byte[] Contraseña { get; private set; }
        public int IdSucursal { get; private set; }

        private UsuarioEnLinea() { }

        public static UsuarioEnLinea Instancia
        {
            get
            {
                if (_instancia == null)
                    _instancia = new UsuarioEnLinea();

                return _instancia;
            }
        }

        public void EstablecerDatosUsuarioEnSesion(EmpleadoConsultado empleado)
        {
            IdEmpleado = empleado.IdEmpleado;
            Nombres = empleado.Nombres;
            Apellidos = empleado.Apellidos;
            Nss = empleado.Nss;
            Rol = empleado.Rol;
            Fechanacimiento = empleado.FechaNacimiento;
            Sexo = empleado.Sexo;
            NumeroTelefono = empleado.NumeroTelefono;
            Correo = empleado.Correo;
            Calle = empleado.Calle;
            NumeroCasa = empleado.NumeroCasa;
            CodigoPostal = empleado.CodigoPostal;
            RFC = empleado.RFC;
            Matricula = empleado.Matricula;
            Foto = empleado.Foto;
            Contratado = empleado.Contratado;
            Contraseña = empleado.Contraseña;
            IdSucursal = empleado.IdSucursal;
        }

        public void CerrarSesionActual()
        {
            _instancia = null;
        }
    }
}
