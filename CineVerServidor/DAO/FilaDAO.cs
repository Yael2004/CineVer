using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class FilaDAO
    {
        public FilaDAO() { }
        public Result<string> AgregarFila(Fila fila)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Fila.Add(fila);
                    entities.SaveChanges();
                    return Result<string>.Exito("Fila agregada exitosamente");
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
        public Result <int> ObtenerIdFila(int idSala, int numeroFila)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var fila = entities.Fila.Where(e=>e.idSala == idSala && e.númeroFila == numeroFila).FirstOrDefault();
                    if(fila != null)
                    {
                        return Result<int>.Exito(fila.idFila);
                    }
                    else
                    {
                        return Result<int>.Fallo("Fila no encontrada");
                    }
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
        public Result <string> EditarFila(Fila filaEditada, Fila filaOriginal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var filaInsertar = entities.Fila.Find(filaOriginal.idFila);
                    filaInsertar.númeroFila = filaEditada.númeroFila;
                    filaInsertar.numeroAsientos = filaEditada.numeroAsientos;
                    entities.SaveChanges();
                    return Result<string>.Exito("Fila editada exitosamente");
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
        public Result<string> EliminarFila(Fila filaFila)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var filaInsertar = entities.Fila.Find(filaFila.idFila);
                    entities.Fila.Remove(filaInsertar);
                    entities.SaveChanges();
                    return Result<string>.Exito("Fila eliminada exitosamente");
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
        public Result<List<Fila>> ObtenerFilasDeSala(int idSala)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var filas = entities.Fila.Where(e => e.idSala == idSala).ToList();
                    return Result< List<Fila>>.Exito(filas);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Fila>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Fila>>.Fallo(sqlEx.Message);
                }
            }
        }
    }
}
