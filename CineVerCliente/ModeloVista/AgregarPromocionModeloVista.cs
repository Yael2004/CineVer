using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.VentaServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class AgregarPromocionModeloVista : BaseModeloVista
    {
        private string _nombre { get; set; }
        private int _productosNecesarios { get; set; }
        private int _productosAPagar { get; set; }
        private List<string> _tipoPromocion { get; set; }
        private List<string> _producto { get; set; }
        private string _tipoPromocionSeleccionado { get; set; }
        private string _productoSeleccionado { get; set; }
        private bool _lunes;
        private bool _martes;
        private bool _miercoles;
        private bool _jueves;
        private bool _viernes;
        private bool _sabado;
        private bool _domingo;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _MostrarMensajeConfirmarPromocion = Visibility.Collapsed;
        private VentaServicioClient VentaServicioClient { get; set; } = new VentaServicioClient();
        private DulceriaServicioClient DulceriaServicioClient { get; set; } = new DulceriaServicioClient();


        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand AceptarPromocionComando { get; }
        public ICommand ConfirmarAceptarPromocionComando { get; }
        public ICommand CancelarAceptarPromocionComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }


        private bool _botonAceptarHabilitado;

        public bool BotonAceptarHabilitado
        {
            get => _botonAceptarHabilitado;
            set
            {
                _botonAceptarHabilitado = value;
                OnPropertyChanged();
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }

        public int ProductosNecesarios
        {
            get { return _productosNecesarios; }
            set
            {
                _productosNecesarios = value;
                OnPropertyChanged();
            }
        }

        public int ProductosAPagar
        {
            get { return _productosAPagar; }
            set
            {
                _productosAPagar = value;
                OnPropertyChanged();
            }
        }

        public List<string> TipoPromocion
        {
            get { return _tipoPromocion; }
            set
            {
                _tipoPromocion = value;
                OnPropertyChanged();
            }
        }

        public List<string> Producto
        {
            get { return _producto; }
            set
            {
                _producto = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmarPromocion
        {
            get { return _MostrarMensajeConfirmarPromocion; }
            set
            {
                _MostrarMensajeConfirmarPromocion = value;
                OnPropertyChanged();
            }
        }

        public string TipoPromocionSeleccionado
        {
            get => _tipoPromocionSeleccionado;
            set
            {
                if (_tipoPromocionSeleccionado != value)
                {
                    _tipoPromocionSeleccionado = value;
                    OnPropertyChanged();

                    if (string.IsNullOrEmpty(value))
                    {
                        BotonAceptarHabilitado = false;
                    }
                    else if (value == "Dulcería")
                    {
                        BotonAceptarHabilitado = true;

                        try
                        {
                            var respuesta = DulceriaServicioClient.ObtenerNombresProductos(2);
                            if (respuesta == null || !respuesta.Resultado.EsExitoso)
                            {
                                Notificacion.Mostrar("Ha ocurrido un error inesperado");
                            }
                            else
                            {
                                Producto = respuesta.NombresProductos.ToList();
                                ProductoSeleccionado = Producto.FirstOrDefault();
                            }
                        }
                        catch (Exception)
                        {
                            Notificacion.Mostrar("Ha ocurrido un error inesperado");
                        }
                    }
                    else if (value == "Taquilla")
                    {
                        Producto = new List<string> { "Boleto" };
                        BotonAceptarHabilitado = true;
                        ProductoSeleccionado = "Boleto";
                    }
                }
            }
        }

        public string ProductoSeleccionado
        {
            get { return _productoSeleccionado; }
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged();
            }
        }

        public bool Lunes
        {
            get { return _lunes; }
            set
            {
                _lunes = value;
                OnPropertyChanged();
            }
        }

        public bool Martes
        {
            get { return _martes; }
            set
            {
                _martes = value;
                OnPropertyChanged();
            }
        }

        public bool Miercoles
        {
            get { return _miercoles; }
            set
            {
                _miercoles = value;
                OnPropertyChanged();
            }
        }

        public bool Jueves
        {
            get { return _jueves; }
            set
            {
                _jueves = value;
                OnPropertyChanged();
            }
        }

        public bool Viernes
        {
            get { return _viernes; }
            set
            {
                _viernes = value;
                OnPropertyChanged();
            }
        }

        public bool Sabado
        {
            get { return _sabado; }
            set
            {
                _sabado = value;
                OnPropertyChanged();
            }
        }

        public bool Domingo
        {
            get { return _domingo; }
            set
            {
                _domingo = value;
                OnPropertyChanged();
            }
        }

        public AgregarPromocionModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            AceptarPromocionComando = new ComandoModeloVista(AceptarPromocion);
            ConfirmarAceptarPromocionComando = new ComandoModeloVista(ConfirmarAceptarPromocion);
            CancelarAceptarPromocionComando = new ComandoModeloVista(CancelarAceptarPromocion);
            CancelarOperacionComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            TipoPromocion = new List<string> { "Taquilla", "Dulcería" };
        }

        private void AceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Visible;
        }

        private void ConfirmarAceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Collapsed;
            try
            {
                var producto = new PromocionDTO
                {
                    Tipo = TipoPromocionSeleccionado,
                    IdSucursal = 2,
                    Producto = ProductoSeleccionado,
                    NumeroProductosNecesarios = ProductosNecesarios,
                    NumeroProductosPagar = ProductosAPagar,
                    LunesAplica = Lunes,
                    MartesAplica = Martes,
                    MiercolesAplica = Miercoles,
                    JuevesAplica = Jueves,
                    ViernesAplica = Viernes,
                    SabadoAplica = Sabado,
                    DomingoAplica = Domingo,
                    Nombre = Nombre
                };
                var respuesta = VentaServicioClient.RegistrarPromocion(producto);

                if (respuesta == null || !respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Ha ocurrido un error inesperado");
                }
                else
                {
                    Notificacion.Mostrar("Se ha registrado la promoción correctamente");
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }

        private void CancelarAceptarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Collapsed;
        }

        private void CancelarOperacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        private void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }
    }
}
