using CineVerCliente.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineVerCliente.ModeloVista
{
    public class DevolverBoletoModeloVista : BaseModeloVista
    {
        private string _numeroSocio;
        private string _folioVenta;
        private MainWindowModeloVista _mainWindowModeloVista;

        private Visibility _numeroSocioCampoVacio;
        private Visibility _folioVentaCampoVacio;
        private Visibility _folioVentaNoExiste;
        private Visibility _numeroSocioNoExiste;

        private Visibility _mostrarVentanaConfirmacion;
        private Visibility _mostrarVentanaDevolucionNoPosible;

        public ICommand DevolverBoletoComando { get; }
        public ICommand AceptarDevolucionComando { get; }
        public ICommand CancelarDevolucionComando { get; }
        public ICommand AceptarNoPosibleComando { get; }

        public string NumeroSocio
        {
            get { return _numeroSocio; }
            set
            {
                _numeroSocio = value;
                OnPropertyChanged();
            }
        }

        public string FolioVenta
        {
            get { return _folioVenta; }
            set
            {
                _folioVenta = value;
                OnPropertyChanged();
            }
        }

        public Visibility NumeroSocioCampoVacio
        {
            get { return _numeroSocioCampoVacio; }
            set
            {
                _numeroSocioCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility FolioVentaCampoVacio
        {
            get { return _folioVentaCampoVacio; }
            set
            {
                _folioVentaCampoVacio = value;
                OnPropertyChanged();
            }
        }

        public Visibility FolioVentaNoExiste
        {
            get { return _folioVentaNoExiste; }
            set
            {
                _folioVentaNoExiste = value;
                OnPropertyChanged();
            }
        }

        public Visibility NumeroSocioNoExiste
        {
            get { return _numeroSocioNoExiste; }
            set
            {
                _numeroSocioNoExiste = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarVentanaConfirmacion
        {
            get { return _mostrarVentanaConfirmacion; }
            set
            {
                _mostrarVentanaConfirmacion = value;
                OnPropertyChanged();
            }
        }

        public Visibility MostrarVentanaDevolucionNoPosible
        {
            get { return _mostrarVentanaDevolucionNoPosible; }
            set
            {
                _mostrarVentanaDevolucionNoPosible = value;
                OnPropertyChanged();
            }
        }

        public DevolverBoletoModeloVista(MainWindowModeloVista mainWindowModeloVista)
        {
            _mainWindowModeloVista = mainWindowModeloVista;

            DevolverBoletoComando = new ComandoModeloVista(DevolverBoleto);
            AceptarDevolucionComando = new ComandoModeloVista(AceptarDevolucion);
            CancelarDevolucionComando = new ComandoModeloVista(CancelarDevolucion);
            AceptarNoPosibleComando = new ComandoModeloVista(AceptarNoPosible);

            MostrarVentanaConfirmacion = Visibility.Collapsed;
            MostrarVentanaDevolucionNoPosible = Visibility.Collapsed;
            FolioVentaCampoVacio = Visibility.Collapsed;
            NumeroSocioCampoVacio = Visibility.Collapsed;
            FolioVentaNoExiste = Visibility.Collapsed;
            NumeroSocioNoExiste = Visibility.Collapsed;
        }

        public void DevolverBoleto(object obj)
        {
            if (ValidarCampos())
            {
                MostrarVentanaConfirmacion = Visibility.Visible;
            }

            FolioVentaNoExiste = Visibility.Collapsed;
            FolioVentaCampoVacio = Visibility.Collapsed;
        }

        public void AceptarDevolucion(object obj)
        {
            try { 
                var clienteVenta = new VentaServicio.VentaServicioClient();
                var clienteSocio = new SocioServicio.SocioServicioClient();

                var resultado = clienteVenta.ObtenerVentaPorFolio(FolioVenta);
                var resultadoSocio = clienteSocio.ExisteSocio(NumeroSocio);

                if (!resultadoSocio.EsExitoso)
                {
                    NumeroSocioNoExiste = Visibility.Visible;
                }
                else if (!resultado.EsExitoso)
                {
                    NumeroSocioNoExiste = Visibility.Collapsed;
                    FolioVentaNoExiste = Visibility.Visible;
                }
                else
                {
                    MostrarVentanaConfirmacion = Visibility.Collapsed;
                    FolioVentaNoExiste = Visibility.Collapsed;
                    FolioVentaCampoVacio = Visibility.Collapsed;

                    clienteVenta.VerificarFechaVentaParaDevolucion(FolioVenta);
                
                    if (!resultadoSocio.EsExitoso)
                    {
                        MostrarVentanaDevolucionNoPosible = Visibility.Visible;
                        return;
                    }

                    //Agregar puntos al socio

                    Notificacion.Mostrar("El boleto ha sido devuelto exitosamente.");
                    MostrarVentanaConfirmacion = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                Notificacion.MostrarExcepcion();
            }
        }

        public void CancelarDevolucion(object obj)
        {
            MostrarVentanaConfirmacion = Visibility.Collapsed;
        }

        public void AceptarNoPosible(object obj)
        {
            MostrarVentanaDevolucionNoPosible = Visibility.Collapsed;
        }

        public bool ValidarCampos()
        {
            bool camposValidos = true;

            camposValidos &= ValidarFolioVenta();
            camposValidos &= ValidarNumeroSocio();

            if (camposValidos)
            {
                return true;
            }

            return false;
        }

        public bool ValidarFolioVenta()
        {
            if (string.IsNullOrWhiteSpace(FolioVenta))
            {
                FolioVentaCampoVacio = Visibility.Visible;
                return false;
            }

            FolioVentaCampoVacio = Visibility.Collapsed;
            return true;
        }

        public bool ValidarNumeroSocio()
        {
            if (string.IsNullOrWhiteSpace(NumeroSocio))
            {
                NumeroSocioCampoVacio = Visibility.Visible;
                return false;
            }

            NumeroSocioCampoVacio = Visibility.Collapsed;
            return true;
        }
    }
}
