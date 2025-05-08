using CineVerCliente.AsientoServicio;
using CineVerCliente.FilaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using CineVerCliente.PeliculaServicio;
using CineVerCliente.FuncionServicio;
using Asiento = CineVerCliente.Helpers.Asiento;
using System.Linq.Expressions;

namespace CineVerCliente.ModeloVista
{
    public class EditarSalaModeloVista:BaseModeloVista
    {
        public ICommand GuardarCommand { get; }
        private SalaServicioClient _salaServicio = new SalaServicioClient();
        private AsientoServicioClient _asientoServicio = new AsientoServicioClient();
        private FilaServicioClient _filaServicio = new FilaServicioClient();
        private SalaDTO _salaSeleccionada;
        public ICommand RegresarCommand { get; }

        private Visibility _nombreSalaVacio;
        private bool _cargandoInicialmente = false;
        public ObservableCollection<string> EstadosDisponibles { get; } =
        new ObservableCollection<string> { "DISPONIBLE", "EN REPARACION", "CERRADA" };
        

        public Visibility NombreSalaVacio
        {
            get => _nombreSalaVacio;
            set
            {
                _nombreSalaVacio = value;
                OnPropertyChanged(nameof(NombreSalaVacio));
            }
        }
        private Visibility _descripcionSalaVacio;
        public Visibility DescripcionSalaVacio
        {
            get => _descripcionSalaVacio;
            set
            {
                _descripcionSalaVacio = value;
                OnPropertyChanged(nameof(DescripcionSalaVacio));
            }
        }
        private string _estadoSala;
        public string EstadoSala
        {
            get => _estadoSala;
            set
            {
                _estadoSala = value;
                OnPropertyChanged(nameof(EstadoSala));
            }
        }

