using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    class SocioConsultado
    {
        private string _nombre;
        private string _apellidos;
        private string _folio;
        private byte[] _foto;
        private int _puntosSocio;

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Folio { get; set; }
        public byte[] Foto { get; set; }
        public int PuntosSocio { get; set; }
    }
}
