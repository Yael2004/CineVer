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
    public class AsientoDAO
    {
        public AsientoDAO() { }
        public Result<List<Asiento>> ObtenerAsientosDeFila(int idFila)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asientos = entities.Asiento.Where(e=>e.idFila == idFila).ToList();
                    return Result<List<Asiento>>.Exito(asientos);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Asiento>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List <Asiento>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<int> ObtenerIdAsiento(int idFila, string letraColumna)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asiento = entities.Asiento.Where(e => e.idFila == idFila && e.letraColumna.Equals(letraColumna)).FirstOrDefault();
                    return Result<int>.Exito(asiento.idAsiento);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<int>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<int>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<string> AgregarAsiento(Asiento asiento)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Asiento.Add(asiento);
                    entities.SaveChanges();
                    return Result<string>.Exito("Asiento agregado exitosamente");
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
        public Result<string> EditarAsiento(Asiento asientoEditado, Asiento asientoOriginal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asientoInsertar = entities.Asiento.Find(asientoOriginal.idAsiento);
                    asientoInsertar.letraColumna = asientoEditado.letraColumna;
                    asientoInsertar.estado = asientoEditado.estado;
                    entities.SaveChanges();
                    return Result<string>.Exito("Asiento Editado exitosamente");
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
        public Result<string> EliminarAsiento(Asiento asiento)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var asientoEliminar = entities.Asiento.Find(asiento.idAsiento);
                    entities.Asiento.Remove(asientoEliminar);
                    entities.SaveChanges ();
                    return Result<string>.Exito("Asiento eliminado exitosamente");
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
