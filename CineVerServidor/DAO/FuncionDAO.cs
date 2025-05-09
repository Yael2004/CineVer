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
                    var funciones = entities.Función.Where(e=>e.fecha == fecha && e.idPelicula == idPelicula).ToList();
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
                    var funciones = entities.Función.Where(e => e.fecha == fecha).ToList();
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
                    var funcionEliminar = entities.Función.Find(funcion.idFuncion);
                    entities.Función.Remove(funcionEliminar);
                    entities.SaveChanges();
                    return Result<string>.Exito("Función eliminada con éxito");
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
    }
}
