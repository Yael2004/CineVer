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
    public class SucursalDAO
    {
        public SucursalDAO() { }

        public Result<List<Sucursal>> ObtenerSucursales()
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var sucursales = entities.Sucursal.ToList();
                    if (sucursales.Count == 0)
                    {
                        return Result<List<Sucursal>>.Fallo("No hay sucursales registradas");
                    }
                    return Result<List<Sucursal>>.Exito(sucursales);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<Sucursal>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<Sucursal>>.Fallo(sqlEx.Message);
                }
            }
        }

        public Result<List<Fila>> ObtenerAsientosPorFila(int idSala)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var filas = entities.Fila.Include("Asiento").Where(f => f.idSala == idSala).ToList();
                    if (filas.Count == 0)
                    {
                        return Result<List<Fila>>.Fallo("No hay filas registradas");
                    }
                    return Result<List<Fila>>.Exito(filas);
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

        public Result<string> AgregarSucursal(Sucursal sucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.Sucursal.Add(sucursal);
                    entities.SaveChanges();
                    return Result<string>.Exito("Sucursal agregada correctamente");
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

        public Result<string> ActualizarSucursal(int idSucursal, Sucursal sucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var sucursalExistente = entities.Sucursal.Find(idSucursal);
                    if (sucursalExistente != null)
                    {
                        sucursalExistente.nombre = sucursal.nombre;
                        sucursalExistente.estado = sucursal.estado;
                        sucursalExistente.ciudad = sucursal.ciudad;
                        sucursalExistente.calle = sucursal.calle;
                        sucursalExistente.numeroEnLaCalle = sucursal.numeroEnLaCalle;
                        sucursalExistente.codigoPostal = sucursal.codigoPostal;
                        sucursalExistente.horaApertura = sucursal.horaApertura;
                        sucursalExistente.horaCierre = sucursal.horaCierre;
                        sucursalExistente.estadoSucursal = sucursal.estadoSucursal;

                        entities.SaveChanges();

                        return Result<string>.Exito("Sucursal actualizada correctamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Sucursal no encontrada");
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

        public Result<string> CerrarSucursal(int idSucursal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var sucursal = entities.Sucursal.Find(idSucursal);
                    if (sucursal != null)
                    {
                        sucursal.estadoSucursal = "Cerrada";
                        entities.SaveChanges();
                        return Result<string>.Exito("Sucursal eliminada correctamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Sucursal no encontrada");
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
