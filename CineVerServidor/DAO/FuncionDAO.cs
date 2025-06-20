using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CineVerEntidades;
using Utilidades;

namespace DAO
{
    public class FuncionDAO
    {
        public Result<List<Función>> ObtenerFuncionesPorPeliculaYFecha(int idPelicula,DateTime fecha)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var funciones = entities.Función.Include("Película").Include("Sala").Where(e=>e.fecha == fecha && e.idPelicula == idPelicula).ToList();
                    return Result<List<Función>>.Exito(funciones);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Función>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Función>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<List<Función>> ObtenerFUncionesPorFecha(DateTime fecha)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var funciones = entities.Función.Include("Película").Include("Sala").Where(e => e.fecha == fecha).ToList();
                    return Result<List<Función>>.Exito(funciones);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Función>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Función>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<List<Función>> ObtenerFuncionesPorSalaYFecha(int idSala,DateTime fecha)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    DateTime fechaLimpia = fecha.Date;
                    DateTime? fechaNullable = fechaLimpia;

                    var funciones = entities.Función.Include("Película").Include("Sala")
                        .Where(e => DbFunctions.TruncateTime(e.fecha) == fechaNullable && e.idSala == idSala)
                        .ToList();



                    return Result<List<Función>>.Exito(funciones);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Función>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Función>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<string> AgregarFuncion(Función funcion)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Función.Add(funcion);
                    entities.SaveChanges();

                    var filas = entities.Fila.Include("Asiento")
                                             .Where(f => f.idSala == funcion.idSala)
                                             .ToList();

                    foreach (var fila in filas)
                    {
                        foreach (var asiento in fila.Asiento)
                        {
                            var asientoFuncion = new AsientoFuncion
                            {
                                idFuncion = funcion.idFuncion,
                                idAsiento = asiento.idAsiento,
                                estado = "DISPONIBLE"
                            };

                            entities.AsientoFuncion.Add(asientoFuncion);
                        }
                    }

                    entities.SaveChanges();
                    return Result<string>.Exito("Función agregada con éxito");
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
                    return Result<string>.Fallo(ex.InnerException?.InnerException?.Message ?? ex.Message);
                }
            }
        }


        public Result<string> EditarFuncion(Función funcionOriginal,Función funcionEditada)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var funcionInsertar = entities.Función.Find(funcionOriginal.idFuncion);
                    funcionInsertar.horaInicio = funcionEditada.horaInicio;
                    funcionInsertar.idSala = funcionEditada.idSala;
                    funcionInsertar.idPelicula = funcionEditada.idPelicula;
                    funcionInsertar.precioBoleto = funcionEditada.precioBoleto;
                    funcionInsertar.fecha = funcionEditada.fecha;
                    entities.SaveChanges();
                    return Result<string>.Exito("Función editada con éxito");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<string> EliminarFuncion(Función funcion)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var boletosAsociados = entities.Venta.Any(b => b.idFuncion == funcion.idFuncion);
                    if (boletosAsociados)
                    {
                        return Result<string>.Fallo("No se puede eliminar la función porque tiene boletos asociados");
                    }

                    var asientosRelacionados = entities.AsientoFuncion
                        .Where(af => af.idFuncion == funcion.idFuncion)
                        .ToList();

                    entities.AsientoFuncion.RemoveRange(asientosRelacionados);

                    var funcionEliminar = entities.Función.Find(funcion.idFuncion);
                    if (funcionEliminar != null)
                    {
                        entities.Función.Remove(funcionEliminar);
                        entities.SaveChanges();
                        return Result<string>.Exito("Función eliminada exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("No se encontró la función para eliminarla");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo($"Validación fallida: {ex.Message}");
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo($"Error SQL: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo($"Error inesperado: {ex.Message}");
                }
            }
        }

    }
}
