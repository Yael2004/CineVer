using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class PromocionDAO
    {
        public Result<List<Promocion>> ObtenerPromociones(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var promociones = (from p in entities.Promocion
                                       where p.idSucursal == idSucursal
                                       select p).ToList();
                    if (promociones.Count > 0)
                    {
                        return Result<List<Promocion>>.Exito(promociones);
                    }
                    else
                    {
                        return Result<List<Promocion>>.Fallo("No hay promociones disponibles para la sucursal solicitada.");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Promocion>>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
            }
        }

        public Result<List<Promocion>> ObtenerPromocionesDulceria(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var promociones = (from p in entities.Promocion
                                       where p.idSucursal == idSucursal && p.tipo == "Dulceria"
                                       select p).ToList();
                    if (promociones.Count > 0)
                    {
                        return Result<List<Promocion>>.Exito(promociones);
                    }
                    else
                    {
                        return Result<List<Promocion>>.Fallo("No hay promociones de dulcería disponibles para la sucursal solicitada.");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Promocion>>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
            }
        }

        public Result<List<Promocion>> ObtenerPromocionesBoletos(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var promociones = (from p in entities.Promocion
                                       where p.idSucursal == idSucursal && p.tipo == "Taquilla"
                                       select p).ToList();
                    if (promociones.Count > 0)
                    {
                        return Result<List<Promocion>>.Exito(promociones);
                    }
                    else
                    {
                        return Result<List<Promocion>>.Fallo("No hay promociones de boletos disponibles para la sucursal solicitada.");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Promocion>>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<List<Promocion>>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> ActualizarPromocion(Promocion promocion)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var promocionExistente = entities.Promocion.Find(promocion.idPromocion);
                    if (promocionExistente != null)
                    {
                        promocionExistente.nombre = promocion.nombre;
                        promocionExistente.tipo = promocion.tipo;
                        promocionExistente.producto = promocion.producto;
                        promocionExistente.numeroProductosNecesarios = promocion.numeroProductosNecesarios;
                        promocionExistente.numeroProductosPagar = promocion.numeroProductosPagar;
                        promocionExistente.nombre = promocion.nombre;
                        promocionExistente.lunesAplica = promocion.lunesAplica;
                        promocionExistente.martesAplica = promocion.martesAplica;
                        promocionExistente.miercolesAplica = promocion.miercolesAplica;
                        promocionExistente.juevesAplica = promocion.juevesAplica;
                        promocionExistente.viernesAplica = promocion.viernesAplica;
                        promocionExistente.sabadoAplica = promocion.sabadoAplica;
                        promocionExistente.domingoAplica = promocion.domingoAplica;

                        entities.SaveChanges();
                        return Result<string>.Exito("Promoción actualizada correctamente.");
                    }
                    else
                    {
                        return Result<string>.Fallo("Promoción no encontrada.");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }
        public Result<string> RegistrarPromocion(Promocion promocion)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var resultado = entities.Promocion.Add(promocion);
                    entities.SaveChanges();

                    if (resultado != null)
                    {
                        return Result<string>.Exito("Promoción registrada correctamente.");
                    }
                    else
                    {
                        return Result<string>.Fallo("Error al registrar la promoción.");
                    }
                }
                catch (DbEntityValidationException ex) 
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }
    }
}
