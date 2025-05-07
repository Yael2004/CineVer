using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using CineVerCliente.PeliculaServicio;

namespace CineVerCliente.ModeloVista
{
    public class ConsultarPeliculasModeloVista:BaseModeloVista
    {
        private PelículaServicioClient peliculaServicio = new PelículaServicioClient();
        private string _busqueda;
        private readonly MainWindowModeloVista _mainWindow;
        public string Busqueda
        {
            get => _busqueda;
            set
            {
                _busqueda = value;
                OnPropertyChanged(nameof(Busqueda));
            }
        }
        public ObservableCollection<ImagenGrupo> GruposDeImagenes { get; set; } = new ObservableCollection<ImagenGrupo>();
        public ConsultarPeliculasModeloVista(MainWindowModeloVista mainWindow)
        {
            _mainWindow = mainWindow;
            CargarImagenesDesdeBytes();
        }

        public void CargarImagenesDesdeBytes()
        {
            var peliculas = peliculaServicio.ObtenerListaPeliculas(1);
            GruposDeImagenes.Clear();
            var imagenes = ObtenerListaImagenes(peliculas);
            

            for (int i = 0; i < imagenes.Count; i += 4)
            {
                GruposDeImagenes.Add(new ImagenGrupo
                {
                    ImagenGrande = imagenes.ElementAtOrDefault(i),
                    ImagenPequena1 = imagenes.ElementAtOrDefault(i + 1),
                    ImagenPequena2 = imagenes.ElementAtOrDefault(i + 2),
                    ImagenPequena3 = imagenes.ElementAtOrDefault(i + 3),
                });
            }
        }
        public ImageSource ConvertirBytesAImagen(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;

            var imagen = new BitmapImage();
            using (var ms = new MemoryStream(bytes))
            {
                imagen.BeginInit();
                imagen.CacheOption = BitmapCacheOption.OnLoad;
                imagen.StreamSource = ms;
                imagen.EndInit();
                imagen.Freeze(); // Importante para evitar errores de hilo si lo usas en bindings
            }
            return imagen;
        }
        public List<ImageSource> ObtenerListaImagenes(ListaPeliculasDTO peliculas)
        {
            var listaImagenes = new List<ImageSource>();
            foreach (var pelicula in peliculas.Peliculas)
            {
                var img = ConvertirBytesAImagen(pelicula.poster);
                if (img != null)
                    listaImagenes.Add(img);
            }
            return listaImagenes;
        }

    }
}
