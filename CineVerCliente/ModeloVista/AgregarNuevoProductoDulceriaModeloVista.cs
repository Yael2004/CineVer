using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
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
    public class AgregarNuevoProductoDulceriaModeloVista : BaseModeloVista
    {
        private string _nombreProducto;
        private int _cantidadInventario;
        private float _costoUnitario;
        private float _precioVentaUnitario;
        private byte[] _imagenProducto;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarProducto = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient DulceriaServicioCliente;

        public ICommand AgregarNuevoProductoComando { get; }
        public ICommand AceptarNuevoProductoComando { get; }
        public ICommand CancelarNuevoProductoComando { get; }
        public ICommand CancelarOperacionComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }
        public ICommand SeleccionarImagenComando { get; }


        public string NombreProducto
        {
            get { return _nombreProducto; }
            set
            {
                _nombreProducto = value;
                OnPropertyChanged();
            }
        }

        public int CantidadInventario
        {
            get { return _cantidadInventario; }
            set
            {
                _cantidadInventario = value;
                OnPropertyChanged();
            }
        }

        public float CostoUnitario
        {
            get { return _costoUnitario; }
            set
            {
                _costoUnitario = value;
                OnPropertyChanged();
            }
        }

        public float PrecioVentaUnitario
        {
            get { return _precioVentaUnitario; }
            set
            {
                _precioVentaUnitario = value;
                OnPropertyChanged();
            }
        }

        public byte[] ImagenProducto
        {
            get { return _imagenProducto; }
            set
            {
                _imagenProducto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImagenProductoPreview));
            }
        }

        public ImageSource ImagenProductoPreview
        {
            get
            {
                if (_imagenProducto == null || _imagenProducto.Length == 0)
                    return null;

                var image = new BitmapImage();
                using (var ms = new MemoryStream(_imagenProducto))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
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

        public Visibility MostrarMensajeConfirmarProducto
        {
            get { return _mostrarMensajeConfirmarProducto; }
            set
            {
                _mostrarMensajeConfirmarProducto = value;
                OnPropertyChanged();
            }
        }

        public AgregarNuevoProductoDulceriaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            DulceriaServicioCliente = new DulceriaServicioClient(); 
            AgregarNuevoProductoComando = new ComandoModeloVista(AgregarNuevoProducto);
            AceptarNuevoProductoComando = new ComandoModeloVista(AceptarNuevoProducto);
            CancelarNuevoProductoComando = new ComandoModeloVista(CancelarNuevoProducto);
            CancelarOperacionComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            SeleccionarImagenComando = new ComandoModeloVista(SeleccionarImagen);
        }

        private void AgregarNuevoProducto(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Visible;
        }

        private void SeleccionarImagen(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagenProducto = System.IO.File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void AceptarNuevoProducto(object obj)
        {
            try
            {
                ProductoDulceriaDTO nuevoProducto = new ProductoDulceriaDTO
                {
                    Nombre = NombreProducto,
                    IdSucursal = 2,
                    CantidadInventario = CantidadInventario,
                    CostoUnitario = (decimal)CostoUnitario,
                    PrecioVentaUnitario = (decimal)PrecioVentaUnitario,
                    Imagen = ImagenProducto
                };
                var resultado = DulceriaServicioCliente.AgregarProductoDulceria(nuevoProducto);
                if (resultado.EsExitoso)
                {
                    Notificacion.Mostrar("Producto agregado exitosamente.");
                    _mainWindowModeloVista.CambiarModeloVista(new AgregarProductoDulceriaModeloVista(_mainWindowModeloVista));
                }
                else
                {
                    Notificacion.Mostrar("Error al agregar el producto.");
                }
            } 
            catch (Exception)
            {
                Notificacion.Mostrar("Error al agregar el producto" );
                return;
            }

            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
        }

        private void CancelarNuevoProducto(object obj)
        {
            MostrarMensajeConfirmarProducto = Visibility.Collapsed;
        }

        private void CancelarOperacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Visible;
        }

        private void ConfirmarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new AgregarProductoDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void CancelarCancelacion(object obj)
        {
            MostrarMensajeCancelarOperacion = Visibility.Collapsed;
        }
    }
}
