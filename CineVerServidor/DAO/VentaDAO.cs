using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class VentaDAO
    {
        public VentaDAO() { }

        //public Result<string> GetAsientosPorFuncion(int idFuncion)
        //{
        //    using (CineVerEntities entities = new CineVerEntities())
        //    {
        //        try
        //        {
        //            var asientos = entities.Asiento.Where(a => a.idFuncion == idFuncion).ToList();
        //            if (asientos.Count == 0)
        //            {
        //                return Result<string>.Fallo("No hay asientos registrados para esta función");
        //            }
        //            return Result<string>.Exito(string.Join(", ", asientos.Select(a => a.numeroAsiento)));
        //        }
        //        catch (Exception ex)
        //        {
        //            return Result<string>.Fallo(ex.Message);
        //        }
        //    }
        //}

        public Result<string> CambiarEstadoAsiento(int idAsiento, string nuevoEstado)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asiento = entities.Asiento.Find(idAsiento);
                    if (asiento == null)
                    {
                        return Result<string>.Fallo("Asiento no encontrado");
                    }
                    asiento.estado = nuevoEstado;
                    entities.SaveChanges();
                    return Result<string>.Exito("Estado del asiento actualizado correctamente");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }

        public Result<List<Venta>> ObtenerVentasPorAnio(int anio, int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var ventas = entities.Venta
                        .Where(v => v.fecha.Value.Year == anio && v.idSucursal == idSucursal)
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<List<Venta>>.Fallo("No se encontraron ventas para el año especificado");
                    }

                    return Result<List<Venta>>.Exito(ventas);
                }
                catch (Exception ex)
                {
                    return Result<List<Venta>>.Fallo("¡Error al consultar las ventas! " + ex.Message);
                }
            }
        }

        public Result<List<Venta>> ObtenerVentasPorMes(int mes, int anio, int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var ventas = entities.Venta
                        .Where(v => v.fecha.Value.Month == mes && v.fecha.Value.Year == anio && v.idSucursal == idSucursal)
                        .ToList();
                    if (ventas.Count == 0)
                    {
                        return Result<List<Venta>>.Fallo("No se encontraron ventas para el mes especificado");
                    }
                    return Result<List<Venta>>.Exito(ventas);
                }
                catch (Exception ex)
                {
                    return Result<List<Venta>>.Fallo("¡Error al consultar las ventas! " + ex.Message);
                }
            }
        }

        public Result<string> ObtenerVentaPorFolio(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var venta = entities.Venta
                        .FirstOrDefault(v => v.folioVenta == folio);
                    if (venta == null)
                    {
                        return Result<string>.Fallo("No se encontró la venta con el folio especificado");
                    }
                    return Result<string>.Exito("Se econtró la venta especificada");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo("¡Error al consultar la venta! " + ex.Message);
                }
            }
        }

        public Result<string> VerificarFechaVentaParaDevolucion(string folio)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var venta = entities.Venta
                        .FirstOrDefault(v => v.folioVenta == folio);

                    var tiempoRestante = venta.fecha.Value - DateTime.Now.Date;

                    if (tiempoRestante.TotalMinutes < 60)
                    {
                        return Result<string>.Fallo("No es posible devolver el boleto");
                    }

                    return Result<string>.Exito("La devolucion es valida");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo("¡Error al verificar la fecha de la venta! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasDeBoletosDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.tipoVenta.Contains("Bole"))
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas de boletos para el día especificado");
                    }

                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);

                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas de boletos! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasDeDulceriaDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.tipoVenta.Contains("Dulce"))
                        .ToList();

                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas de dulcería para el día especificado");
                    }
                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);

                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas de dulcería! " + ex.Message);
                }
            }
        }

        public Result<decimal> ObtenerVentasEnEfectivoDelDia(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fechaHoy = DateTime.Now.Date;
                    var ventas = entities.Venta
                        .Where(v => DbFunctions.TruncateTime(v.fecha) == fechaHoy && v.idSucursal == idSucursal && v.metodoPago.Contains("Efectivo"))
                        .ToList();
                    if (ventas.Count == 0)
                    {
                        return Result<decimal>.Fallo("No se encontraron ventas en efectivo para el día especificado");
                    }
                    decimal totalVentas = (decimal)ventas.Sum(v => v.total);
                    return Result<decimal>.Exito(totalVentas);
                }
                catch (Exception ex)
                {
                    return Result<decimal>.Fallo("¡Error al consultar las ventas en efectivo! " + ex.Message);
                }
            }
        }

        public Result<string> RealizarPagoDulceria(Venta venta, Dictionary<int, int> productos)
        {
            using (CineVerEntities entities = new CineVerEntities())
            using (var transaction = entities.Database.BeginTransaction())
            {
                try
                {
                    venta.fecha = DateTime.Now;
                    entities.Venta.Add(venta);
                    entities.SaveChanges();

                    if (venta.idSocio != 0)
                    {
                        var cuentaFidelidad = entities.CuentaFidelidad
                            .FirstOrDefault(c => c.idSocio == venta.idSocio);

                        if (cuentaFidelidad != null)
                        {
                            decimal puntosGanados = venta.total.GetValueOrDefault() * 0.10m;
                            cuentaFidelidad.puntos = cuentaFidelidad.puntos.GetValueOrDefault() + puntosGanados;
                        }

                    }

                    foreach (var item in productos)
                    {
                        int idProducto = item.Key;
                        int cantidadVendida = item.Value;

                        var producto = entities.ProductoDulceria.Find(idProducto);
                        if (producto == null)
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"Producto con ID {idProducto} no encontrado");
                        }

                        if (producto.cantidadInventario < cantidadVendida)
                        {
                            transaction.Rollback();
                            return Result<string>.Fallo($"Inventario insuficiente para el producto con ID {idProducto}");
                        }

                        producto.cantidadInventario -= cantidadVendida;
                    }

                    entities.SaveChanges();
                    transaction.Commit();

                    return Result<string>.Exito("Venta registrada correctamente");
                }
                catch (DbEntityValidationException ex)
                {
                    transaction.Rollback();
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }

    }
}
