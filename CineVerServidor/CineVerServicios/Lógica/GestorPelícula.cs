using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using CineVerEntidades;
using Utilidades;
using CineVerServicios.DTO;

namespace CineVerServicios.Lógica
{
    internal class GestorPelícula
    {
        private PelículaDAO peliculaDAO = new PelículaDAO();
        public Result<ListaPeliculasDTO> ObtenerPeliculasSucursal(int idSucursal)
        {
            var result = peliculaDAO.ObtenerPeliculasPorSucursal(idSucursal);
            if (!result.EsExitoso)
            {
                return Result<ListaPeliculasDTO>.Fallo(result.Error);
            }
            else
            {
                var listaPeliculas = new ListaPeliculasDTO();
                foreach (var pelicula in result.Valor)
                {
                    var peliculaDTO = new PeliculaDTOs();
                    peliculaDTO.nombre = pelicula.nombre;
                    peliculaDTO.director = pelicula.director;
                    peliculaDTO.idPelicula = pelicula.idPelicula;
                    peliculaDTO.duracion = pelicula.duracion;
                    peliculaDTO.idSucursal = pelicula.idSucursal;
                    peliculaDTO.genero = pelicula.genero;
                    peliculaDTO.sinopsis = pelicula.sinopsis;
                    peliculaDTO.poster = pelicula.poster;
                    listaPeliculas.Peliculas.Add(peliculaDTO);
                }
                return Result<ListaPeliculasDTO>.Exito(listaPeliculas);
            }
        }
        public Result<string> AgregarPelicula(PeliculaDTOs pelicula)
        {
            Película peliculaEntitie = new Película();
            peliculaEntitie.idSucursal = pelicula.idSucursal;
            peliculaEntitie.nombre = pelicula.nombre;
            peliculaEntitie.genero = pelicula.genero;
            peliculaEntitie.idPelicula = pelicula.idPelicula;
            peliculaEntitie.duracion = pelicula.duracion;
            peliculaEntitie.director = pelicula.director;
            peliculaEntitie.sinopsis = pelicula.sinopsis;
            peliculaEntitie.poster = pelicula.poster;
            var result = peliculaDAO.AgregarPelicula(peliculaEntitie);
            if (!result.EsExitoso)
            {
                return Result<string>.Fallo(result.Error);
            }
            else
            {
                return Result<string>.Exito(result.Valor);
            }
        }
        public Result<string> EliminarPelicula(PeliculaDTOs pelicula)
        {
            Película peliculaEntitie = new Película();
            peliculaEntitie.idSucursal = pelicula.idSucursal;
            peliculaEntitie.nombre = pelicula.nombre;
            peliculaEntitie.genero = pelicula.genero;
            peliculaEntitie.idPelicula = pelicula.idPelicula;
            peliculaEntitie.duracion = pelicula.duracion;
            peliculaEntitie.director = pelicula.director;
            peliculaEntitie.sinopsis = pelicula.sinopsis;
            peliculaEntitie.poster = pelicula.poster;
            var result = peliculaDAO.EliminarPelicula(peliculaEntitie);
            if (!result.EsExitoso)
            {
                return Result<string>.Fallo(result.Error);
            }
            else
            {
                return Result<string>.Exito(result.Valor);
            }
        }
        public Result<string> EditarPelicula(PeliculaDTOs peliculaEditada, PeliculaDTOs peliculaOriginal)
        {
            Película peliculaEntitie = new Película();
            peliculaEntitie.idSucursal = peliculaEditada.idSucursal;
            peliculaEntitie.nombre = peliculaEditada.nombre;
            peliculaEntitie.genero = peliculaEditada.genero;
            peliculaEntitie.idPelicula = peliculaEditada.idPelicula;
            peliculaEntitie.duracion = peliculaEditada.duracion;
            peliculaEntitie.director = peliculaEditada.director;
            peliculaEntitie.sinopsis = peliculaEditada.sinopsis;
            peliculaEntitie.poster = peliculaEditada.poster;

            Película peliculaEntitie2 = new Película();
            peliculaEntitie2.idSucursal = peliculaOriginal.idSucursal;
            peliculaEntitie2.nombre = peliculaOriginal.nombre;
            peliculaEntitie2.genero = peliculaOriginal.genero;
            peliculaEntitie2.idPelicula = peliculaOriginal.idPelicula;
            peliculaEntitie2.duracion = peliculaOriginal.duracion;
            peliculaEntitie2.director = peliculaOriginal.director;
            peliculaEntitie2.sinopsis = peliculaOriginal.sinopsis;
            peliculaEntitie2.poster = peliculaOriginal.poster;
            var result = peliculaDAO.EditarPelicula(peliculaEntitie,peliculaEntitie2);
            if (!result.EsExitoso)
            {
                return Result<string>.Fallo(result.Error);
            }
            else
            {
                return Result<string>.Exito(result.Valor);
            }
        }
        public Result<int> ObtenerIdPelicula(string nombre, string director)
        {
            var result = peliculaDAO.ObtenerIdPelicula(nombre, director);
            if (!result.EsExitoso)
            {
                return Result<int>.Fallo(result.Error);
            }
            else
            {
                return Result<int>.Exito(result.Valor);
            }
        }
        public Result<ListaPeliculasDTO> ObtenerListaPeliculasPorNombre(int idSucursal,string nombre)
        {
            var result = peliculaDAO.ObtenerPeliculasPorNombre(idSucursal, nombre);
            if (!result.EsExitoso)
            {
                return Result<ListaPeliculasDTO>.Fallo(result.Error);
            }
            else
            {
                var listaPeliculas = new ListaPeliculasDTO();
                foreach (var pelicula in result.Valor)
                {
                    var peliculaDTO = new PeliculaDTOs();
                    peliculaDTO.nombre = pelicula.nombre;
                    peliculaDTO.director = pelicula.director;
                    peliculaDTO.idPelicula = pelicula.idPelicula;
                    peliculaDTO.duracion = pelicula.duracion;
                    peliculaDTO.idSucursal = pelicula.idSucursal;
                    peliculaDTO.genero = pelicula.genero;
                    peliculaDTO.sinopsis = pelicula.sinopsis;
                    peliculaDTO.poster = pelicula.poster;
                    listaPeliculas.Peliculas.Add(peliculaDTO);
                }
                return Result<ListaPeliculasDTO>.Exito(listaPeliculas);
            }
        }
    }
}
