using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                GenerarFilas();
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
            Filas.Clear();
            for (int i = 1; i <= NumeroFilas; i++)
            {
                Filas.Add(new FilaAsientos { NumeroFila = i });
            }
        }
        private void Guardar(Object obj)
        {

        }

        private void Regresar(Object obj)
        {
            
        }

        private void OcultarCamposVacios()
        {
            NombreSalaVacio = Visibility.Collapsed;
            DescripcionSalaVacio = Visibility.Collapsed;
            NumeroFilasVacio = Visibility.Collapsed;
        }

    }
}
