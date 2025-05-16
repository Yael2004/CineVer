using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VentaServicio : IVentaServicio
    {
        private GestorVenta _gestorVenta = new GestorVenta();
        public Task<ResultDTO> ActualizarPromocion(PromocionDTO promocion)
        {
            var resultado = _gestorVenta.ActualizarPromocion(promocion);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ListaPromocionesDTO> ObtenerPromociones(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerPromociones(idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Valor);
            }
        }

        public Task<ListaPromocionesDTO> ObtenerPromocionesBoletos(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerPromocionesBoletos(idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Valor);
            }
        }

        public Task<ListaPromocionesDTO> ObtenerPromocionesDulceria(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerPromocionesDulceria(idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Valor);
            }
        }

        public Task<ResultDTO> ObtenerVentaPorFolio(string folio)
        {
            var resultado = _gestorVenta.ObtenerVentaPorFolio(folio);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<VentaTipoResponseDTO> ObtenerVentasDeBoletosDelDia(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerVentasDeBoletosDelDia(idSucursal);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    Total = resultado.Valor,
                    ResultDTO = new ResultDTO(true, string.Empty)
                });
            }
            else
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<VentaTipoResponseDTO> ObtenerVentasDeDulceriaDelDia(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerVentasDeDulceriaDelDia(idSucursal);
            
            if (resultado.EsExitoso)
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    Total = resultado.Valor,
                    ResultDTO = new ResultDTO(true, string.Empty)
                });
            }
            else
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<VentaTipoResponseDTO> ObtenerVentasEnEfectivoDelDia(int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerVentasEnEfectivoDelDia(idSucursal);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    Total = resultado.Valor,
                    ResultDTO = new ResultDTO(true, string.Empty)
                });
            }
            else
            {
                return Task.FromResult(new VentaTipoResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<ListaVentasDTO> ObtenerVentasPorAnio(int anio, int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerVentasPorAnio(anio, idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Valor);
            }
        }

        public Task<ListaVentasDTO> ObtenerVentasPorMes(int mes, int anio, int idSucursal)
        {
            var resultado = _gestorVenta.ObtenerVentasPorMes(mes, anio, idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                throw new FaultException(resultado.Error);
            }
        }

        public Task<ResultDTO> RealizarPagoBoletos(VentaDTO venta)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> RealizarPagoDulceria(VentaDTO venta, Dictionary<int, int> productos)
        {
            var resultado = _gestorVenta.RealizarPagoDulceria(venta, productos);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> RegistrarPromocion(PromocionDTO promocion)
        {
            var resultado = _gestorVenta.RegistrarPromocion(promocion);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> VerificarFechaVentaParaDevolucion(string folio)
        {
            var resultado = _gestorVenta.VerificarFechaVentaParaDevolucion(folio);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }
    }
}
