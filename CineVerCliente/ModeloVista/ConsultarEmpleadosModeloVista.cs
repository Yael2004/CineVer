using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineVerCliente.ModeloVista
{
    public class ConsultarEmpleadosModeloVista : BaseModeloVista
    {
        private string _textoBusqueda;

        private ObservableCollection<EmpleadoConsultado> _todosLosElementos;
        private ObservableCollection<EmpleadoConsultado> _elementosFiltrados;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public string TextoBusqueda
        {
            get { return _textoBusqueda; }
            set
            {
                _textoBusqueda = value;
                OnPropertyChanged();
                FiltrarElementos();
            }
        }

        public ObservableCollection<EmpleadoConsultado> TodosLosElementos
        {
            get { return _todosLosElementos; }
            set
            {
                _todosLosElementos = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmpleadoConsultado> ElementosFiltrados
        {
            get { return _elementosFiltrados; }
            set
            {
                _elementosFiltrados = value;
                OnPropertyChanged();
            }
        }

        public ConsultarEmpleadosModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            _todosLosElementos = new ObservableCollection<EmpleadoConsultado>
            {
                new EmpleadoConsultado { Nombre = "Goku", Matricula = "CNVX2985637" },
                new EmpleadoConsultado { Nombre = "Vegeta", Matricula = "CNVX2876509" },
                new EmpleadoConsultado { Nombre = "Gohan", Matricula = "CNVX2946527" },
            };

            ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosElementos);
        }

        private void FiltrarElementos()
        {
            if (string.IsNullOrWhiteSpace(TextoBusqueda))
            {
                ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(_todosLosElementos);
            }
            else
            {
                var filtrados = _todosLosElementos
                    .Where(empleado => empleado.Nombre.ToLower().Contains(TextoBusqueda.ToLower()) ||
                                empleado.Matricula.ToLower().Contains(TextoBusqueda.ToLower())).ToList();

                ElementosFiltrados = new ObservableCollection<EmpleadoConsultado>(filtrados);
            }
        }
    }
}
