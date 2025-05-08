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
    public class SalaDAO
    {
        public SalaDAO() { }
        public Result<List <Sala>> ObtenerSalasPorSucursal(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var salas = entities.Sala.Where(e=>e.idSucursal == idSucursal).ToList();
                    return Result<List<Sala>>.Exito(salas);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Sala>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Sala>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<int> ObtenerIDSala(int? idSucursal, string nombreSala)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var id = entities.Sala.Where(e=>e.idSucursal == idSucursal && e.nombre.Equals(nombreSala)).FirstOrDefault();
                    return Result<int>.Exito(id.idSala);
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
        public Result<bool> VerificarExistenciaSala (int? idSucursal, string nombreSala)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var sala = entities.Sala.Where(e => e.idSucursal == idSucursal && e.nombre.Equals(nombreSala)).FirstOrDefault();
                    if(sala == null)
                    {
                        return Result<bool>.Exito(false);
                    }
                    else
                    {
                        return Result<bool>.Exito(true);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<bool>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<bool>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<string> EditarSala(Sala salaEditada, Sala salaOriginal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    int id = ObtenerIDSala(salaOriginal.idSucursal, salaOriginal.nombre).Valor;
                    var salaInsertar = entities.Sala.Find(id);
                    if (salaInsertar != null)
                    {
                        salaInsertar.estadoSala = salaEditada.estadoSala;
                        salaInsertar.nombre = salaEditada.nombre;
                        salaInsertar.numeroFilas = salaEditada.numeroFilas.Value;
                        salaInsertar.descripcion = salaEditada.descripcion;
                        entities.SaveChanges();
                        return Result<string>.Exito("Sala editada");
                    }
                    else
                    {
                        return Result<string>.Fallo("No se encontro la sala que se quiere editar");
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
            }
        }
        public Result<string> AgregarSala(Sala sala)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    if (VerificarExistenciaSala(sala.idSucursal, sala.nombre).Valor)
                    {
                        return Result<string>.Fallo("Ya se agregó una sala con ese nombre");
                    }
                    else
                    {
                        entities.Sala.Add(sala);
                        entities.SaveChanges();
                        return Result<string>.Exito("Sala agregada exitosamente");
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
            }
        }
        public Result<string> EliminarSala(Sala sala)
        {
        using (CineVerEntities entities = new CineVerEntities())
        {
            try
            {
                int id = ObtenerIDSala(sala.idSucursal, sala.nombre).Valor;
                var salaInsertar = entities.Sala.Find(id);
                    if (salaInsertar != null)
                    {
                        entities.Sala.Remove(salaInsertar);
                        entities.SaveChanges();
                        return Result<string>.Exito("Sala eliminada exitosamente");
                }
                    else
                    {
                        return Result<string>.Fallo("Sala no encontrada");
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
        }
    }
}
}
