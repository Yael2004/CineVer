﻿using CineVerCliente.Helpers;
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
        public Visibility _sociosMenu;
        public Visibility _dulceriaMenu;
        public Visibility _ventasMenu;
        public Visibility _boletosMenu;
        public Visibility _funcionesMenu;
        public Visibility _sucursalesMenu;
        public Visibility _sesionMenu;
        public Visibility _salasMenu;
        public Visibility _agregarPromocionMenu;
        public Visibility _editarPromocionMenu;
        public Visibility _registrarEmpleadoMenu;
        public Visibility _consultarEmpleadoMenu;
        public Visibility _registrarSocioMenu;
        public Visibility _consultarSociosMenu;
        public Visibility _corteDeCajaMenu;
        public Visibility _obtenerEstadisticasMenu;
        public Visibility _registrarGastoMenu;
        public Visibility _agregarInvetarioProductosMenu;
        public Visibility _editarProductosMenu;
        public Visibility _realizarVentaMenu;
        public Visibility _reportarMermaMenu;
        public Visibility _consultarSucursalesMenu;
        public Visibility _agregarSucursalMenu;
        public Visibility _carteleraMenu;

        public ICommand ConsultarSucursalesComando {  get; }
        public ICommand AgregarSucursalComando {  get; }
        public ICommand RegistrarEmpleadoComando {  get; }
        public ICommand ConsultarEmpleadosComando {  get; }
        public ICommand ReportarMermaProductoComando {  get; }
        public ICommand RealizarCorteCajaComando {  get; }
        public ICommand DevolverBoletoComando {  get; }
        public ICommand RegistrarSocioComando {  get; }
        public ICommand ConsultarSociosComando {  get; }
        public ICommand RegistrarGastoComando {  get; }
        public ICommand AgregarProductoDulceriaComando { get; }
        public ICommand EditarProductoDulceriaComando { get; }
        public ICommand RealizarVentaDulceriaComando { get; }
        public ICommand AgregarPromocionComando { get; }
        public ICommand EditarPromocionComando { get; }
        public ICommand CerrarSesionComando { get; }
        public ICommand ObtenerEstadisticasComando { get; }
        public ICommand ConsultarFuncionesComando { get; }
        public ICommand ConsultarSalasComando { get; }
        public ICommand ConsultarPeliculasComando { get; }

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

        public Visibility CarteleraMenu
        {
            get { return _carteleraMenu; }
            set
            {
                _carteleraMenu = value;
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

        public Visibility SociosMenu
        {
            get { return _sociosMenu; }
            set
            {
                _sociosMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility DulceriaMenu
        {
            get { return _dulceriaMenu; }
            set
            {
                _dulceriaMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility VentasMenu
        {
            get { return _ventasMenu; }
            set
            {
                _ventasMenu = value;
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

        public Visibility BoletosMenu
        {
            get { return _boletosMenu; }
            set
            {
                _boletosMenu = value;
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

        public Visibility SesionMenu
        {
            get { return _sesionMenu; }
            set
            {
                _sesionMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility SalasMenu
        {
            get { return _salasMenu; }
            set
            {
                _salasMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility AgregarPromocionMenu
        {
            get { return _agregarPromocionMenu; }
            set
            {
                _agregarPromocionMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility EditarPromocionMenu
        {
            get { return _editarPromocionMenu; }
            set
            {
                _editarPromocionMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility RegistrarEmpleadoMenu
        {
            get { return _registrarEmpleadoMenu; }
            set
            {
                _registrarEmpleadoMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ConsultarEmpleadoMenu
        {
            get { return _consultarEmpleadoMenu; }
            set
            {
                _consultarEmpleadoMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility RegistrarSocioMenu
        {
            get { return _registrarSocioMenu; }
            set
            {
                _registrarSocioMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ConsultarSociosMenu
        {
            get { return _consultarSociosMenu; }
            set
            {
                _consultarSociosMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility CorteDeCajaMenu
        {
            get { return _corteDeCajaMenu; }
            set
            {
                _corteDeCajaMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ObtenerEstadisticasMenu
        {
            get { return _obtenerEstadisticasMenu; }
            set
            {
                _obtenerEstadisticasMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility RegistrarGastoMenu
        {
            get { return _registrarGastoMenu; }
            set
            {
                _registrarGastoMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility AgregarInventarioProductosMenu
        {
            get { return _agregarInvetarioProductosMenu; }
            set
            {
                _agregarInvetarioProductosMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility EditarProductosMenu
        {
            get { return _editarProductosMenu; }
            set
            {
                _editarProductosMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility RealizarVentaMenu
        {
            get { return _realizarVentaMenu; }
            set
            {
                _realizarVentaMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ReportarMermaMenu
        {
            get { return _reportarMermaMenu; }
            set
            {
                _reportarMermaMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility ConsultarSucursalesMenu
        {
            get { return _consultarSucursalesMenu; }
            set
            {
                _consultarSucursalesMenu = value;
                OnPropertyChanged();
            }
        }

        public Visibility AgregarSucursalMenu
        {
            get { return _agregarSucursalMenu; }
            set
            {
                _agregarSucursalMenu = value;
                OnPropertyChanged();
            }
        }

        public MainWindowModeloVista()
        {
            _mainWindowModeloVista = this;
            VistaActualModelo = new IniciarSesionModeloVista(this);
            ConsultarSucursalesComando = new ComandoModeloVista(Sucursales);
            AgregarSucursalComando = new ComandoModeloVista(AgregarSucursal);
            RegistrarEmpleadoComando = new ComandoModeloVista(RegistrarEmpleado);
            ConsultarEmpleadosComando = new ComandoModeloVista(ConsultarEmpleados);
            ReportarMermaProductoComando = new ComandoModeloVista(ReportarMermaProducto);
            RealizarCorteCajaComando = new ComandoModeloVista(RealizarCorteCaja);
            DevolverBoletoComando = new ComandoModeloVista(DevolverBoleto);
            RegistrarSocioComando = new ComandoModeloVista(RegistrarSocio);
            ConsultarSociosComando = new ComandoModeloVista(ConsultarSocios);
            RegistrarGastoComando = new ComandoModeloVista(RegistrarGasto);
            AgregarProductoDulceriaComando = new ComandoModeloVista(AgregarProductoDulceria);
            EditarProductoDulceriaComando = new ComandoModeloVista(EditarProductoDulceria);
            RealizarVentaDulceriaComando = new ComandoModeloVista(RealizarVentaDulceria);
            AgregarPromocionComando = new ComandoModeloVista(AgregarPromocion);
            EditarPromocionComando = new ComandoModeloVista(EditarPromocion);
            CerrarSesionComando = new ComandoModeloVista(CerrarSesion);
            ObtenerEstadisticasComando = new ComandoModeloVista(ObtenerEstadisticas);
            ConsultarFuncionesComando = new ComandoModeloVista(ConsultarFunciones);
            ConsultarSalasComando = new ComandoModeloVista(ConsultarSalas);
            ConsultarPeliculasComando = new ComandoModeloVista(ConsultarPeliculas);


            PromocionesMenu = Visibility.Collapsed;
            EmpleadosMenu = Visibility.Collapsed;
            SociosMenu = Visibility.Collapsed;
            DulceriaMenu = Visibility.Collapsed;
            VentasMenu = Visibility.Collapsed;
            BoletosMenu = Visibility.Collapsed;
            FuncionesMenu = Visibility.Collapsed;
            SucursalesMenu = Visibility.Collapsed;
            SalasMenu = Visibility.Collapsed;
            SesionMenu = Visibility.Collapsed;

        }

        public MainWindowModeloVista(MainWindow _mainWindowMV)
        {
            _mainWindowModeloVista = this;
            VistaActualModelo = new IniciarSesionModeloVista(this);
            ConsultarSucursalesComando = new ComandoModeloVista(Sucursales);
            AgregarSucursalComando = new ComandoModeloVista(AgregarSucursal);
            RegistrarEmpleadoComando = new ComandoModeloVista(RegistrarEmpleado);
            ConsultarEmpleadosComando = new ComandoModeloVista(ConsultarEmpleados);
            ReportarMermaProductoComando = new ComandoModeloVista(ReportarMermaProducto);
            RealizarCorteCajaComando = new ComandoModeloVista(RealizarCorteCaja);
            DevolverBoletoComando = new ComandoModeloVista(DevolverBoleto);
            RegistrarSocioComando = new ComandoModeloVista(RegistrarSocio);
            ConsultarSociosComando = new ComandoModeloVista(ConsultarSocios);
            RegistrarGastoComando = new ComandoModeloVista(RegistrarGasto);
            AgregarProductoDulceriaComando = new ComandoModeloVista(AgregarProductoDulceria);
            EditarProductoDulceriaComando = new ComandoModeloVista(EditarProductoDulceria);
            RealizarVentaDulceriaComando = new ComandoModeloVista(RealizarVentaDulceria);
            AgregarPromocionComando = new ComandoModeloVista(AgregarPromocion);
            EditarPromocionComando = new ComandoModeloVista(EditarPromocion);
            CerrarSesionComando = new ComandoModeloVista(CerrarSesion);
        }

        public void CrearMenus()
        {
            string rol = UsuarioEnLinea.Instancia.Rol;

            if (string.IsNullOrEmpty(rol))
            {
                PromocionesMenu = Visibility.Collapsed;
                EmpleadosMenu = Visibility.Collapsed;
                SociosMenu = Visibility.Collapsed;
                DulceriaMenu = Visibility.Collapsed;
                VentasMenu = Visibility.Collapsed;
                BoletosMenu = Visibility.Collapsed;
                FuncionesMenu = Visibility.Collapsed;
                SucursalesMenu = Visibility.Collapsed;
                SalasMenu = Visibility.Collapsed;
                SesionMenu = Visibility.Collapsed;
            }
            else if (rol.Equals("admin"))
            {
                PromocionesMenu = Visibility.Visible;
                EmpleadosMenu = Visibility.Visible;
                SociosMenu = Visibility.Visible;
                DulceriaMenu = Visibility.Visible;
                VentasMenu = Visibility.Visible;
                BoletosMenu = Visibility.Visible;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Visible;
                SalasMenu = Visibility.Visible;
                SesionMenu = Visibility.Visible;
                CarteleraMenu = Visibility.Visible;
            }
            else if (rol.Equals("Gerente"))
            {
                PromocionesMenu = Visibility.Visible;
                EmpleadosMenu = Visibility.Visible;
                SociosMenu = Visibility.Visible;
                DulceriaMenu = Visibility.Visible;
                VentasMenu = Visibility.Visible;
                BoletosMenu = Visibility.Visible;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Collapsed;
                SalasMenu = Visibility.Visible;
                SesionMenu = Visibility.Visible;
                CarteleraMenu = Visibility.Visible;
            }
            else if (rol.Equals("Empleado administrativo"))
            {
                PromocionesMenu = Visibility.Collapsed;
                EmpleadosMenu = Visibility.Visible;
                RegistrarEmpleadoMenu = Visibility.Collapsed;
                SociosMenu = Visibility.Visible;
                RegistrarSocioMenu = Visibility.Collapsed;
                DulceriaMenu = Visibility.Visible;
                EditarProductosMenu = Visibility.Collapsed;
                VentasMenu = Visibility.Visible;
                ObtenerEstadisticasMenu = Visibility.Collapsed;
                BoletosMenu = Visibility.Visible;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Collapsed;
                SalasMenu = Visibility.Collapsed;
                SesionMenu = Visibility.Visible;
                CarteleraMenu = Visibility.Collapsed;
            }
            else if (rol.Equals("Empleado operativo"))
            {
                PromocionesMenu = Visibility.Collapsed;
                EmpleadosMenu = Visibility.Collapsed;
                SociosMenu = Visibility.Visible;
                RegistrarSocioMenu = Visibility.Collapsed;
                DulceriaMenu = Visibility.Visible;
                EditarProductosMenu = Visibility.Collapsed;
                AgregarInventarioProductosMenu = Visibility.Collapsed;
                ReportarMermaMenu = Visibility.Collapsed;
                VentasMenu = Visibility.Collapsed;
                BoletosMenu = Visibility.Visible;
                FuncionesMenu = Visibility.Visible;
                SucursalesMenu = Visibility.Collapsed;
                SalasMenu = Visibility.Collapsed;
                SesionMenu = Visibility.Visible;
                CarteleraMenu = Visibility.Collapsed;
            }
        }

        public void CambiarModeloVista(BaseModeloVista nuevoModeloVista)
        {
            VistaActualModelo = nuevoModeloVista;
        }
        public void ConsultarFunciones(object obj)
        {
            CambiarModeloVista(new ConsultarFuncionesModeloVista(_mainWindowModeloVista));
        }

        public void AgregarFuncion(object obj)
        {
            CambiarModeloVista(new AgregarFuncionModeloVista(_mainWindowModeloVista));
        }

        public void ConsultarSalas(object obj)
        {
            CambiarModeloVista(new ConsultarSalasModeloVista(_mainWindowModeloVista));
        }
        public void Sucursales(object obj)
        {
            CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
        }
        private void ConsultarPeliculas(object obj)
        {
            CambiarModeloVista(new ConsultarPeliculasModeloVista(_mainWindowModeloVista));
        }

        private void AgregarSucursal(object obj)
        {
            CambiarModeloVista(new AgregarSucursalModeloVista(_mainWindowModeloVista));
        }

        private void RegistrarEmpleado(object obj)
        {
            CambiarModeloVista(new RegistrarEmpleadoModeloVista(_mainWindowModeloVista));
        }

        private void ConsultarEmpleados(object obj)
        {
            CambiarModeloVista(new ConsultarEmpleadosModeloVista(_mainWindowModeloVista));
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

        private void ConsultarSocios(object obj)
        {
            CambiarModeloVista(new ConsultarSociosModeloVista(_mainWindowModeloVista));
        }

        private void RegistrarGasto(object obj)
        {
            CambiarModeloVista(new RegistrarGastoModeloVista(_mainWindowModeloVista));
        }

        private void AgregarProductoDulceria(object obj)
        {
            CambiarModeloVista(new AgregarProductoDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void EditarProductoDulceria(object obj)
        {
            CambiarModeloVista(new EditarProductoDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void ReportarMermaProducto(object obj)
        {
            CambiarModeloVista(new ReportarMermaProductoModeloVista(_mainWindowModeloVista));
        }

        private void RealizarVentaDulceria(object obj)
        {
            CambiarModeloVista(new RealizarVentaDulceriaModeloVista(_mainWindowModeloVista));
        }

        private void AgregarPromocion(object obj)
        {
            CambiarModeloVista(new AgregarPromocionModeloVista(_mainWindowModeloVista));
        }

        private void EditarPromocion(object obj)
        {
            CambiarModeloVista(new EditarPromocionModeloVista(_mainWindowModeloVista));
        }

        private void CerrarSesion(object obj)
        {
            UsuarioEnLinea.Instancia.CerrarSesionActual();
            CambiarModeloVista(new IniciarSesionModeloVista(_mainWindowModeloVista));

            CrearMenus();
        }

        private void ObtenerEstadisticas(object obj)
        {
            CambiarModeloVista(new ObtenerEstadisticasModeloVista(_mainWindowModeloVista));
        }
    }
}
