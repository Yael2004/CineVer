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
    public class ConsultarSucursalesModeloVista : BaseModeloVista
    {
        private string _nombre;
        private string _estado;
        private string _ciudad;
        private string _estadoSucursal;

        private int _idSucursal;

        private ObservableCollection<SucursalConsultada> _sucursales;

        Visibility _mostrarMensajeConfirmacion;

        public ICommand AgregarSucursalComando { get; }
        public ICommand EditarSucursalComando { get; }
        public ICommand EliminarSucursalComando { get; }
        public ICommand AceptarComando { get; }
        public ICommand CancelarComando { get; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;
        public ConsultarSucursalesModeloVista(MainWindowModeloVista mainWindowModeloVista) 
        { 
            MostrarMensajeConfirmacion = Visibility.Collapsed;

            _mainWindowModeloVista = mainWindowModeloVista;

            AgregarSucursalComando = new ComandoModeloVista(AgregarSucursal);
            EditarSucursalComando = new ComandoModeloVista(EditarSucursal);
            EliminarSucursalComando = new ComandoModeloVista(EliminarSucursal);
            AceptarComando = new ComandoModeloVista(AceptarEliminarSucursal);
            CancelarComando = new ComandoModeloVista(CancelarEliminarSucursal);


            CargarSucursales();
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

        public string Estado
        {
            get { return _estado; }
            set
            {
                _estado = value;
                OnPropertyChanged();
            }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set
            {
                _ciudad = value;
                OnPropertyChanged();
            }
        }

        public string EstadoSucursal
        {
            get { return _estadoSucursal; }
            set
            {
                _estadoSucursal = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SucursalConsultada> Sucursales
        {
            get { return _sucursales; }
            set
            {
                _sucursales = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarMensajeConfirmacion
        {
            get { return _mostrarMensajeConfirmacion; }
            set
            {
                _mostrarMensajeConfirmacion = value;
                OnPropertyChanged();
            }
        }

        public void AgregarSucursal(object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new AgregarSucursalModeloVista(_mainWindowModeloVista));
        }

        public void EditarSucursal(object obj)
        {

            if (obj is SucursalConsultada sucursal)
            {
                var editarSucursal = new EditarSucursalModeloVista(_mainWindowModeloVista);
                editarSucursal.CargarSucursal(sucursal);
                _mainWindowModeloVista.CambiarModeloVista(editarSucursal);
            }
        }

        public void EliminarSucursal(object obj)
        {
            if (obj is SucursalConsultada sucursal)
            {
                MostrarMensajeConfirmacion = Visibility.Visible;
                _idSucursal = sucursal.IdSucursal;
            }
        }

        public void AceptarEliminarSucursal(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;

            try { 
                var cliente = new SucursalServicio.SucursalServicioClient();
                var respuesta = cliente.CerrarSucursal(_idSucursal);

                if (respuesta.EsExitoso)
                {
                    Notificacion.Mostrar("Sucursal eliminada exitosamente.");
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                    CargarSucursales();
                }
                else
                {
                    Notificacion.Mostrar("Error al eliminar la sucursal.");
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
                MostrarMensajeConfirmacion = Visibility.Collapsed;
            }
        }

        public void CancelarEliminarSucursal(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }

        private async void CargarSucursales()
        {
            try { 
                var cliente = new SucursalServicio.SucursalServicioClient();
                var respuesta = await cliente.ObtenerSucursalesAsync();

                Sucursales = new ObservableCollection<SucursalConsultada>();
                foreach (var sucursal in respuesta.Sucursales)
                {
                    Sucursales.Add(new SucursalConsultada
                    {
                        IdSucursal = sucursal.IdSucursal,
                        Nombre = sucursal.Nombre,
                        Estado = sucursal.Estado,
                        Ciudad = sucursal.Ciudad,
                        Calle = sucursal.Calle,
                        CodigoPostal = sucursal.CodigoPostal,
                        Numero = sucursal.NumeroEnLaCalle,
                        HoraApertura = sucursal.HoraApertura,
                        HoraCierre = sucursal.HoraCierre,
                        EstadoSucursal = sucursal.EstadoSucursal
                    });
                }
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }
        }
    }
}
