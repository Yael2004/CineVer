using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class EmpleadoConsultado
    {
        private string _nombre;
        private string _apellidos;
        private string _matricula;
        private byte[] _foto;

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Matricula { get; set; }
        public byte[] Foto { get; set; }
    }
}
