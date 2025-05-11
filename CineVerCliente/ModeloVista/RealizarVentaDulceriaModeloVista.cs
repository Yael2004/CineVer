using CineVerCliente.DulceriaServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
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
    public class RealizarVentaDulceriaModeloVista : BaseModeloVista
    {
        public ObservableCollection<ProductoDulceria> Productos { get; set; }
        private string _promocion;
        private Visibility _mostrarMensajeCancelarOperacion = Visibility.Collapsed;
        private Visibility _mostrarMensajeConfirmarCambio = Visibility.Collapsed;
        private Visibility _mostrarVentanaVenta = Visibility.Collapsed;

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        private DulceriaServicioClient DulceriaServicioCliente;

        public ICommand RealizarVentaComando { get; }
        public ICommand ConfirmarVentaComando { get; }
        public ICommand CancelarVentaComando { get; }

        public ICommand CancelarComando { get; }
        public ICommand ConfirmarCancelacionComando { get; }
        public ICommand CancelarCancelacionComando { get; }

        public ICommand AceptarVentanaVentaComando { get; }
        public ICommand CancelarVentanaVentaComando { get; }
        public ICommand CrearCuentaComando { get; }

        public Visibility MostrarMensajeCancelarOperacion
        {
            get { return _mostrarMensajeCancelarOperacion; }
            set
            {
                _mostrarMensajeCancelarOperacion = value;
                OnPropertyChanged(nameof(MostrarMensajeCancelarOperacion));
            }
        }

        public Visibility MostrarMensajeConfirmarCambio
        {
            get { return _mostrarMensajeConfirmarCambio; }
            set
            {
                _mostrarMensajeConfirmarCambio = value;
                OnPropertyChanged(nameof(MostrarMensajeConfirmarCambio));
            }
        }

        public Visibility MostrarVentanaVenta
        {
            get { return _mostrarVentanaVenta; }
            set
            {
                _mostrarVentanaVenta = value;
                OnPropertyChanged(nameof(MostrarVentanaVenta));
            }
        }

        public string Promocion
        {
            get { return _promocion; }
            set
            {
                _promocion = value;
                OnPropertyChanged(nameof(Promocion));
            }
        }

        public RealizarVentaDulceriaModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            DulceriaServicioCliente = new DulceriaServicioClient();
            Productos = new ObservableCollection<ProductoDulceria>();
            RealizarVentaComando = new ComandoModeloVista(RealizarVenta);
            ConfirmarVentaComando = new ComandoModeloVista(ConfirmarVenta);
            CancelarVentaComando = new ComandoModeloVista(CancelarVenta);
            CancelarComando = new ComandoModeloVista(CancelarOperacion);
            ConfirmarCancelacionComando = new ComandoModeloVista(ConfirmarCancelacion);
            CancelarCancelacionComando = new ComandoModeloVista(CancelarCancelacion);
            AceptarVentanaVentaComando = new ComandoModeloVista(AceptarVentanaVenta);
            CancelarVentanaVentaComando = new ComandoModeloVista(CancelarVentanaVenta);
            CrearCuentaComando = new ComandoModeloVista(CrearCuenta);
            InicializarListaProductos();
        }

        private void InicializarListaProductos()
        {
            try
            {
                var productos = DulceriaServicioCliente.ObtenerProductosDulceria();
                if (productos != null)
                {
                    foreach (var producto in productos.Productos)
                    {
                        Productos.Add(new ProductoDulceria
                        {
                            Id = producto.IdProducto,
                            Nombre = producto.Nombre,
                            CostoUnitario = producto.CostoUnitario.ToString(),
                            PrecioVentaUnitario = producto.PrecioVentaUnitario.ToString(),
                            CantidadInventario = producto.CantidadInventario.ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Ha ocurrido un error inesperado");
            }
        }

        public void RealizarVenta(object obj)
        {
            MostrarVentanaVenta = Visibility.Visible;
        }

        public void ConfirmarVenta(object obj)
        {
            // Aquí puedes agregar la lógica para confirmar la venta
            // Por ejemplo, enviar la información al servicio o realizar alguna acción adicional
            MostrarVentanaVenta = Visibility.Collapsed;
        }

        public void CancelarVenta(object obj)
        {
            // Aquí puedes agregar la lógica para cancelar la venta
            // Por ejemplo, restablecer los campos o realizar alguna acción adicional
            MostrarVentanaVenta = Visibility.Collapsed;
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

        public void AceptarVentanaVenta(object obj)
        {
            // Aquí puedes agregar la lógica para aceptar la ventana de venta
            // Por ejemplo, enviar la información al servicio o realizar alguna acción adicional
            MostrarVentanaVenta = Visibility.Collapsed;
        }

        public void CancelarVentanaVenta(object obj)
        {
            // Aquí puedes agregar la lógica para cancelar la ventana de venta
            // Por ejemplo, restablecer los campos o realizar alguna acción adicional
            MostrarVentanaVenta = Visibility.Collapsed;
        }

        private void CrearCuenta(object obj)
        {
            // Aquí puedes agregar la lógica para crear una cuenta
            // Por ejemplo, abrir una ventana de registro o realizar alguna acción adicional
            //_mainWindowModeloVista.CambiarModeloVista(new CrearCuentaModeloVista(_mainWindowModeloVista));
        }
    }
}
