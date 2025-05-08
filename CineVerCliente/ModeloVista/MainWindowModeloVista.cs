using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using CineVerCliente.Vista;
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
    public class MainWindowModeloVista : BaseModeloVista
    {
        private object _vistaActual;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public Visibility _promocionesMenu;
        public Visibility _empleadosMenu;
        public Visibility _reportesMenu;
        public Visibility _funcionesMenu;
        public Visibility _sucursalesMenu;

        public ICommand SucursalComando {  get; }
        public ICommand EmpleadoComando { get; }
        public ICommand AgregarProductoDulceriaComando { get; }
        public ICommand CorteCaja { get; }
        public ICommand BoletoComando { get; }

        public object VistaActualModelo
        {
            get { return _vistaActual; }
            set
            {
                _vistaActual = value;
                OnPropertyChanged();
            }
        }

        public Visibility PromocionesMenu
        {
            get { return _promocionesMenu; }
            set
            {
                _promocionesMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility EmpleadosMenu
        {
            get { return _empleadosMenu; }
            set
            {
                _empleadosMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ReportesMenu
        {
            get { return _reportesMenu; }
            set
            {
                _reportesMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility FuncionesMenu
        {
            get { return _funcionesMenu; }
            set
            {
                _funcionesMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility SucursalesMenu
        {
            get { return _sucursalesMenu; }
            set
            {
                _sucursalesMenu = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModeloVista()
        {
            _mainWindowModeloVista = this;
            CrearMenus();
            SucursalComando = new ComandoModeloVista(Sucursales);
            EmpleadoComando = new ComandoModeloVista(RegistrarEmpleado);
            AgregarProductoDulceriaComando = new ComandoModeloVista(AgregarProductoDulceria);
            CorteCaja = new ComandoModeloVista(RealizarCorteCaja);
            BoletoComando = new ComandoModeloVista(DevolverBoleto);
            SucursalComando = new ComandoModeloVista(Sucursales);
        }

        private void CrearMenus()
        {
            //string rol = UsuarioEnLinea.Instancia.Rol;
            string rol = "Gerente"; // Solo es para probar

            if (string.IsNullOrEmpty(rol))
            {
                PromocionesMenu = Visibility.Collapsed;
                EmpleadosMenu = Visibility.Collapsed;
                ReportesMenu = Visibility.Collapsed;
                FuncionesMenu = Visibility.Collapsed;
                SucursalesMenu = Visibility.Collapsed;
                Notificacion.Mostrar("Error al iniciar sesión", 4000);
                //Aqui regresarlo al loggin
            }
            else if (rol.Equals("Gerente"))
            {
                PromocionesMenu = Visibility.Visible;
                EmpleadosMenu = Visibility.Visible;
                ReportesMenu = Visibility.Visible;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Visible;
            }
            else if (rol.Equals("Empleado administrativo"))
            {
                PromocionesMenu = Visibility.Visible;
                EmpleadosMenu = Visibility.Visible;
                ReportesMenu = Visibility.Collapsed;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Collapsed;
            }
            else if (rol.Equals("Empleado operativo"))
            {
                PromocionesMenu = Visibility.Visible;
                EmpleadosMenu = Visibility.Collapsed;
                ReportesMenu = Visibility.Collapsed;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Collapsed;
            }
        }

        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
        }

        public void Sucursales(object obj)
        {
            CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
        }

        private void AgregarSucursal(object obj)
        {
            CambiarModeloVista(new AgregarSucursalModeloVista(_mainWindowModeloVista));
        }

        private void EditarSucursal(object obj)
        {
            CambiarModeloVista(new EditarSucursalModeloVista(_mainWindowModeloVista));
        }

        private void RegistrarEmpleado(object obj)
        {
            CambiarModeloVista(new RegistrarEmpleadoModeloVista(_mainWindowModeloVista));
        }

        private void ModificarEmpleado(object obj)
        {
            CambiarModeloVista(new ModificarEmpleadoModeloVista(_mainWindowModeloVista));
        }

        private void ConsultarEmpleados(object obj)
        {
            CambiarModeloVista(new ConsultarEmpleadosModeloVista(_mainWindowModeloVista));
        }

        private void AgregarProductoDulceria(object obj)
        {
            CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
        }

        private void RealizarCorteCaja(object obj)
        {
            CambiarModeloVista(new RealizarCorteCajaModeloVista(_mainWindowModeloVista));
        }

        private void DevolverBoleto(object obj)
        {
            CambiarModeloVista(new DevolverBoletoModeloVista(_mainWindowModeloVista));
        }

        private void RegistrarSocio(object obj)
        {
            CambiarModeloVista(new RegistrarSocioModeloVista(_mainWindowModeloVista));
        }

        private void ModificarSocio(object obj)
        {
            CambiarModeloVista(new ModificarSocioModeloVista(_mainWindowModeloVista));
        }

        private void ConsultarSocios(object obj)
        {
            CambiarModeloVista(new ConsultarSociosModeloVista(_mainWindowModeloVista));
        }

        private void RegistrarGasto(object obj)
        {
            CambiarModeloVista(new RegistrarGastoModeloVista(_mainWindowModeloVista));
        }

        private void IniciarSesion(object obj)
        {
            CambiarModeloVista(new IniciarSesionModeloVista(_mainWindowModeloVista));
        }

        private void VenderBoleto(object obj)
        {
            CambiarModeloVista(new VenderBoletoModeloVista(_mainWindowModeloVista));
        }
    }
}
