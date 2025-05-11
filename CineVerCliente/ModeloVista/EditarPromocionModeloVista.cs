using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.DulceriaServicio;
using CineVerCliente.VentaServicio;

namespace CineVerCliente.ModeloVista
{
    public class EditarPromocionModeloVista : BaseModeloVista
    {
        private string _nombre { get; set; }
        private int _productosNecesarios { get; set; }
        private int _productosAPagar { get; set; }
        private List<string> _tipoPromocion { get; set; }
        private List<string> _producto { get; set; }
        private List<PromocionDTO> _promociones { get; set; } = new List<PromocionDTO>();
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

        public ICommand EditarPromocionComando { get; }
        public ICommand ConfirmarEditarPromocionComando { get; }
        public ICommand CancelarEditarPromocionComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        private bool _botonAceptarHabilitado;

        private List<string> _nombresPromociones;
        public List<string> NombresPromociones
        {
            get => _nombresPromociones;
            set
            {
                _nombresPromociones = value;
                OnPropertyChanged();
            }
        }

        private string _nombrePromocionSeleccionado;
        public string NombrePromocionSeleccionado
        {
            get => _nombrePromocionSeleccionado;
            set
            {
                _nombrePromocionSeleccionado = value;
                OnPropertyChanged();
                CargarDatosDePromocionSeleccionada();
            }
        }

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
                    else if (value == "Cancelada")
                    {
                        Producto = new List<string>();
                        BotonAceptarHabilitado = true;
                        ProductoSeleccionado = null;
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

        public EditarPromocionModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            EditarPromocionComando = new ComandoModeloVista(EditarPromocion);
            ConfirmarEditarPromocionComando = new ComandoModeloVista(ConfirmarEditarPromocion);
            CancelarEditarPromocionComando = new ComandoModeloVista(CancelarEditarPromocion);
            CancelarOperacionComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            TipoPromocion = new List<string> { "Taquilla", "Dulcería", "Cancelada" };
            InicializarPromociones();
        }

        private void InicializarPromociones()
        {
            try
            {
                var respuesta = VentaServicioClient.ObtenerPromociones(2);
                if (respuesta == null || !respuesta.Result.EsExitoso)
                {
                    Notificacion.Mostrar("Ha ocurrido un error inesperado");
                }
                else
                {
                    _promociones = respuesta.Promociones.ToList();
                    NombresPromociones = _promociones.Select(p => p.Nombre).ToList();
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }

        private void CargarDatosDePromocionSeleccionada()
        {
            var promocion = _promociones.FirstOrDefault(p => p.Nombre == NombrePromocionSeleccionado);
            if (promocion != null)
            {
                Nombre = promocion.Nombre;
                TipoPromocionSeleccionado = promocion.Tipo;
                ProductoSeleccionado = promocion.Producto;
                ProductosNecesarios = promocion.NumeroProductosNecesarios;
                ProductosAPagar = promocion.NumeroProductosPagar;
                Lunes = promocion.LunesAplica;
                Martes = promocion.MartesAplica;
                Miercoles = promocion.MiercolesAplica;
                Jueves = promocion.JuevesAplica;
                Viernes = promocion.ViernesAplica;
                Sabado = promocion.SabadoAplica;
                Domingo = promocion.DomingoAplica;
            }
        }


        private void EditarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Visible;
        }

        private void ConfirmarEditarPromocion(object obj)
        {
            MostrarMensajeConfirmarPromocion = Visibility.Collapsed;

            try
            {
                var promocionExistente = _promociones.FirstOrDefault(p => p.Nombre == NombrePromocionSeleccionado);

                if (promocionExistente == null)
                {
                    Notificacion.Mostrar("No se encontró la promoción seleccionada.");
                    return;
                }

                var promocionEditada = new PromocionDTO
                {
                    IdPromocion = promocionExistente.IdPromocion,
                    Nombre = Nombre,
                    Tipo = TipoPromocionSeleccionado,
                    Producto = ProductoSeleccionado,
                    NumeroProductosNecesarios = ProductosNecesarios,
                    NumeroProductosPagar = ProductosAPagar,
                    LunesAplica = Lunes,
                    MartesAplica = Martes,
                    MiercolesAplica = Miercoles,
                    JuevesAplica = Jueves,
                    ViernesAplica = Viernes,
                    SabadoAplica = Sabado,
                    DomingoAplica = Domingo
                };

                var respuesta = VentaServicioClient.ActualizarPromocion(promocionEditada);

                if (respuesta == null || !respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("No se pudo editar la promoción.");
                }
                else
                {
                    Notificacion.Mostrar("La promoción se actualizó correctamente.");
                    InicializarPromociones();
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error inesperado al editar la promoción.");
            }
        }


        private void CancelarEditarPromocion(object obj)
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
