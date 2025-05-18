using CineVerCliente.FuncionServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.PeliculaServicio;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CineVerCliente.ModeloVista
{
    public class ConsultarFuncionesModeloVista : BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ICommand EditarFuncionCommand { get; }
        public ICommand EliminarFuncionCommand { get; }
        public ICommand AceptarComando { get; }
        public ICommand AgregarFuncionComando { get; }
        public ICommand CancelarComando { get; } 
        public ICommand VenderComando { get; }
        public FuncionDTO FuncionSeleccionada { get; set; }
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

        private ObservableCollection<PeliculaConFunciones> _peliculas;
        public ICommand AgregarFuncionCommand { get; }

        public ObservableCollection<PeliculaConFunciones> Peliculas
        {
            get { return _peliculas; }
            set
            {
                _peliculas = value;
                OnPropertyChanged();
            }
        }


        private SalaServicioClient _salaServicioCliente;
        private PelículaServicioClient _peliculaServicioCliente;
        private FuncionServicioClient _funcionServicioCliente;
        private ImageSource _poster;
        private DateTime _fechaSeleccionada;

        public DateTime FechaSeleccionada
        {
            get { return _fechaSeleccionada; }
            set
            {
                _fechaSeleccionada = value;
                OnPropertyChanged();
                CargarPeliculasPorFecha();
            }
        }

        public ConsultarFuncionesModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            MostrarMensajeConfirmar = false;
            _salaServicioCliente = new SalaServicioClient();
            _peliculaServicioCliente = new PelículaServicioClient();
            _funcionServicioCliente = new FuncionServicioClient();
            AgregarFuncionComando = new ComandoModeloVista(AgregarFuncion2);
            AgregarFuncionCommand = new ComandoModeloVista(AgregarFuncion);
            EditarFuncionCommand = new ComandoModeloVista(EditarFuncion);
            EliminarFuncionCommand = new ComandoModeloVista(EliminarFuncion);
            AceptarComando = new ComandoModeloVista(AceptarEliminar);
            CancelarComando = new ComandoModeloVista(CancelarEliminar);
            VenderComando = new ComandoModeloVista(Vender);

            FechaSeleccionada = DateTime.Today;

        }
        private void AgregarFuncion2(Object obj)
        {
            CambiarModeloVista(new AgregarFuncionModeloVista(_mainWindowModeloVista));
        }
        private void CargarPeliculasPorFecha()
        {
            var funcionesDelDia = _funcionServicioCliente.ObtenerFuncionesPorFecha(FechaSeleccionada);

            Peliculas = new ObservableCollection<PeliculaConFunciones>();

            List<int?> listaIdPeliculas = funcionesDelDia.funciones
                .Select(f => f.idPelicula)
                .Where(id => id.HasValue)
                .Distinct()
                .ToList();

            foreach (var id in listaIdPeliculas)
            {
                var peliculaDto = _peliculaServicioCliente.ObtenerPeliculaPorID(id.Value);
                var funcionesDto = _funcionServicioCliente
                    .ObtenerFuncionesPorPeliculaYFecha(id.Value, FechaSeleccionada)
                    .funciones;

                PeliculaConFunciones peliculaConFunciones = new PeliculaConFunciones
                {
                    Pelicula = peliculaDto,
                    Funciones = funcionesDto
                .OrderBy(f => f.horaInicio)
                .Select(f => new FuncionVista { Funcion = f })
                .ToArray()
                };

                Peliculas.Add(peliculaConFunciones);
            }
        }


        public ImageSource ByteArrayToImageSource(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze(); // Para que sea usable en hilos diferentes si es necesario
            }

            return image;
        }
        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            _mainWindowModeloVista.VistaActualModelo = nuevoModeloVista;
        }
        public void AgregarFuncion(object obj)
        {
            System.Diagnostics.Debug.WriteLine($"Tipo de objeto recibido: {obj?.GetType()}");
            if (obj is PeliculaConFunciones peliculaConFunciones)
            {

                var funcionModeloVista = new AgregarFuncionModeloVista(_mainWindowModeloVista, peliculaConFunciones.Pelicula);
                CambiarModeloVista(funcionModeloVista);
            }
        }
        private void EditarFuncion(Object obj)
        {
            if (obj is FuncionVista funcionVisita)
            {
                if (funcionVisita.Funcion.fecha == DateTime.Today && funcionVisita.Funcion.horaInicio <= DateTime.Now.TimeOfDay)
                {
                    Notificacion.Mostrar("No se puede eliminar una función que ya empezó o terminó.");
                }
                else if (funcionVisita.Funcion.fecha < DateTime.Today)
                {
                    Notificacion.Mostrar("No se puede eliminar una función que ya ha pasado.");
                }
                else
                {
                    var funcionModeloVista = new EditarFuncionModeloVista(_mainWindowModeloVista, funcionVisita.Funcion);
                    CambiarModeloVista(funcionModeloVista);
                }
            }
        }

        private void EliminarFuncion(Object obj)
        {

            if (obj is FuncionVista funcionVisita)
            {
                if(funcionVisita.Funcion.fecha == DateTime.Today && funcionVisita.Funcion.horaInicio <= DateTime.Now.TimeOfDay)
                {
                    Notificacion.Mostrar("No se puede eliminar una función que ya empezó o terminó.");
                }
                else if(funcionVisita.Funcion.fecha < DateTime.Today)
                {
                    Notificacion.Mostrar("No se puede eliminar una función que ya ha pasado.");
                }
                else{
                    FuncionSeleccionada = funcionVisita.Funcion;
                    MostrarMensajeConfirmar = true;
                }

                    
            }
        }

        private void Vender(object obj)
        {
            if (obj is FuncionVista funcionVista)
            {
                var idPelicula = funcionVista.Funcion.idPelicula ?? 0;
                var cliente = _peliculaServicioCliente.ObtenerPeliculaPorID(idPelicula);

                var pelicula = new Pelicula
                {
                    Nombre = cliente.nombre,
                    Duracion = (TimeSpan)cliente.duracion,
                    Poster = cliente.poster
                };

                var funcion = new Funcion
                {
                    Fecha = (DateTime)funcionVista.Funcion.fecha,
                    HoraInicio = (TimeSpan)funcionVista.Funcion.horaInicio,
                    Precio = (decimal)funcionVista.Funcion.precioBoleto,
                    IdSala = (int)funcionVista.Funcion.idSala
                };

                var ventaModeloVista = new VenderBoletoModeloVista(_mainWindowModeloVista, pelicula, funcion);
                CambiarModeloVista(ventaModeloVista);
            }
        }

        private void AceptarEliminar(object obj)
        {
            if (FuncionSeleccionada != null)
            {
                _funcionServicioCliente.EliminarFuncion(FuncionSeleccionada);
                CargarPeliculasPorFecha();
                MostrarMensajeConfirmar = false;
                CargarPeliculasPorFecha();
            }
        }
        private void CancelarEliminar(object obj)
        {
            MostrarMensajeConfirmar = false;
        }
    }
    }
