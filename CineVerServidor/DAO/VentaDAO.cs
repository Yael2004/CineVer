using CineVerEntidades;
using System;
using System.Collections.Generic;
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

    }
}
