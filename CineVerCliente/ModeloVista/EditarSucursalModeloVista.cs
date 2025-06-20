﻿using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CineVerCliente.Helpers;
using System.Collections.ObjectModel;

namespace CineVerCliente.ModeloVista
{
    public class EditarSucursalModeloVista : BaseModeloVista
    {
        private int _idSucursal;
        private string _nombreSucursal;
        private string _codigoPostal;
        private string _estado;
        private string _ciudad;
        private string _calle;
        private string _numero;
        private TimeSpan _horaApertura;
        private TimeSpan _horaCierre;
        private string _estadoSucursal;

        private Visibility _nombreSucursalCampoVacio;
        private Visibility _cpCampoVacio;
        private Visibility _estadoCampoVacio;
        private Visibility _ciudadCampoVacio;
        private Visibility _calleCampoVacio;
        private Visibility _numeroCampoVacio;
        private Visibility _horaAperturaCampoVacio;
        private Visibility _horaCierreCampoVacio;

        private Visibility _mostrarMensajeConfirmacion = Visibility.Collapsed;

        public ObservableCollection<string> _estadosSucursal;

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public ICommand AceptarEdicionComando { get; }
        public ICommand CancelarEdicionComando { get; }
        public ICommand GestionarSalasComando { get; }
        public ICommand AceptarConfirmarcionComando { get; }
        public ICommand CancelarConfirmacionComando { get; }

