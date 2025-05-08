using CineVerCliente.Modelo;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CineVerCliente.ModeloVista
{
    public class ObtenerEstadisticasModeloVista : BaseModeloVista
    {
        private int _anioSeleccionado;
        private int _mesSeleccionado;

        public string NombreMes { get; set; }
        public int Anio { get; set; }
        public List<VentaDetalle> Ventas { get; set; }

        private MainWindowModeloVista _mainWindowModeloVista;
        public SeriesCollection Coleccion { get; set; }
        public Func<double, string> YFormato { get; set; }
        public List<int> Anios { get; set; }

        public string[] Meses { get; set; } = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        private Visibility _mostrarTabla;
        private Visibility _mostrarGrafica;

        public ICommand MesClicComando { get; set; }
        public ICommand RegresarComando { get; set; }

        public int AnioSeleccionado
        {
            get => _anioSeleccionado;
            set
            {
                _anioSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarTabla
        {
            get => _mostrarTabla;
            set
            {
                _mostrarTabla = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarGrafica
        {
            get => _mostrarGrafica;
            set
            {
                _mostrarGrafica = value;
                OnPropertyChanged();
            }
        }

        public int MesSeleccionado
        {
            get => _mesSeleccionado;
            set
            {
                _mesSeleccionado = value;
                NombreMes = new DateTime(AnioSeleccionado, _mesSeleccionado, 1).ToString("MMMM", new System.Globalization.CultureInfo("es-MX"));
                Ventas = GenerarVentasDummy(_mesSeleccionado);
                OnPropertyChanged(nameof(MesSeleccionado));
                OnPropertyChanged(nameof(NombreMes));
                OnPropertyChanged(nameof(Ventas));
            }
        }

        public ObtenerEstadisticasModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            Anios = Enumerable.Range(2020, 6).ToList();
            AnioSeleccionado = DateTime.Now.Year;

            YFormato = value => value.ToString("C");

            MesClicComando = new ComandoGenericoModeloVista<ChartPoint>(MesClic);
            RegresarComando = new ComandoModeloVista(RegresarAGrafica);

            MostrarTabla = Visibility.Collapsed;
            MostrarGrafica = Visibility.Visible;

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

        private List<VentaDetalle> GenerarVentasDummy(int mes)
        {
            return new List<VentaDetalle>
            {
                new VentaDetalle { Dia = 1, Tipo = "Boletos", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },
                new VentaDetalle { Dia = 1, Tipo = "Dulcería", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },

                new VentaDetalle { Dia = 2, Tipo = "Boletos", InicioDia = 2000, FinDia = 10000, VentasTotales = 8000 },
                new VentaDetalle { Dia = 2, Tipo = "Dulcería", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },

                new VentaDetalle { Dia = 3, Tipo = "Boletos", InicioDia = 2000, FinDia = 25000, VentasTotales = 23000 },
                new VentaDetalle { Dia = 3, Tipo = "Dulcería", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },

                new VentaDetalle { Dia = 4, Tipo = "Boletos", InicioDia = 3000, FinDia = 12000, VentasTotales = 9000 },
                new VentaDetalle { Dia = 4, Tipo = "Dulcería", InicioDia = 2000, FinDia = 15000, VentasTotales = 13000 },

                new VentaDetalle { Dia = 5, Tipo = "Boletos", InicioDia = 5000, FinDia = 10300, VentasTotales = 5300 },
                new VentaDetalle { Dia = 5, Tipo = "Dulcería", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },

                new VentaDetalle { Dia = 6, Tipo = "Boletos", InicioDia = 4200, FinDia = 8800, VentasTotales = 4600 },
                new VentaDetalle { Dia = 6, Tipo = "Dulcería", InicioDia = 5000, FinDia = 15000, VentasTotales = 10000 },
            };
        }

        private void MesClic(ChartPoint punto)
        {
            if (punto == null) return;

            int mes = (int)punto.X + 1;
            MesSeleccionado = mes;

            MostrarTabla = Visibility.Visible;
            MostrarGrafica = Visibility.Collapsed;
        }

        private void RegresarAGrafica(object obj)
        {
            MostrarTabla = Visibility.Collapsed;
            MostrarGrafica = Visibility.Visible;
        }
    }
}
