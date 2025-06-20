﻿using CineVerCliente.Helpers;
using CineVerCliente.PeliculaServicio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CineVerCliente.Modelo;

namespace CineVerCliente.ModeloVista
{
    public class EditarPeliculaModeloVista:BaseModeloVista
    {
        private string _titulo;
        private string _genero;
        private string _director;
        private string _sinopsis;
        private int _duracion;
        private string _poster;
        public string Poster
        {
            get => _poster;
            set
            {
                _poster = value;
                OnPropertyChanged(nameof(Poster));
            }
        }
        private PeliculaDTOs _peliculaOriginal;
        public PeliculaDTOs PeliculaOriginal
        {
            get => _peliculaOriginal;
            set
            {
                _peliculaOriginal = value;
                OnPropertyChanged(nameof(PeliculaOriginal));
            }
        }


        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        public string Genero
        {
            get => _genero;
            set
            {
                _genero = value;
                OnPropertyChanged(nameof(Genero));
            }
        }

        public string Director
        {
            get => _director;
            set
            {
                _director = value;
                OnPropertyChanged(nameof(Director));
            }
        }

        public string Sinopsis
        {
            get => _sinopsis;
            set
            {
                _sinopsis = value;
                OnPropertyChanged(nameof(Sinopsis));
            }
        }

        public int Duracion
        {
            get => _duracion;
            set
            {
                _duracion = value;
                OnPropertyChanged(nameof(Duracion));
            }
        }
        public ObservableCollection<string> _generos;
        public ObservableCollection<string> Generos
        {
            get => _generos;
            set
            {
                _generos = value;
                OnPropertyChanged(nameof(Generos));
            }
        }

        private Visibility _tituloVacio;
        private Visibility _duracionVacio;
        private Visibility _sinopsisVacio;
        private Visibility _directorVacio;
        private Visibility _generoVacio;
        private Visibility _posterVacio;

        public Visibility TituloVacio
        {
            get { return _tituloVacio; }
            set
            {
                _tituloVacio = value;
                OnPropertyChanged(nameof(TituloVacio));
            }
        }

        public Visibility DuracionVacio
        {
            get { return _duracionVacio; }
            set
            {
                _duracionVacio = value;
                OnPropertyChanged(nameof(DuracionVacio));
            }
        }

        public Visibility SinopsisVacio
        {
            get { return _sinopsisVacio; }
            set
            {
                _sinopsisVacio = value;
                OnPropertyChanged(nameof(SinopsisVacio));
            }
        }

        public Visibility DirectorVacio
        {
            get { return _directorVacio; }
            set
            {
                _directorVacio = value;
                OnPropertyChanged(nameof(DirectorVacio));
            }
        }

        public Visibility GeneroVacio
        {
            get { return _generoVacio; }
            set
            {
                _generoVacio = value;
                OnPropertyChanged(nameof(GeneroVacio));
            }
        }

        public Visibility PosterVacio
        {
            get { return _posterVacio; }
            set
            {
                _posterVacio = value;
                OnPropertyChanged(nameof(PosterVacio));
            }
        }

        private Visibility _mostrarMensajeConfirmar;


        private ImageSource _posterPath;
        public ImageSource PosterPath
        {
            get => _posterPath;
            set
            {
                _posterPath = value;
                OnPropertyChanged(nameof(PosterPath));
            }
        }

        public ICommand CargarImagenCommand { get; }
        public ICommand AumentarDuracionCommand { get; }
        public ICommand DisminuirDuracionCommand { get; }
        public ICommand AgregarActorCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand RegresarCommand { get; }
        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public EditarPeliculaModeloVista(MainWindowModeloVista mainWindowModeloVista,PeliculaDTOs peliculaOriginal)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            Generos = new ObservableCollection<string> { "Terror", "Acción", "Comedia", "Romance", "Drama" };
            CargarImagenCommand = new ComandoModeloVista(CargarImagen);
            GuardarCommand = new ComandoModeloVista(Guardar);
            PeliculaOriginal = peliculaOriginal;
            RegresarCommand = new ComandoModeloVista(Regresar);
            PosterPath = ConvertirBytesAImagen(PeliculaOriginal.poster);
            OcultarCamposVacios();
            Titulo = PeliculaOriginal.nombre;
            Genero = PeliculaOriginal.genero;
            Director = PeliculaOriginal.director;
            Sinopsis = PeliculaOriginal.sinopsis;
            Duracion = (int)PeliculaOriginal.duracion.Value.TotalMinutes;
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
        public EditarPeliculaModeloVista()
        {
        }

