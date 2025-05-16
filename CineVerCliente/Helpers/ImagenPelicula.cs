using CineVerCliente.PeliculaServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CineVerCliente.Helpers
{
    public class ImagenPelicula
    {
        public PeliculaDTOs Pelicula { get; set; }
        public ImageSource Imagen { get; set; }
    }
}
