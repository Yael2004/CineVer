using CineVerEntidades;
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
        private PromocionDAO promocionDAO = new PromocionDAO();

        public Result<ResultDTO> ActualizarPromocion(PromocionDTO promocion)
        {
            var promocionEntity = new Promocion
            {
                nombre = promocion.Nombre,
                tipo = promocion.Tipo,
                producto = promocion.Producto,
                numeroProductosNecesarios = promocion.NumeroProductosNecesarios,
                numeroProductosPagar = promocion.NumeroProductosPagar,
                lunesAplica = promocion.LunesAplica,
                martesAplica = promocion.MartesAplica,
                miercolesAplica = promocion.MiercolesAplica,
                juevesAplica = promocion.JuevesAplica,
                viernesAplica = promocion.ViernesAplica,
                sabadoAplica = promocion.SabadoAplica,
                domingoAplica = promocion.DomingoAplica,
            };
            var resultado = promocionDAO.ActualizarPromocion(promocionEntity);
            if (!resultado.EsExitoso)
            {
                return Result<ResultDTO>.Fallo(resultado.Error);
            }
            else
            {
                return Result<ResultDTO>.Exito(new ResultDTO(true, "Promoción actualizada correctamente"));
            }
        }

        public Result<ListaPromocionesDTO> ObtenerPromociones(int idSucursal)
        {
            var resultado = promocionDAO.ObtenerPromociones(idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<ListaPromocionesDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaPromociones = new ListaPromocionesDTO();
                foreach (var promocion in resultado.Valor)
                {
                    var promocionDTO = new PromocionDTO
                    {
                        IdPromocion = promocion.idPromocion,
                        Nombre = promocion.nombre,
                        Tipo = promocion.tipo,
                        Producto = promocion.producto,
                        NumeroProductosNecesarios = (int)promocion.numeroProductosNecesarios,
                        NumeroProductosPagar = (int)promocion.numeroProductosPagar,
                        LunesAplica = (bool)promocion.lunesAplica,
                        MartesAplica = (bool)promocion.martesAplica,
                        MiercolesAplica = (bool)promocion.miercolesAplica,
                        JuevesAplica = (bool)promocion.juevesAplica,
                        ViernesAplica = (bool)promocion.viernesAplica,
                        SabadoAplica = (bool)promocion.sabadoAplica,
                        DomingoAplica = (bool)promocion.domingoAplica
                    };
                    listaPromociones.Promociones.Add(promocionDTO);
                }
                return Result<ListaPromocionesDTO>.Exito(listaPromociones);
            }
        }

        public Result<ListaPromocionesDTO> ObtenerPromocionesBoletos(int idSucursal)
        {
            var resultado = promocionDAO.ObtenerPromocionesBoletos(idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<ListaPromocionesDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaPromociones = new ListaPromocionesDTO();
                foreach (var promocion in resultado.Valor)
                {
                    var promocionDTO = new PromocionDTO
                    {
                        IdPromocion = promocion.idPromocion,
                        Nombre = promocion.nombre,
                        Tipo = promocion.tipo,
                        Producto = promocion.producto,
                        NumeroProductosNecesarios = (int)promocion.numeroProductosNecesarios,
                        NumeroProductosPagar = (int)promocion.numeroProductosPagar,
                        LunesAplica = (bool)promocion.lunesAplica,
                        MartesAplica = (bool)promocion.martesAplica,
                        MiercolesAplica = (bool)promocion.miercolesAplica,
                        JuevesAplica = (bool)promocion.juevesAplica,
                        ViernesAplica = (bool)promocion.viernesAplica,
                        SabadoAplica = (bool)promocion.sabadoAplica,
                        DomingoAplica = (bool)promocion.domingoAplica
                    };
                    listaPromociones.Promociones.Add(promocionDTO);
                }
                return Result<ListaPromocionesDTO>.Exito(listaPromociones);
            }
        }

        public Result<ListaPromocionesDTO> ObtenerPromocionesDulceria(int idSucursal)
        {
            var resultado = promocionDAO.ObtenerPromocionesDulceria(idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<ListaPromocionesDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaPromociones = new ListaPromocionesDTO();
                foreach (var promocion in resultado.Valor)
                {
                    var promocionDTO = new PromocionDTO
                    {
                        IdPromocion = promocion.idPromocion,
                        Nombre = promocion.nombre,
                        Tipo = promocion.tipo,
                        Producto = promocion.producto,
                        NumeroProductosNecesarios = (int)promocion.numeroProductosNecesarios,
                        NumeroProductosPagar = (int)promocion.numeroProductosPagar,
                        LunesAplica = (bool)promocion.lunesAplica,
                        MartesAplica = (bool)promocion.martesAplica,
                        MiercolesAplica = (bool)promocion.miercolesAplica,
                        JuevesAplica = (bool)promocion.juevesAplica,
                        ViernesAplica = (bool)promocion.viernesAplica,
                        SabadoAplica = (bool)promocion.sabadoAplica,
                        DomingoAplica = (bool)promocion.domingoAplica
                    };
                    listaPromociones.Promociones.Add(promocionDTO);
                }
                return Result<ListaPromocionesDTO>.Exito(listaPromociones);
            }
        }

        public Result<ResultDTO> RegistrarPromocion(PromocionDTO promocion)
        {
            var promocionEntity = new Promocion
            {
                idSucursal = promocion.IdSucursal,
                nombre = promocion.Nombre,
                tipo = promocion.Tipo,
                producto = promocion.Producto,
                numeroProductosNecesarios = promocion.NumeroProductosNecesarios,
                numeroProductosPagar = promocion.NumeroProductosPagar,
                lunesAplica = promocion.LunesAplica,
                martesAplica = promocion.MartesAplica,
                miercolesAplica = promocion.MiercolesAplica,
                juevesAplica = promocion.JuevesAplica,
                viernesAplica = promocion.ViernesAplica,
                sabadoAplica = promocion.SabadoAplica,
                domingoAplica = promocion.DomingoAplica
            };
            var resultado = promocionDAO.RegistrarPromocion(promocionEntity);
            if (!resultado.EsExitoso)
            {
                return Result<ResultDTO>.Fallo(resultado.Error);
            }
            else
            {
                return Result<ResultDTO>.Exito(new ResultDTO(true, "Promoción registrada correctamente"));
            }
        }

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

        public Result<decimal> ObtenerVentasDeDulceriaDelDia(int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasDeDulceriaDelDia(idSucursal);
            if (!resultado.EsExitoso)
            {
                return Result<decimal>.Fallo(resultado.Error);
            }
            else
            {
                return Result<decimal>.Exito(resultado.Valor);
            }
        }

        public Result<decimal> ObtenerVentasEnEfectivoDelDia(int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasEnEfectivoDelDia(idSucursal);

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
