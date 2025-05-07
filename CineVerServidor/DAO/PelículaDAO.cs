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
                    var pelicula = entities.Película.Where(e => e.director.Equals(director) && e.nombre.Equals(nombrePelicula)).FirstOrDefault();
                    if(pelicula != null)
                    {
                        return Result<bool>.Exito(true);
                    }
                    else
                    {
                        return Result<bool>.Exito(false);
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
        public Result<string> AgregarPelicula(Película pelicula)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    if(ExistePelicula(pelicula.nombre, pelicula.director).Valor)
                    {
                        return Result<string>.Fallo("La película ya ha sido agregada previamente");
                    }
                    else
                    {
                        entities.Película.Add(pelicula);
                        entities.SaveChanges();
                        return Result<string>.Exito("Pelicula agregada exitosamente");
                    }
                }
                catch(DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch(SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch(Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }
        public Result<int> ObtenerIdPelicula(string nombre, string director)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var pelicula = entities.Película.Where(e=>e.nombre.Equals(nombre) && e.director.Equals(director)).FirstOrDefault();
                    return Result<int>.Exito(pelicula.idPelicula);
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
        public Result<String> EditarPelicula(Película peliculaEditada, Película peliculaOriginal)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    int idPelicula = ObtenerIdPelicula(peliculaOriginal.nombre,peliculaOriginal.director).Valor;
                    Película peliculaNueva = entities.Película.Find(idPelicula);
                    if (peliculaNueva != null)
                    {
                        peliculaNueva.nombre = peliculaEditada.nombre;
                        peliculaNueva.director = peliculaEditada.director;
                        peliculaNueva.duracion = peliculaEditada.duracion;
                        peliculaNueva.genero = peliculaEditada.genero;
                        peliculaNueva.sinopsis = peliculaEditada.sinopsis;
                        entities.SaveChanges();
                        return Result<string>.Exito("Pelicula editada exitosamente");
                    }
                    else
                    {
                        return Result<String>.Fallo("No se encontro la pelicula");
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
        public Result<string> EliminarPelicula(Película pelicula)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var peliculaEliminar = entities.Película.Find(ObtenerIdPelicula(pelicula.nombre,pelicula.director).Valor);
                    if(peliculaEliminar != null)
                    {
                        entities.Película.Remove(peliculaEliminar);
                        entities.SaveChanges();
                        return Result<string>.Exito("Pelicula eliminada exitosamente");
                    }
                    else
                    {
                        return Result<string>.Fallo("Pelicula no encontrada");
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
        public Result<List <Película>> ObtenerPeliculasPorNombre(int idSucursal, string nombre)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    List <Película> listaPeliculas = entities.Película.Where(e=>e.nombre.Equals(nombre)&&e.idSucursal == idSucursal).ToList();
                    return Result<List<Película>>.Exito(listaPeliculas);
                }
                catch (DbEntityValidationException ex)
                {
                    return Result < List < Película >>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result< List<Película>>.Fallo(sqlEx.Message);
                }
            }
        }
    }
}
