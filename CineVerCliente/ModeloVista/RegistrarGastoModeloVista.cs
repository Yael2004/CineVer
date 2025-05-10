using CineVerCliente.GastoServicio;
using CineVerCliente.Helpers;
using CineVerCliente.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    class RegistrarGastoModeloVista : BaseModeloVista
    {
        private string _motivo;
        private decimal _monto;

        private Visibility _nombreGastoCampoVacio;
        private Visibility _totalGastoCampoVacio;

        private Visibility _mostrarMensajeConfirmacion = Visibility.Collapsed;

        public ICommand RegistrarComando { get; }
        public ICommand CancelarComando { get; }
        public ICommand AceptarConfirmacionComando { get; }
        public ICommand CancelarConfirmacionComando { get; }

        private readonly MainWindowModeloVista _mainWindowModeloVista;

        public string Motivo
        {
            get { return _motivo; }
            set
            {
                _motivo = value;
                OnPropertyChanged();
            }
        }

        public decimal Monto
        {
            get { return _monto; }
            set
            {
                _monto = value;
                OnPropertyChanged();
            }
        }

        public Visibility NombreGastoCampoVacio
        {
            get { return _nombreGastoCampoVacio; }
            set
            {
                _nombreGastoCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility TotalGastoCampoVacio
        {
            get { return _totalGastoCampoVacio; }
            set
            {
                _totalGastoCampoVacio = value;
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

        public RegistrarGastoModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;
            RegistrarComando = new ComandoModeloVista(RegistrarGasto);
            CancelarComando = new ComandoModeloVista(Cancelar);
            AceptarConfirmacionComando = new ComandoModeloVista(AceptarConfirmacion);
            CancelarConfirmacionComando = new ComandoModeloVista(CancelarConfirmacion);
            
            OcultarCampos();
        }

        public void RegistrarGasto(Object obj)
        {
            if (ValidarCampos())
            {
                MostrarMensajeConfirmacion = Visibility.Visible;
            }
        }

        public void Cancelar(Object obj)
        {
            _mainWindowModeloVista.CambiarModeloVista(new MainWindowModeloVista());
        }

        public void AceptarConfirmacion(Object obj)
        {
            var cliente = new GastoServicio.GastoServicioClient();

            try
            {
                var gasto = new GastoDTO
                {
                    Motivo = Motivo,
                    Monto = Monto,
                    Fecha = DateTime.Now,
                    IdEmpleado = UsuarioEnLinea.Instancia.IdEmpleado,
                    IdSucursal = UsuarioEnLinea.Instancia.IdSucursal
                };

                var resultado = cliente.RegistrarGasto(gasto);

                if (resultado.EsExitoso)
                {
                    Notificacion.Mostrar("Gasto registrado correctamente", 4000);
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                }
                else
                {
                    Notificacion.Mostrar("Error al registrar el gasto", 4000);
                    MostrarMensajeConfirmacion = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
                Notificacion.Mostrar("Error al registrar el gasto", 4000);
                MostrarMensajeConfirmacion = Visibility.Collapsed;
            }
        }

        public void CancelarConfirmacion(Object obj)
        {
            MostrarMensajeConfirmacion = Visibility.Collapsed;
        }

        private bool ValidarCampos()
        {
            bool valido = true;

            valido &= ValidarMotivo();
            valido &= ValidarMonto();

            if (valido)
            {
                return true;
            }

            return false;
        }

        private bool ValidarMotivo()
        {
            if (string.IsNullOrEmpty(Motivo))
            {
                NombreGastoCampoVacio = Visibility.Visible;
                return false;
            }

            NombreGastoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private bool ValidarMonto()
        {
            if (Monto == 0)
            {
                TotalGastoCampoVacio = Visibility.Visible;
                return false;
            }
            TotalGastoCampoVacio = Visibility.Collapsed;
            return true;
        }

        private void OcultarCampos()
        {
            NombreGastoCampoVacio = Visibility.Collapsed;
            TotalGastoCampoVacio = Visibility.Collapsed;
        }
    }
}
