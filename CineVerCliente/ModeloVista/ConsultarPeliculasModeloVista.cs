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
using System.Windows.Input;
using System.Windows;
using CineVerCliente.Modelo;

namespace CineVerCliente.ModeloVista
{
    public class ConsultarPeliculasModeloVista:BaseModeloVista
    {
        private PelículaServicioClient peliculaServicio = new PelículaServicioClient();
        private string _busqueda;
        private readonly MainWindowModeloVista _mainWindow;
        
        public ICommand EditarPeliculaCommand { get; }
        public ICommand EliminarPeliculaCommand { get; }
        public ICommand AceptarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand AgregarPeliculaComando { get; }
        public string Busqueda
        {
            get => _busqueda;
            set
            {
                _busqueda = value;
                OnPropertyChanged(nameof(Busqueda));
                CargarImagenesDesdeBytes(peliculaServicio.ObtenerPeliculasPorNombre(UsuarioEnLinea.Instancia.IdSucursal,_busqueda));
            }
        }
        public ObservableCollection<ImagenGrupo> GruposDeImagenes { get; set; } = new ObservableCollection<ImagenGrupo>();
        public ConsultarPeliculasModeloVista(MainWindowModeloVista mainWindow)
        {
            _mainWindow = mainWindow;
            EditarPeliculaCommand = new ComandoModeloVista(EditarPelicula);
            EliminarPeliculaCommand = new ComandoModeloVista(EliminarPelicula);
            AceptarComando = new ComandoModeloVista(AceptarEliminar);
            CancelarComando = new ComandoModeloVista(CancelarEliminar);
            AgregarPeliculaComando = new ComandoModeloVista(AgregarPelicula);
            CargarImagenesDesdeBytes(peliculaServicio.ObtenerListaPeliculas(1));
        }

        public void CargarImagenesDesdeBytes(ListaPeliculasDTO peliculas)
        {
            
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
        public List<ImagenPelicula> ObtenerListaImagenes(ListaPeliculasDTO peliculas)
        {
            var lista = new List<ImagenPelicula>();
            foreach (var pelicula in peliculas.Peliculas)
            {
                var img = ConvertirBytesAImagen(pelicula.poster);
                if (img != null)
                {
                    lista.Add(new ImagenPelicula
                    {
                        Pelicula = pelicula,
                        Imagen = img
                    });
                }
            }
            return lista;
        }
        public void AgregarPelicula(Object obj)
        {
            CambiarModeloVista(new AgregarPelículaModeloVista(_mainWindow));
        }
        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
            _mainWindow.VistaActualModelo = nuevoModeloVista;
        }
        private object _vistaActual;
        public object VistaActualModelo
        {
            get { return _vistaActual; }
            set
            {
                _vistaActual = value;
                OnPropertyChanged();
            }
        }
        private void EditarPelicula(Object obj)
        {
            if(obj is PeliculaDTOs pelicula)
            {
                CambiarModeloVista(new EditarPeliculaModeloVista(_mainWindow,pelicula));
                
            }
        }
        private bool _mostrarMensajeConfirmar;
        public bool MostrarMensajeConfirmar
        {
            get => _mostrarMensajeConfirmar;
            set
            {
                _mostrarMensajeConfirmar = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmar));
            }
        }

        private PeliculaDTOs _peliculaSeleccionada;

        private void EliminarPelicula(Object obj)
        {
            if(obj is PeliculaDTOs pelicula)
            {
                _peliculaSeleccionada = pelicula;
                MostrarMensajeConfirmar = true;
            }
        }
        private void AceptarEliminar(Object obj)
        {
            if (_peliculaSeleccionada != null)
            {
                peliculaServicio.EliminarPelicula(_peliculaSeleccionada);
                CargarImagenesDesdeBytes(peliculaServicio.ObtenerListaPeliculas(1));
                MostrarMensajeConfirmar = false;
            }
        }
        private void CancelarEliminar(Object obj)
        {
            MostrarMensajeConfirmar = false;
        }


    }
}
