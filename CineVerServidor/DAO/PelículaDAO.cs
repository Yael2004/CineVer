using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CineVerEntidades;
using Utilidades;

namespace DAO
{
    public class PelículaDAO
    {
        public PelículaDAO() { }
        public  Result<List <Película>> ObtenerPeliculasPorSucursal(int idSucursal)
        {
            using(CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var peliculas = entities.Película.Where(p => p.idSucursal == idSucursal).ToList();
                    if(peliculas.Count > 0)
                    {
                        return Result<List<Película>>.Fallo("No hay películas agregadas a la sucursal que consulta");
                    }
                    return Result<List<Película>>.Exito(peliculas);
                }
                catch(DbEntityValidationException ex)
                {
                    return Result<List<Película>>.Fallo(ex.Message);
                }
                catch(SqlException sqlEx)
                {
                    return Result<List<Película>>.Fallo(sqlEx.Message);
                }
            }
        }
        public Result<bool> ExistePelicula(String nombrePelicula, String director)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var pelicula = entities.Película.Where(e => e.director.Equals(director) && e.nombre.Equals(nombrePelicula));
                    if(pelicula != null)
                    {

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
    }
}
