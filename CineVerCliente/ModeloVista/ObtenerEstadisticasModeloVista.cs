using CineVerCliente.Helpers;
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
                GenerarVentasAnio();
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
        }

        private async void GenerarVentasMes(int mes)
        {
            try
            {
                var cliente = new VentaServicio.VentaServicioClient();
                var resultado = await cliente.ObtenerVentasPorMesAsync(mes, AnioSeleccionado, 1);

                if (resultado == null)
                {
                    return;
                }

                Ventas = resultado.Ventas.Select(v => new VentaDetalle
                {
                    Fecha = v.Fecha,
                    Tipo = v.TIpoVenta,
                    Total = v.Total
                }).ToList();

                var ventasDiarias = Ventas
                    .GroupBy(v => new { Dia = v.Fecha.Day, Tipo = v.Tipo })
                    .Select(g => new VentaDetalle
                    {
                        Dia = g.Key.Dia,
                        Tipo = g.Key.Tipo,
                        VentasTotales = g.Sum(v => v.Total),
                        InicioDia = g.Min(v => v.Fecha),
                        FinDia = g.Max(v => v.Fecha)
                    })
                    .ToList();


                Ventas = ventasDiarias;
                OnPropertyChanged(nameof(Ventas));

                MostrarTabla = Visibility.Visible;
                MostrarGrafica = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }
        }


        private async void GenerarVentasAnio()
        {
            try
            {
                var cliente = new VentaServicio.VentaServicioClient();
                var resultado = await cliente.ObtenerVentasPorAnioAsync(AnioSeleccionado, 1);

                if (resultado == null)
                {
                    return;
                }

                Ventas = resultado.Ventas.Select(v => new VentaDetalle
                {
                    Fecha = v.Fecha,
                    Tipo = v.TIpoVenta,
                    Total = v.Total
                }).ToList();

                var valoresDulceria = new List<decimal>(new decimal[12]);
                var valoresBoletos = new List<decimal>(new decimal[12]);

                foreach (var venta in Ventas)
                {
                    if (venta.Fecha.Month >= 1 && venta.Fecha.Month <= 12)
                    {
                        int mes = venta.Fecha.Month - 1;
                        if (venta.Tipo.Contains("Dulce"))
                            valoresDulceria[mes] += venta.Total;
                        else if (venta.Tipo.Contains("Bole"))
                            valoresBoletos[mes] += venta.Total;
                    }
                }

                Coleccion = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Dulcería",
                        Values = new ChartValues<decimal>(valoresDulceria),
                        Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#E5A000")),
                        MaxColumnWidth = 30,
                        ColumnPadding = 5
                    },
                    new ColumnSeries
                    {
                        Title = "Boletos",
                        Values = new ChartValues<decimal>(valoresBoletos),
                        Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#441FEC")),
                        MaxColumnWidth = 30,
                        ColumnPadding = 5
                    }
                };

                OnPropertyChanged(nameof(Coleccion));
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }
        }

        private void MesClic(ChartPoint punto)
        {
            if (punto == null) return;

            int mes = (int)punto.X + 1;
            MesSeleccionado = mes;

            GenerarVentasMes(mes);

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