        private Visibility _numeroFilasVacio;
        public Visibility NumeroFilasVacio
        {
            get => _numeroFilasVacio;
            set
            {
                _numeroFilasVacio = value;
                OnPropertyChanged(nameof(NumeroFilasVacio));
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
        private string _descripcionSala;
        public string DescripcionSala
        {
            get => _descripcionSala;
            set
            {
                _descripcionSala = value;
                OnPropertyChanged(nameof(DescripcionSala));
            }
        }
        private int _numeroFilas;
        public int NumeroFilas
        {
            get => _numeroFilas;
            set
            {
                _numeroFilas = value;
                OnPropertyChanged(nameof(NumeroFilas));
                if (!_cargandoInicialmente)
                {
                    ActualizarFilas();
                }
            }
        }
        private readonly MainWindowModeloVista _mainWindowModeloVista;

        private ObservableCollection<FilaAsientos> _filas;
        public ObservableCollection<FilaAsientos> Filas
        {
            get => _filas;
            set
            {
                _filas = value;
                GenerarMapaSala();
                OnPropertyChanged(nameof(Filas));
            }
        }

        public EditarSalaModeloVista(MainWindowModeloVista mainWindowModeloVista, SalaDTO sala)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            GuardarCommand = new ComandoModeloVista(Guardar);
            RegresarCommand = new ComandoModeloVista(Regresar);
            Filas = new ObservableCollection<FilaAsientos>();
            _salaSeleccionada = sala;
            NombreSala = sala.nombre;
            DescripcionSala = sala.descripcion;
            EstadoSala = sala.estadoSala;
            _cargandoInicialmente = true;
            NumeroFilas = sala.numeroFilas.Value;
            _cargandoInicialmente = false;
            var filas = _filaServicio.ObtenerFilasDeSala(sala.idSala);
            
            foreach (var fila in filas.Filas)
            {
                var nuevaFila = new FilaAsientos
                {
                    NumeroFila = fila.númeroFila.Value,
                    CantidadAsientos = fila.numeroAsientos.Value
                };
                nuevaFila.PropertyChanged += Fila_PropertyChanged;
                Filas.Add(nuevaFila);
            }
            GenerarMapaSala();
            OcultarCamposVacios();
        }

        private void GenerarFilas()
        {
            foreach (var fila in Filas)
            {
                fila.PropertyChanged -= Fila_PropertyChanged;
            }
            Filas.Clear();
            for (int i = 1; i <= NumeroFilas; i++)
            {
                var fila = new FilaAsientos { NumeroFila = i };
                fila.PropertyChanged += Fila_PropertyChanged;

                Filas.Add(fila);
            }
            GenerarMapaSala();
        }
        private void ActualizarFilas()
        {
            // Si agregas filas nuevas
            while (Filas.Count < NumeroFilas)
            {
                var nuevaFila = new FilaAsientos
                {
                    NumeroFila = Filas.Count + 1,
                    CantidadAsientos = 0
                };
                nuevaFila.PropertyChanged += Fila_PropertyChanged;
                Filas.Add(nuevaFila);
            }

            // Si reduces el número de filas
            while (Filas.Count > NumeroFilas)
            {
                var fila = Filas[Filas.Count - 1];
                fila.PropertyChanged -= Fila_PropertyChanged;
                Filas.RemoveAt(Filas.Count - 1);
            }

            GenerarMapaSala();
        }

        private bool ValidarNombreSala()
        {
            if (string.IsNullOrEmpty(NombreSala))
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
        private bool ValidarDescripcionSala()
        {
            if (string.IsNullOrEmpty(DescripcionSala))
            {
                DescripcionSalaVacio = Visibility.Visible;
                return false;
            }
            else
            {
                DescripcionSalaVacio = Visibility.Collapsed;
                return true;
            }
        }
        private bool ValidarNumeroFilas()
        {
            if (NumeroFilas <= 0)
            {
                NumeroFilasVacio = Visibility.Visible;
                return false;
            }
            else
            {
                NumeroFilasVacio = Visibility.Collapsed;
                return true;
            }
        }
        private void EliminarFilasYAsientos()
        {
            var filas = _filaServicio.ObtenerFilasDeSala(_salaSeleccionada.idSala);
            if (filas == null || filas.Filas == null)
            {
                // Manejo de error: No se encontraron filas para eliminar.
                return;
            }

            foreach (var fila in filas.Filas)
            {
                var asientos = _asientoServicio.ObtenerListaAsientosPorFila(fila.idFila);
                if (asientos == null || asientos.Asientos == null)
                {
                    // Manejo de error: No se encontraron asientos para la fila.
                    continue;
                }

                foreach (var asiento in asientos.Asientos)
                {
                    _asientoServicio.EliminarAsiento(asiento);
                }

                _filaServicio.EliminarFila(fila);
            }
        }

        private void Guardar(Object obj)
        {
            if (ValidarCampos())
            {
                SalaDTO sala = new SalaDTO();
                sala.nombre = NombreSala;
                sala.descripcion = DescripcionSala;
                sala.estadoSala = EstadoSala;
                sala.numeroFilas = Filas.Count();
                sala.idSucursal = _salaSeleccionada.idSucursal; // Cambia esto por el ID de la sucursal correspondiente

                string mensaje = _salaServicio.EditarSala(sala,_salaSeleccionada);
                Console.WriteLine(mensaje);
                int idSala = _salaSeleccionada.idSala;

                // Primero, elimina los asientos y filas de la sala seleccionada
                EliminarFilasYAsientos();

                // Luego, agrega las nuevas filas
                foreach (var fila in Filas)
                {
                    FilaDTO filaDTO = new FilaDTO();
                    filaDTO.númeroFila = fila.NumeroFila;
                    filaDTO.numeroAsientos = fila.CantidadAsientos;
                    filaDTO.idSala = idSala;
                    _filaServicio.AgregarFila(filaDTO);
                    int idFila = _filaServicio.ObtenerIdFila(idSala, fila.NumeroFila);
                    // Ahora, agrega los asientos para cada fila
                    for (int i = 1; i <= fila.CantidadAsientos; i++)
                    {
                        AsientoDTO asientoDTO = new AsientoDTO();
                        char letra = (char)('A' + i - 1);
                        string letraColumna = letra.ToString();
                        asientoDTO.letraColumna = letraColumna;
                        asientoDTO.estado = "DISPONIBLE"; // O el estado que corresponda
                        asientoDTO.idFila = idFila;
                        try
                        {
                            _asientoServicio.AgregarAsiento(asientoDTO); // Agregar el asiento para la fila
                        }
                        catch (Exception ex)
                        {
                            // Manejo de error: No se pudo agregar el asiento.
                            // Puedes registrar el error o mostrar un mensaje al usuario.
                            Console.WriteLine($"Error al agregar asiento: {ex.Message}");
                        }
                        }
                }

                Notificacion.Mostrar(mensaje);
            }
        }



        private bool ValidarCampos()
        {
            bool valido = true;
            valido &= ValidarNombreSala();
            valido &= ValidarNumeroFilas();
            valido &= ValidarDescripcionSala();
            if (valido)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Regresar(Object obj)
        {
            CambiarModeloVista(new ConsultarSalasModeloVista(_mainWindowModeloVista));
        }
        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            _mainWindowModeloVista.VistaActualModelo = nuevoModeloVista;
        }


        private void OcultarCamposVacios()
        {
            NombreSalaVacio = Visibility.Collapsed;
            DescripcionSalaVacio = Visibility.Collapsed;
            NumeroFilasVacio = Visibility.Collapsed;
        }

        public ObservableCollection<ObservableCollection<Asiento>> MapaSala { get; set; }

        private void GenerarMapaSala()
        {
            MapaSala = new ObservableCollection<ObservableCollection<Asiento>>();

            foreach (var fila in Filas)
            {
                var filaAsientos = new ObservableCollection<Asiento>();
                for (int i = 1; i <= fila.CantidadAsientos; i++)
                {
                    filaAsientos.Add(new Asiento
                    {
                        Fila = fila.NumeroFila,
                        Numero = i
                    });
                }
                MapaSala.Add(filaAsientos);
            }

            OnPropertyChanged(nameof(MapaSala));
        }

        private void Fila_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FilaAsientos.CantidadAsientos))
            {
                GenerarMapaSala(); // Se vuelve a generar el mapa solo si cambia la cantidad de asientos
            }
        }
    }
}
