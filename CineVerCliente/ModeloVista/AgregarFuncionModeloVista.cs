using CineVerCliente.FuncionServicio;
using CineVerCliente.PeliculaServicio;
using CineVerCliente.SalaServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarFuncionModeloVista : BaseModeloVista
    {
        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ICommand GuardarCommand { get; }
        public ICommand CancelarCommand { get; }
        private string _nombrePelicula;
        public string NombrePelicula
        {
            get => _nombrePelicula;
            set
            {
                _nombrePelicula = value;
                OnPropertyChanged(nameof(NombrePelicula));

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

        }
        private void Guardar(Object obj)
        {

        }
        private void Regresar(Object obj)
        {

        }
    }
}
