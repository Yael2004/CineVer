using CineVerCliente.Vista;
using System;
using System.Collections.Generic;
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

        public ICommand SucursalComando {  get; }
        public ICommand EmpleadoComando { get; }
        public ICommand AgregarProductoDulceriaComando { get; }
        public ICommand CorteCaja { get; }
        public ICommand DevolverBoletoComando { get; }

        public object VistaActualModelo
        {
            get { return _vistaActual; }
            set
            {
                _vistaActual = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModeloVista()
        {
            _mainWindowModeloVista = this;
            SucursalComando = new ComandoModeloVista(Sucursales);
            EmpleadoComando = new ComandoModeloVista(ConsultarEmpleados);
            AgregarProductoDulceriaComando = new ComandoModeloVista(AgregarProductoDulceria);
            CorteCaja = new ComandoModeloVista(RealizarCorteCaja);
            DevolverBoletoComando = new ComandoModeloVista(DevolverBoleto);
            SucursalComando = new ComandoModeloVista(EditarSucursal);
            EmpleadoComando = new ComandoModeloVista(IniciarSesion);
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
            CambiarModeloVista(new ObtenerEstadisticasModeloVista(_mainWindowModeloVista));
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
    }
}
