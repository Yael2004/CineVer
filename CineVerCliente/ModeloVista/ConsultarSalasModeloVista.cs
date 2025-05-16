using CineVerCliente.AsientoServicio;
using CineVerCliente.FilaServicio;
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
    public class ConsultarSalasModeloVista:BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ObservableCollection<SalaDTO> Salas { get; set; } = new ObservableCollection<SalaDTO>();
        public ICommand EditarSalaCommand { get; }
        public ICommand AgregarSalaCommand { get; }
        public ICommand EliminarSalaCommand { get; }
        public ICommand AceptarComando { get; }
        public ICommand CancelarComando { get; }
        private SalaDTO _salaSeleccionada;
        private string _nombreSala;
        public string NombreSala
        {
            get => _nombreSala;
            set
            {
                _nombreSala = value;
                OnPropertyChanged(nameof(NombreSala));
                var salasNombre = salaServicio.ObtenerSalasPorSucursalYNombre(1, _nombreSala);
                Console.WriteLine(_nombreSala);
                Salas.Clear();
                if (salasNombre.Salas != null)
                {
                    foreach (var sala in salasNombre.Salas)
                    {
                        Salas.Add(sala);
                    }
                }
            }
        }
        private SalaServicioClient salaServicio = new SalaServicioClient();
        public ConsultarSalasModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            EditarSalaCommand = new ComandoModeloVista(EditarSala);
            EliminarSalaCommand = new ComandoModeloVista(EliminarSala);
            AceptarComando = new ComandoModeloVista(AceptarEliminar);
            CancelarComando = new ComandoModeloVista(CancelarEliminar);
            _mainWindowModeloVista = mainWindowModeloVista;
            AgregarSalaCommand = new ComandoModeloVista(AgregarSala);
            
            var salasList = salaServicio.ObtenerSalasPorSucursal(1);
            Salas = new ObservableCollection<SalaDTO>(salasList.Salas);
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

        private void EditarSala(object obj)
        {
            if (obj is SalaDTO sala)
            {
                _salaSeleccionada = sala;
                CambiarModeloVista(new EditarSalaModeloVista(_mainWindowModeloVista, sala));
            }
        }

        private void EliminarSala(object obj)
        {
            if (obj is SalaDTO sala)
            {
                _salaSeleccionada = sala;
                MostrarMensajeConfirmar = true;
            }
        }
        private void EliminarFilasYAsientos()
        {
            var _filaServicio = new FilaServicioClient();
            var _asientoServicio = new AsientoServicioClient();
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
        private void AceptarEliminar(object obj)
        {
            if (_salaSeleccionada != null)
            {
                var salaServicio = new SalaServicioClient();
                EliminarFilasYAsientos();
                salaServicio.EliminarSala(_salaSeleccionada);
                Salas.Remove(_salaSeleccionada);
                MostrarMensajeConfirmar = false;
            }
        }
        private void CancelarEliminar(object obj)
        {
            MostrarMensajeConfirmar = false;
        }
        private void AgregarSala(object obj)
        {
            CambiarModeloVista(new AgregarSalaModeloVista(_mainWindowModeloVista));
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
            _mainWindowModeloVista.VistaActualModelo = nuevoModeloVista;
        }

    }
}