        public int IdSucursal
        {
            get { return _idSucursal; }
            set
            {
                _idSucursal = value;
                OnPropertyChanged();
            }
        }
        public string NombreSucursal
        {
            get { return _nombreSucursal; }
            set
            {
                _nombreSucursal = value;
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

        public string Calle
        {
            get { return _calle; }
            set
            {
                _calle = value;
                OnPropertyChanged();
            }
        }

        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraApertura
        {
            get { return _horaApertura; }
            set
            {
                _horaApertura = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan HoraCierre
        {
            get { return _horaCierre; }
            set
            {
                _horaCierre = value;
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

        public ObservableCollection<string> EstadosSucursal
        {
            get { return _estadosSucursal; }
            set
            {
                _estadosSucursal = value;
                OnPropertyChanged();
            }
        }

        public Visibility NombreSucursalCampoVacio
        {
            get { return _nombreSucursalCampoVacio; }
            set
            {
                _nombreSucursalCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CPCampoVacio
        {
            get { return _cpCampoVacio; }
            set
            {
                _cpCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility EstadoCampoVacio
        {
            get { return _estadoCampoVacio; }
            set
            {
                _estadoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CiudadCampoVacio
        {
            get { return _ciudadCampoVacio; }
            set
            {
                _ciudadCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility CalleCampoVacio
        {
            get { return _calleCampoVacio; }
            set
            {
                _calleCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility NumeroCampoVacio
        {
            get { return _numeroCampoVacio; }
            set
            {
                _numeroCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility HoraAperturaCampoVacio
        {
            get { return _horaAperturaCampoVacio; }
            set
            {
                _horaAperturaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility HoraCierreCampoVacio
        {
            get { return _horaCierreCampoVacio; }
            set
            {
                _horaCierreCampoVacio = value;
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

        public EditarSucursalModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            
            AceptarEdicionComando = new ComandoModeloVista(AceptarEdicion);
            CancelarEdicionComando = new ComandoModeloVista(CancelarEdicion);
            AceptarConfirmarcionComando = new ComandoModeloVista(AceptarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);

            EstadosSucursal = new ObservableCollection<string> { "Abierta", "Cerrada" };

            OcultarCamposVacios();
        }

        private async void AceptarEdicion(object obj)
        {
            try { 
                var cliente = new SucursalServicio.SucursalServicioClient();

                if (ValidarCampos())
                {
                    var sucursalEditada = new SucursalServicio.SucursalDTO
                    {
                        Nombre = NombreSucursal,
                        CodigoPostal = CodigoPostal,
                        Estado = Estado,
                        Ciudad = Ciudad,
                        Calle = Calle,
                        NumeroEnLaCalle = Numero,
                        HoraApertura = HoraApertura,
                        HoraCierre = HoraCierre,
                        EstadoSucursal = EstadoSucursal
                    };

                    var resultado = await cliente.ActualizarSucursalAsync(IdSucursal, sucursalEditada);

                    if (resultado.EsExitoso)
                    {
                        Notificacion.Mostrar("Sucursal actualizada con éxito");
                    }
                    else
                    {
                        Notificacion.Mostrar("Error al actualizar la sucursal: " + resultado.Error);
                    }
                        _mainWindowModeloVista.CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
                }
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }
        }

        private void CancelarEdicion(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Visible;
        }

        private void AceptarConfirmacion(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
            _mainWindowModeloVista.CambiarModeloVista(new ConsultarSucursalesModeloVista(_mainWindowModeloVista));
        }

        private void CancelarConfirmacion(object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }

        public void CargarSucursal(SucursalConsultada sucursal)
        {
            IdSucursal = sucursal.IdSucursal;
            NombreSucursal = sucursal.Nombre;
            CodigoPostal = sucursal.CodigoPostal;
            Estado = sucursal.Estado;
            Ciudad = sucursal.Ciudad;
            Calle = sucursal.Calle;
            Numero = sucursal.Numero;
            HoraApertura = sucursal.HoraApertura;
            HoraCierre = sucursal.HoraCierre;
            
            if (sucursal.EstadoSucursal == "Abierta")
            {
                EstadoSucursal = EstadosSucursal.First();
            }
            else
            {
                EstadoSucursal = EstadosSucursal.Last();
            }
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarNombre();
            valido &= ValidarCP();
            valido &= ValidarEstado();
            valido &= ValidarCiudad();
            valido &= ValidarCalle();
            valido &= ValidarNumero();
            valido &= ValidarHoraApertura();
            valido &= ValidarHoraCierre();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarNombre()
        {
            if (string.IsNullOrEmpty(NombreSucursal))
            {
                NombreSucursalCampoVacio = Visibility.Visible;
                return false;
            }

            NombreSucursalCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCP()
        {
            if (string.IsNullOrEmpty(CodigoPostal))
            {
                CPCampoVacio = Visibility.Visible;
                return false;
            }

            CPCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarEstado()
        {
            if (string.IsNullOrEmpty(Estado))
            {
                EstadoCampoVacio = Visibility.Visible;
                return false;
            }

            EstadoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCiudad()
        {
            if (string.IsNullOrEmpty(Ciudad))
            {
                CiudadCampoVacio = Visibility.Visible;
                return false;
            }

            CiudadCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarCalle()
        {
            if (string.IsNullOrEmpty(Calle))
            {
                CalleCampoVacio = Visibility.Visible;
                return false;
            }

            CalleCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarNumero()
        {
            if (string.IsNullOrEmpty(Numero.ToString()))
            {
                NumeroCampoVacio = Visibility.Visible;
                return false;
            }

            NumeroCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarHoraApertura()
        {
            if (string.IsNullOrEmpty(HoraApertura.ToString()))
            {
                HoraAperturaCampoVacio = Visibility.Visible;
                return false;
            }

            HoraAperturaCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarHoraCierre()
        {
            if (string.IsNullOrEmpty(HoraCierre.ToString()))
            {
                HoraCierreCampoVacio = Visibility.Visible;
                return false;
            }

            HoraCierreCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCamposVacios()
        {
            NombreSucursalCampoVacio = Visibility.Collapsed;
            CPCampoVacio = Visibility.Collapsed;
            EstadoCampoVacio = Visibility.Collapsed;
            CiudadCampoVacio = Visibility.Collapsed;
            CalleCampoVacio = Visibility.Collapsed;
            NumeroCampoVacio = Visibility.Collapsed;
            HoraAperturaCampoVacio = Visibility.Collapsed;
            HoraCierreCampoVacio = Visibility.Collapsed;
        }
    }
}
