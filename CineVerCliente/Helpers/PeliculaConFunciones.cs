using CineVerCliente.FuncionServicio;
using CineVerCliente.PeliculaServicio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CineVerCliente.Helpers
{
    public class PeliculaConFunciones
    {
        public PeliculaDTOs Pelicula { get; set; }

        public FuncionVista[] Funciones { get; set; }

        public string Titulo => Pelicula?.nombre;

        public string Clasificacion => Pelicula?.genero;

        public string Duracion => Pelicula != null
            ? $"{(int)Pelicula.duracion.Value.TotalHours}h {Pelicula.duracion.Value.Minutes}m"
            : string.Empty;

        public ImageSource Poster
        {
            get
            {
                if (Pelicula?.poster == null || Pelicula.poster.Length == 0)
                    return null;

                using (var ms = new MemoryStream(Pelicula.poster))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
        }
    }

}
