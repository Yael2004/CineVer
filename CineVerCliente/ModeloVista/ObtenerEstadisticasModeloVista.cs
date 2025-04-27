using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CineVerCliente.ModeloVista
{
    public class ObtenerEstadisticasModeloVista : BaseModeloVista
    {
        private int _anioSeleccionado;
        
        private MainWindowModeloVista _mainWindowModeloVista;
        public SeriesCollection Coleccion { get; set; }
        public Func<double, string> YFormato { get; set; }
        public List<int> Anios { get; set; }

        public string[] Meses { get; set; } = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public int AnioSeleccionado
        {
            get => _anioSeleccionado;
            set
            {
                _anioSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public ObtenerEstadisticasModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            Anios = Enumerable.Range(2020, 6).ToList();
            AnioSeleccionado = DateTime.Now.Year;

            YFormato = value => value.ToString("C");

            Coleccion = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Dulcería",
                    Values = new ChartValues<double> { 100, 200, 150, 250, 300, 300, 150, 50, 250, 500, 100, 200 },
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E5A000")),
                    MaxColumnWidth = 30,
                    ColumnPadding = 5
                },
                new ColumnSeries
                {
                    Title = "Boletos",
                    Values = new ChartValues<double> { 80, 180, 120, 210, 270, 300, 200, 150, 170, 240, 170, 300 },
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#441FEC")),
                    MaxColumnWidth = 30,
                    ColumnPadding = 5
                }
            };
        }
    }
}
