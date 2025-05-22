using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CineVerCliente.ModeloVista
{
    public class CantidadMermaModeloVista : BaseModeloVista
    {
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeAceptarOperacion = Visibility.Collapsed;
        public ICommand CancelarComando { get; set; }
        public ICommand ConfirmarComando { get; set; }
        public ICommand CancelarCancelacionComando { get; set; }
        public ICommand ConfirmarCancelacionComando { get; set; }
        public ICommand ConfirmarConfirmacionComando { get; set; }
        public ICommand CancelarConfirmacionComando { get; set; }
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
        public string CantidadInventario { get; set; }
        public string CostoUnitario { get; set; }
        public string PrecioVentaUnitario { get; set; }
        public string CantidadMerma { get; set; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient _dulceriaServicioCliente;

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeAceptarOperacion
        {
            get { return _mostrarMensajeAceptarOperacion; }
            set
            {
                _mostrarMensajeAceptarOperacion = value;
                OnPropertyChanged();
            }
        }

        public CantidadMermaModeloVista(MainWindowModeloVista mainWindowModeloVista, ProductoDulceria producto)
        { 
            _mainWindowModeloVista = mainWindowModeloVista;
            _dulceriaServicioCliente = new DulceriaServicioClient();
            IdProducto = producto.Id;
            Nombre = producto.Nombre;
            Imagen = producto.Imagen;
            CantidadInventario = producto.CantidadInventario;
            CostoUnitario = producto.CostoUnitario;
            PrecioVentaUnitario = producto.PrecioVentaUnitario;
            CancelarComando = new ComandoModeloVista(Cancelar);
            ConfirmarComando = new ComandoModeloVista(Confirmar);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            ConfirmarConfirmacionComando = new ComandoModeloVista(ConfirmarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);
        }

        public void Cancelar(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        public void Confirmar(object obj)
        {
            MostrarMensajeAceptarOperacion = Visibility.Visible;
        }

        public void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }

        public void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
        }

        public void ConfirmarConfirmacion(object obj)
        {
            if (string.IsNullOrWhiteSpace(CantidadMerma))
            {
                CantidadMerma = "0";
            }

            try 
            {
                if (int.TryParse(CantidadMerma, out int cantidadMerma))
                {
                    if (cantidadMerma == 0)
                    {
                        Notificacion.Mostrar("Se ha eliminado el producto correctamente");
                        MostrarMensajeAceptarOperacion = Visibility.Collapsed;
                    }
                    else
                    {
                        var resultado = _dulceriaServicioCliente.ReportarMerma(IdProducto, cantidadMerma);
                        if (resultado.EsExitoso)
                        {
                            MostrarMensajeAceptarOperacion = Visibility.Collapsed;
                            _mainWindowModeloVista.CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
                            Notificacion.Mostrar("Se ha eliminado el producto correctamente");
                        }
                        else
                        {
                            Notificacion.Mostrar("Ha ocurrido un error inesperado");
                        }
                    }
                }
                else
                {
                    Notificacion.Mostrar("Ha ocurrido un error inesperado");
                }
            }
            catch (Exception)
            {
                Notificacion.MostrarExcepcion();
            }
        }

        public void CancelarConfirmacion(object obj)
        {
            MostrarMensajeAceptarOperacion = Visibility.Collapsed;
        }

        public ImageSource ImagenProducto
        {
            get
            {
                if (Imagen == null || Imagen.Length == 0)
                    return null;

                var image = new BitmapImage();
                using (var mem = new MemoryStream(Imagen))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = mem;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
        }
    }
}
