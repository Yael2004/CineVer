using CineVerCliente.FuncionServicio;
using CineVerCliente.PeliculaServicio;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CineVerCliente.ModeloVista
{
    public class AgregarFuncionModeloVista : BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }
        private ObservableCollection<PeliculaDTOs> _peliculas;
        public ObservableCollection<PeliculaDTOs> Peliculas
        {
            get => _peliculas;
            set
            {
                _peliculas = value;
                OnPropertyChanged(nameof(Peliculas));
            }
        }
        private PeliculaDTOs _peliculaSeleccionada;
        public PeliculaDTOs PeliculaSeleccionada
        {
            get => _peliculaSeleccionada;
            set
            {
                _peliculaSeleccionada = value;
                OnPropertyChanged(nameof(PeliculaSeleccionada));

                if (_peliculaSeleccionada?.poster != null)
                {
                    Poster = ConvertirBytesAImagen(_peliculaSeleccionada.poster);
                }
                else
                {
                    Poster = null;
                }
            }
        }


        private string _nombreSala;
        public string NombreSala
        {
            get => _nombreSala;
            set
            {
                _nombreSala = value;
                OnPropertyChanged(nameof(NombreSala));

            }
        }

        private SalaServicioClient _salaServicio = new SalaServicioClient();
        private PelículaServicioClient _peliculaServicio = new PelículaServicioClient();
        private FuncionServicioClient _funcionServicio = new FuncionServicioClient();

        public AgregarFuncionModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            GuardarCommand = new ComandoModeloVista(Guardar);
            CancelarCommand = new ComandoModeloVista(Regresar);

            var peliculasBase = _peliculaServicio.ObtenerListaPeliculas(1);    //Cambiar por el id de la sucursal
            _peliculas = new ObservableCollection<PeliculaDTOs>(peliculasBase.Peliculas);
            Poster = new BitmapImage(new Uri("pack://application:,,,/Vista/Icono_Imagen.png"));
        }
        public ImageSource ConvertirRutaAImageSource(string ruta)
        {
            if (string.IsNullOrEmpty(ruta) || !System.IO.File.Exists(ruta))
                return null;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ruta, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze(); // Muy importante si usarás la imagen en bindings de UI
            return bitmap;
        }

        private ImageSource _poster;
        public ImageSource Poster
        {
            get => _poster;
            set
            {
                _poster = value;
                OnPropertyChanged(nameof(Poster));
            }
        }
        private ImageSource ConvertirBytesAImagen(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;

            var imagen = new System.Windows.Media.Imaging.BitmapImage();
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                imagen.BeginInit();
                imagen.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                imagen.StreamSource = ms;
                imagen.EndInit();
                imagen.Freeze(); // Evita problemas de hilos en WPF
            }
            return imagen;
        }

        private void Guardar(Object obj)
        {

        }
        private void Regresar(Object obj)
        {

        }
    }
}
