using CineVerCliente.FuncionServicio;
using CineVerCliente.Helpers;
using CineVerCliente.PeliculaServicio;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CineVerCliente.ModeloVista
{
    public class AgregarFuncionModeloVista : BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ICommand GuardarCommand { get; }
        public ICommand RegresarCommand { get; }
        private Visibility _nombrePeliculaVacio;
        private DateTime? _fechaFuncionInicio;
        private PelículaServicioClient _peliculaServicioClient;
        public DateTime? FechaFuncionInicio
        {
            get => _fechaFuncionInicio;
            set
            {
                _fechaFuncionInicio = value;
                OnPropertyChanged(nameof(FechaFuncionInicio));
            }
        }

        private DateTime? _fechaFuncionFin;
        public DateTime? FechaFuncionFin
        {
            get => _fechaFuncionFin;
            set
            {
                _fechaFuncionFin = value;
                OnPropertyChanged(nameof(FechaFuncionFin));
            }
        }

        private Visibility _fechaInicioVacio;
        public Visibility FechaInicioVacio
        {
            get => _fechaInicioVacio;
            set
            {
                _fechaInicioVacio = value;
                OnPropertyChanged(nameof(FechaInicioVacio));
            }
        }
        private Visibility _fechaFinVacio;
        public Visibility FechaFinVacio
        {
            get => _fechaFinVacio;
            set
            {
                _fechaFinVacio = value;
                OnPropertyChanged(nameof(FechaFinVacio));
            }
        }

        private Visibility _horaFuncionVacio;
        public Visibility HoraFuncionVacio
        {
            get => _horaFuncionVacio;
            set
            {
                _horaFuncionVacio = value;
                OnPropertyChanged(nameof(HoraFuncionVacio));
            }
        }

        private Visibility _costoVacio;
        public Visibility CostoVacio
        {
            get => _costoVacio;
            set
            {
                _costoVacio = value;
                OnPropertyChanged(nameof(CostoVacio));
            }
        }

        private DateTime? _horaFuncion;
        public DateTime? HoraFuncion
        {
            get => _horaFuncion;
            set
            {
                _horaFuncion = value;
                OnPropertyChanged(nameof(HoraFuncion));
            }
        }

        private double _costoBoleto;
        public double CostoBoleto
        {
            get => _costoBoleto;
            set
            {
                _costoBoleto = value;
                OnPropertyChanged(nameof(CostoBoleto));
            }
        }

        public Visibility NombrePeliculaVacio
        {
            get => _nombrePeliculaVacio;
            set
            {
                _nombrePeliculaVacio = value;
                OnPropertyChanged(nameof(NombrePeliculaVacio));
            }
        }
        private Visibility _nombreSalaVacio;
        public Visibility NombreSalaVacio
        {
            get => _nombreSalaVacio;
            set
            {
                _nombreSalaVacio = value;
                OnPropertyChanged(nameof(NombreSalaVacio));
            }
        }

        private SalaDTO _salaSeleccionada;
        public SalaDTO SalaSeleccionada
        {
            get => _salaSeleccionada;
            set
            {
                _salaSeleccionada = value;
                OnPropertyChanged(nameof(SalaSeleccionada));
            }
        }
        private ObservableCollection<SalaDTO> _salas;
        public ObservableCollection<SalaDTO> Salas
        {
            get => _salas;
            set
            {
                _salas = value;
                OnPropertyChanged(nameof(Salas));
            }
        }
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
            RegresarCommand = new ComandoModeloVista(Regresar);
            _peliculaServicioClient = new PelículaServicioClient();

            var peliculasBase = _peliculaServicio.ObtenerListaPeliculas(1);    //Cambiar por el id de la sucursal
            _peliculas = new ObservableCollection<PeliculaDTOs>(peliculasBase.Peliculas);
            var salasBase = _salaServicio.ObtenerSalasPorSucursal(1);    //Cambiar por el id de la sucursal
            _salas = new ObservableCollection<SalaDTO>(salasBase.Salas);
            Poster = new BitmapImage(new Uri("pack://application:,,,/Vista/Icono_Imagen.png"));
            OcultarCamposVacios();
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
            if (ValidarCampos())
            {
                DateTime hoy = DateTime.Today;
                DateTime ahora = DateTime.Now;
                Decimal costoDecimal2 = Convert.ToDecimal(CostoBoleto);
                if (_fechaFuncionInicio > FechaFuncionFin)
                {
                    // Mostrar mensaje o establecer visibilidad de advertencia
                    Notificacion.Mostrar("La fecha de inicio no puede ser posterior a la fecha de fin.");
                    
                }
                else if (costoDecimal2 >= 1000)
                {
                    Notificacion.Mostrar("El costo del boleto debe ser menor a $1000.");
                    
                }
                else if (_fechaFuncionInicio < hoy)
                {
                    Notificacion.Mostrar("No se pueden agregar funciones en fechas anteriores al día de hoy.");
                }
                else if (_fechaFuncionInicio == hoy && HoraFuncion.Value.TimeOfDay < ahora.TimeOfDay)
                {
                    Notificacion.Mostrar("No se pueden agregar funciones en horas anteriores a la hora actual.");
                }
                else
                {
                    DateTime fechaActual = FechaFuncionInicio.Value;
                    DateTime fechaFin = FechaFuncionFin.Value;
                    List <FuncionDTO> funcionesNoAgregadas = new List<FuncionDTO>();

                    while (fechaActual <= fechaFin)
                    {
                        var funcion = new FuncionDTO();
                        funcion.idSala = SalaSeleccionada.idSala;
                        funcion.idPelicula = PeliculaSeleccionada.idPelicula;
                        Decimal costoDecimal = Convert.ToDecimal(CostoBoleto);
                        funcion.precioBoleto = costoDecimal;
                        funcion.fecha = fechaActual;
                        funcion.horaInicio = HoraFuncion.Value.TimeOfDay;

                        var fechaSinHora = fechaActual.Date;
                        
                        var FuncionesDiaSala = _funcionServicio.ObtenerFuncionesPorFechaYSala(SalaSeleccionada.idSala, fechaSinHora);
                        

                        var horaInicioNueva = fechaActual + HoraFuncion.Value.TimeOfDay;
                        var duracionNueva = PeliculaSeleccionada.duracion.Value;
                        var horaFinNueva = horaInicioNueva + duracionNueva;

                        bool traslape = false;
                        Console.WriteLine("Si pasa por aqui, hay {0}",FuncionesDiaSala.funciones.Count());

                        foreach (var funcionExistente in FuncionesDiaSala.funciones)
                        {
                            Console.WriteLine("ID: {0} Nombre {1}", funcionExistente.idPelicula,funcionExistente.Película.nombre);
                            var horaInicioExistente = funcionExistente.fecha + funcionExistente.horaInicio.Value; // ya es DateTime
                            PeliculaDTOs peliculaExistente = _peliculaServicioClient.ObtenerPeliculaPorID(funcionExistente.idPelicula.Value);
                            var duracionExistente = peliculaExistente.duracion;
                            var horaFinExistente = horaInicioExistente + duracionExistente;

                            Console.WriteLine("Si paso");
                            // Verificar traslape: si una empieza antes de que termine la otra y viceversa
                            if (!(horaFinNueva <= horaInicioExistente || horaInicioNueva >= horaFinExistente))
                            {
                                traslape = true;
                                break;
                            }
                        }

                        if (traslape)
                        {
                            funcionesNoAgregadas.Add(funcion);
                        }
                        else
                        {
                            // Agregar función porque no hay traslape
                            Console.WriteLine( _funcionServicio.AgregarFuncion(funcion));
                        }
                        fechaActual = fechaActual.AddDays(1);

                    }
                    if(funcionesNoAgregadas.Count > 0)
                    {
                        // Mostrar mensaje o establecer visibilidad de advertencia
                        Notificacion.Mostrar("Algunas funciones no se pudieron agregar debido a traslapes.");
                    }
                    else
                    {
                        Notificacion.Mostrar("Funciones agregadas exitosamente.");
                    }


                }

                    

            }
        }
        private void Regresar(Object obj)
        {

        }
        public bool ValidarCampos()
        {
            bool valido = true;
            valido &= ValidarTituloPelicula();
            valido &= ValidarNombreSala();
            valido &= ValidarCosto();
            valido &= ValidarHoraFuncion();
            valido &= ValidarFechaInicio();
            valido &= ValidarFechaFin();
            return valido;
        }
        public bool ValidarTituloPelicula()
        {
            if (string.IsNullOrEmpty(PeliculaSeleccionada?.nombre))
            {
                NombrePeliculaVacio = Visibility.Visible;
                return false;
            }
            else
            {
                NombrePeliculaVacio = Visibility.Collapsed;
                return true;
            }
        }
        public bool ValidarNombreSala()
        {
            if (string.IsNullOrEmpty(SalaSeleccionada?.nombre))
            {
                NombreSalaVacio = Visibility.Visible;
                return false;
            }
            else
            {
                NombreSalaVacio = Visibility.Collapsed;
                return true;
            }
        }
        public bool ValidarCosto()
        {
            if (CostoBoleto <= 0)
            {
                CostoVacio = Visibility.Visible;
                return false;
            }
            else
            {
                CostoVacio = Visibility.Collapsed;
                return true;
            }
        }
        public bool ValidarHoraFuncion()
        {
            if (HoraFuncion == null)
            {
                HoraFuncionVacio = Visibility.Visible;
                return false;
            }
            else
            {
                HoraFuncionVacio = Visibility.Collapsed;
                return true;
            }
        }
        public bool ValidarFechaInicio()
        {
            if (FechaFuncionInicio == null)
            {
                FechaInicioVacio = Visibility.Visible;
                return false;
            }
            else
            {
                FechaInicioVacio = Visibility.Collapsed;
                return true;
            }
        }
        public bool ValidarFechaFin()
        {
            if (FechaFuncionFin == null)
            {
                FechaFinVacio = Visibility.Visible;
                return false;
            }
            else
            {
                FechaFinVacio = Visibility.Collapsed;
                return true;
            }
        }
        public void OcultarCamposVacios()
        {
            NombrePeliculaVacio = Visibility.Collapsed;
            NombreSalaVacio = Visibility.Collapsed;
            CostoVacio = Visibility.Collapsed;
            HoraFuncionVacio = Visibility.Collapsed;
            FechaInicioVacio = Visibility.Collapsed;
            FechaFinVacio = Visibility.Collapsed;
        }
    }
}