        private void CargarImagen(Object obj)
        {
            var dlg = new OpenFileDialog { Filter = "Imagenes|*.jpg;*.png" };
            if (dlg.ShowDialog() == true)
            {
                Poster = dlg.FileName;
                PosterPath = new BitmapImage(new Uri(Poster));
            }
                
        }

        private void Guardar(Object obj)
        {
            if (ValidarCampos())
            {
                PeliculaDTOs pelicula = new PeliculaDTOs();
                pelicula.director = Director;
                TimeSpan duracion = TimeSpan.FromMinutes(Duracion);
                pelicula.duracion = duracion;
                pelicula.director = Director;
                pelicula.sinopsis = Sinopsis;
                pelicula.idSucursal = UsuarioEnLinea.Instancia.IdSucursal;
                pelicula.genero = Genero;
                pelicula.nombre = Titulo;

                if (!string.IsNullOrEmpty(Poster) && File.Exists(Poster))
                {
                    pelicula.poster = File.ReadAllBytes(Poster);
                }
                else
                {
                    pelicula.poster = PeliculaOriginal.poster;
                }

                PelículaServicioClient peliculaServicio = new PelículaServicioClient();
                string mensaje = peliculaServicio.EditarPelicula(pelicula, PeliculaOriginal);
                Notificacion.Mostrar(mensaje);
            }
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
        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
            _mainWindowModeloVista.VistaActualModelo = nuevoModeloVista;
        }
        private void Regresar(Object obj)
        {
            CambiarModeloVista(new ConsultarPeliculasModeloVista(_mainWindowModeloVista));
        }
        public Visibility MostrarMensajeConfirmar
        {
            get { return _mostrarMensajeConfirmar; }
            set
            {
                _mostrarMensajeConfirmar = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmar));
            }
        }
        private bool ValidarCampos()
        {
            bool valido = true;
            valido &= ValidarDirector();
            valido &= ValidarGenero();
            valido &= ValidarPoster();
            valido &= ValidarSinopsis();
            valido &= ValidarTitulo();
            valido &= ValidarDuracion();
            if (valido)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidarTitulo()
        {
            if (string.IsNullOrEmpty(Titulo))
            {
                TituloVacio = Visibility.Visible; return false;
            }
            else
            {
                TituloVacio = Visibility.Collapsed;
                return true;
            }
        }
        private bool ValidarPoster()
        {
            if (PosterPath == null)
            {
                PosterVacio = Visibility.Visible;
                return false;
            }
            else
            {
                PosterVacio = Visibility.Collapsed; return true;
            }
        }
        private bool ValidarGenero()
        {
            if (string.IsNullOrEmpty(Genero))
            {
                GeneroVacio = Visibility.Visible; return false;
            }
            else
            {
                GeneroVacio = Visibility.Collapsed;
                return true;
            }

        }
        private bool ValidarDirector()
        {
            if (string.IsNullOrEmpty(Director))
            {
                DirectorVacio = Visibility.Visible; return false;
            }
            DirectorVacio = Visibility.Collapsed;
            return true;
        }
        private bool ValidarSinopsis()
        {
            if (string.IsNullOrEmpty(Sinopsis))
            {
                SinopsisVacio = Visibility.Visible; return false;
            }
            else
            {
                SinopsisVacio = Visibility.Collapsed;
                return true;
            }
        }
        private bool ValidarDuracion()
        {
            if (Duracion <= 0)
            {
                DuracionVacio = Visibility.Visible;
                return false;
            }
            else
            {
                DuracionVacio = Visibility.Collapsed;
                return true;
            }
        }
        private void OcultarCamposVacios()
        {
            TituloVacio = Visibility.Collapsed;
            DuracionVacio = Visibility.Collapsed;
            SinopsisVacio = Visibility.Collapsed;
            DirectorVacio = Visibility.Collapsed;
            GeneroVacio = Visibility.Collapsed;
            PosterVacio = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
