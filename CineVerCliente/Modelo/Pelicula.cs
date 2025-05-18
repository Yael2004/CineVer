using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class Pelicula
    {
        public string Nombre { get; set; }
        public TimeSpan Duracion { get; set; }
        public Byte[] Poster { get; set; }
    }
}
