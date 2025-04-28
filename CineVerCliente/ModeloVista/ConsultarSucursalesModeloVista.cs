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
        private string _codigoPostal;

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
            _mainWindowModeloVista = mainWindowModeloVista;

            AgregarSucursalComando = new ComandoModeloVista(AgregarSucursal);
            EditarSucursalComando = new ComandoModeloVista(EditarSucursal);
            EliminarSucursalComando = new ComandoModeloVista(EliminarSucursal);
            AceptarComando = new ComandoModeloVista(AceptarEliminarSucursal);
            CancelarComando = new ComandoModeloVista(CancelarEliminarSucursal);

            MostrarMensajeConfirmacion = Visibility.Collapsed;

            Sucursales = new ObservableCollection<SucursalConsultada>
            {
                new SucursalConsultada { Nombre = "Sucursal 1", Estado = "Estado 1", Ciudad = "Ciudad 1", CodigoPostal = "12345" },
                new SucursalConsultada { Nombre = "Sucursal 2", Estado = "Estado 2", Ciudad = "Ciudad 2", CodigoPostal = "67890" },
                new SucursalConsultada { Nombre = "Sucursal 3", Estado = "Estado 3", Ciudad = "Ciudad 3", CodigoPostal = "54321" },
                new SucursalConsultada { Nombre = "Sucursal 4", Estado = "Estado 4", Ciudad = "Ciudad 4", CodigoPostal = "09876" },
                new SucursalConsultada { Nombre = "Sucursal 5", Estado = "Estado 5", Ciudad = "Ciudad 5", CodigoPostal = "13579" },
                new SucursalConsultada { Nombre = "Sucursal 6", Estado = "Estado 6", Ciudad = "Ciudad 6", CodigoPostal = "24680" }
            };
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

        public string CodigoPostal
        {
            get { return _codigoPostal; }
            set
            {
                _codigoPostal = value;
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
                _mainWindowModeloVista.CambiarModeloVista(new EditarSucursalModeloVista(_mainWindowModeloVista));
            }
        }

        public void EliminarSucursal(object obj)
        {
            if (obj is SucursalConsultada sucursal)
            {
                MostrarMensajeConfirmacion = Visibility.Visible;
            }
        }

        public void AceptarEliminarSucursal(object obj)
        {
            if (obj is SucursalConsultada sucursal)
            {
                MostrarMensajeConfirmacion = Visibility.Collapsed;
            }
        }

        public void CancelarEliminarSucursal(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }
    }
}
