using CineVerCliente.Helpers;
using CineVerCliente.PeliculaServicio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarPelículaModeloVista : BaseModeloVista
    {
        public string Titulo { get; set; }
        public string Director { get; set; }
        public string Sinopsis { get; set; }
        public int Duracion { get; set; }
        public ObservableCollection<string> Generos { get; set; }
        public string GeneroSeleccionado { get; set; }

        private Visibility TituloVacio;
        private Visibility DuracionVacio;
        private Visibility SinopsisVacio;
        private Visibility DirectorVacio;
        private Visibility GeneroVacio;
        private Visibility PosterVacio;
        private Visibility _mostrarMensajeConfirmar;

        public string PosterPath { get; set; }

        public ICommand CargarImagenCommand { get; }
        public ICommand AumentarDuracionCommand { get; }
        public ICommand DisminuirDuracionCommand { get; }
        public ICommand AgregarActorCommand { get; }
        public ICommand GuardarCommand { get; }
        public ICommand RegresarCommand { get; }
        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public AgregarPelículaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            Generos = new ObservableCollection<string> { "Terror", "Acción", "Comedia", "Romance", "Drama" };
            CargarImagenCommand = new ComandoModeloVista(CargarImagen);
            GuardarCommand = new ComandoModeloVista(Guardar);
            RegresarCommand = new ComandoModeloVista(Regresar);
        }

        public AgregarPelículaModeloVista()
        {
        }

        private void CargarImagen(Object obj)
        {
            var dlg = new OpenFileDialog { Filter = "Imagenes|*.jpg;*.png" };
            if (dlg.ShowDialog() == true)
                PosterPath = dlg.FileName;
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
                pelicula.nombre = Titulo;
                pelicula.poster = File.Exists(PosterPath) ? File.ReadAllBytes(PosterPath) : null;
                PelículaServicioClient peliculaServicio = new PelículaServicioClient();
                string mensaje = peliculaServicio.AgregarPelicula(pelicula);
                Notificacion.Mostrar(mensaje);
            }
        }
        private void Regresar(Object obj)
        {

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
            if (string.IsNullOrEmpty(PosterPath))
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
            if (string.IsNullOrEmpty(GeneroSeleccionado))
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
                SinopsisVacio=Visibility.Collapsed;
                return true;
            }
        }
        private bool ValidarDuracion()
        {
            if(Duracion <= 0)
            {
                DuracionVacio = Visibility.Visible;
                return false;
            }
            else
            {
                DuracionVacio=Visibility.Collapsed;
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
