using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorVenta
    {
        private VentaDAO ventaDAO = new VentaDAO();
        public Result<ListaVentasDTO> ObtenerVentasPorAnio(int anio, int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasPorAnio(anio, idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<ListaVentasDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaVentas = new ListaVentasDTO();
                foreach (var venta in resultado.Valor)
                {
                    var ventaDTO = new VentaDTO
                    {
                        IdVenta = venta.idVenta,
                        Fecha = (DateTime)venta.fecha,
                        Total = (decimal)venta.total,
                        MetodoPago = venta.metodoPago,
                        TIpoVenta = venta.tipoVenta,
                        FolioVenta = venta.folioVenta,
                        IdSucursal = (int)venta.idSucursal
                    };
                    listaVentas.Ventas.Add(ventaDTO);
                }
                return Result<ListaVentasDTO>.Exito(listaVentas);
            }
        }

        public Result<ListaVentasDTO> ObtenerVentasPorMes(int mes, int anio, int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasPorMes(mes, anio, idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<ListaVentasDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaVentas = new ListaVentasDTO();
                foreach (var venta in resultado.Valor)
                {
                    var ventaDTO = new VentaDTO
                    {
                        IdVenta = venta.idVenta,
                        Fecha = (DateTime)venta.fecha,
                        Total = (decimal)venta.total,
                        MetodoPago = venta.metodoPago,
                        TIpoVenta = venta.tipoVenta,
                        IdSucursal = (int)venta.idSucursal
                    };
                    listaVentas.Ventas.Add(ventaDTO);
                }
                return Result<ListaVentasDTO>.Exito(listaVentas);
            }
        }

        public Result<string> ObtenerVentaPorFolio(string folio)
        {
            var resultado = ventaDAO.ObtenerVentaPorFolio(folio);
            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Se econtró la venta especificada");
            }
        }

        public Result<string> VerificarFechaVentaParaDevolucion(string folio)
        {
            var resultado = ventaDAO.VerificarFechaVentaParaDevolucion(folio);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Se econtró la venta especificada");
            }
        }

        public Result<decimal> ObtenerVentasDeBoletosDelDia(int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasDeBoletosDelDia(idSucursal);
            
            if (!resultado.EsExitoso)
            {
                return Result<decimal>.Fallo(resultado.Error);
            }
            else
            {
                return Result<decimal>.Exito(resultado.Valor);
            }
        }
    }
}
