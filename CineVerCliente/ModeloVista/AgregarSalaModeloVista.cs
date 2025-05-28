using CineVerCliente.AsientoServicio;
using CineVerCliente.FilaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarSalaModeloVista:BaseModeloVista
    {
        public ICommand GuardarCommand { get; }
        private SalaServicioClient _salaServicio = new SalaServicioClient();
        private AsientoServicioClient _asientoServicio = new AsientoServicioClient();
        private FilaServicioClient _filaServicio = new FilaServicioClient();
        public ICommand RegresarCommand { get; }

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
                ActualizarFilas();
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

        public AgregarSalaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            GuardarCommand = new ComandoModeloVista(Guardar);
            RegresarCommand = new ComandoModeloVista(Regresar);
            Filas = new ObservableCollection<FilaAsientos>();
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
        private void Guardar(Object obj)
        {
            if (ValidarCampos())
            {
                SalaDTO sala = new SalaDTO();
                sala.nombre = NombreSala;
                sala.descripcion = DescripcionSala;
                sala.estadoSala = "DISPONIBLE";
                sala.numeroFilas = NumeroFilas;
                sala.idSucursal = UsuarioEnLinea.Instancia.IdSucursal;
                string mensaje = _salaServicio.AgregarSala(sala);
                int idSala = _salaServicio.ObtenerIdSala(1, sala.nombre);
                foreach(var fila in Filas)
                {
                    FilaDTO filaDTO = new FilaDTO();
                    filaDTO.númeroFila = fila.NumeroFila;
                    filaDTO.numeroAsientos = fila.CantidadAsientos;
                    filaDTO.idSala = idSala;
                    _filaServicio.AgregarFila(filaDTO);
                    for (int i = 1; i <= fila.CantidadAsientos; i++)
                    {
                        AsientoDTO asientoDTO = new AsientoDTO();
                        char letra = (char)('A' + i - 1);
                        string letraColumna = letra.ToString();
                        asientoDTO.letraColumna = letraColumna;
                        int idFila = _filaServicio.ObtenerIdFila(idSala, fila.NumeroFila);
                        asientoDTO.estado = "DISPONIBLE";
                        asientoDTO.idFila = idFila;
                        _asientoServicio.AgregarAsiento(asientoDTO);
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

        public ObservableCollection<ObservableCollection<Helpers.Asiento>> MapaSala { get; set; }

        private void GenerarMapaSala()
        {
            MapaSala = new ObservableCollection<ObservableCollection<Helpers.Asiento>>();

            foreach (var fila in Filas)
            {
                var filaAsientos = new ObservableCollection<Helpers.Asiento>();
                for (int i = 1; i <= fila.CantidadAsientos; i++)
                {
                    filaAsientos.Add(new Helpers.Asiento
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
