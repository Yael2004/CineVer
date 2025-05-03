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

        private async void CargarSucursales()
        {
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
                    HoraCierre = sucursal.HoraCierre
                });
            }
        }
    }
}
