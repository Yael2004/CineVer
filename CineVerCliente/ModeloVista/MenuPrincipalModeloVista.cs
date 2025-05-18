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
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class MenuPrincipalModeloVista:BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DateTime? _fechaSeleccionada;
        public DateTime? FechaSeleccionada
        {
            get => _fechaSeleccionada;
            set
            {
                if (_fechaSeleccionada != value)
                {
                    _fechaSeleccionada = value;
                    OnPropertyChanged();
                    CargarOpcionesPrimero(); // Actualiza el primer ComboBox
                }
            }
        }
        private FuncionServicioClient _funcionServicioClient;
        private PelículaServicioClient _peliculaServicioClient;
        private SalaServicioClient _salaServicioCLiente;
        public ICommand VenderBoletoCommand;


        private ObservableCollection<PeliculaServicio.PeliculaDTOs> _peliculas;
        public ObservableCollection<PeliculaServicio.PeliculaDTOs> Peliculas
        {
            get => _peliculas;
            set { _peliculas = value; OnPropertyChanged(); }
        }

        private PeliculaServicio.PeliculaDTOs _peliculaSeleccionada;
        public PeliculaServicio.PeliculaDTOs PeliculaSeleccionada
        {
            get => _peliculaSeleccionada;
            set
            {
                if (_peliculaSeleccionada != value)
                {
                    _peliculaSeleccionada = value;
                    OnPropertyChanged();
                    CargarOpcionesSegundo(); // Actualiza el segundo ComboBox
                }
            }
        }

        private ObservableCollection<FuncionDTO> _funciones;
        public ObservableCollection<FuncionDTO> Funciones
        {
            get => _funciones;
            set { _funciones = value; OnPropertyChanged(); }
        }

        private string _funcionSeleccionada;
        public string FuncionSeleccionada
        {
            get => _funcionSeleccionada;
            set { _funcionSeleccionada = value; OnPropertyChanged(); }
        }

        private void CargarOpcionesPrimero()
        {
            
            Peliculas.Clear();

            if (FechaSeleccionada.HasValue)
            {
                var funcionesDelDia = _funcionServicioClient.ObtenerFuncionesPorFecha(FechaSeleccionada.Value);
                

                List<int?> listaIdPeliculas = funcionesDelDia.funciones
                    .Select(f => f.idPelicula)
                    .Where(id => id.HasValue)
                    .Distinct()
                    .ToList();
                foreach (var id in listaIdPeliculas)
                {
                    PeliculaServicio.PeliculaDTOs peliculaInsertar = _peliculaServicioClient.ObtenerPeliculaPorID(id.Value);
                    Peliculas.Add(peliculaInsertar);
                }
                }

            PeliculaSeleccionada = null; // Reset
            Funciones.Clear(); // También resetear el segundo ComboBox
        }

        private void CargarOpcionesSegundo()
        {
            Funciones.Clear();

            if (PeliculaSeleccionada != null)
            {
                var funcionesBase = _funcionServicioClient.ObtenerFuncionesPorPeliculaYFecha(PeliculaSeleccionada.idPelicula, FechaSeleccionada.Value).funciones;
                foreach (var funcion in funcionesBase)
                {
                    Funciones.Add(funcion);
                }
            }

            FuncionSeleccionada = null;
        }

        
        public MenuPrincipalModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            _peliculaServicioClient = new PelículaServicioClient();
            _salaServicioCLiente = new SalaServicioClient();
            _funcionServicioClient = new FuncionServicioClient();
            Peliculas = new ObservableCollection<PeliculaServicio.PeliculaDTOs>();
            Funciones = new ObservableCollection<FuncionDTO>();
            VenderBoletoCommand = new ComandoModeloVista(VenderBoleto);
            FechaSeleccionada = DateTime.Today;
        }
        private void VenderBoleto(Object obj)
        {
            if (FuncionSeleccionada != null)
            {
                //Pasar a ventana de vender boleto
            }

        }
    }
}
