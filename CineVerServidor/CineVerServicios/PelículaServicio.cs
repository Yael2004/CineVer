using CineVerEntidades;
using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    public class PelículaServicio : IPelículaServicio
    {
        private GestorPelícula _gestorPelicula = new GestorPelícula();

        public Task<string> AgregarPelicula(PeliculaDTOs pelicula)
        {
            var resultado = _gestorPelicula.AgregarPelicula(pelicula);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Error);
            }
        }

        public Task<string> EditarPelicula(PeliculaDTOs peliculaEditada, PeliculaDTOs peliculaOriginal)
        {
            var resultado = _gestorPelicula.EditarPelicula(peliculaEditada,peliculaOriginal);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Error);
            }
        }

        public Task<string> EliminarPelicula(PeliculaDTOs pelicula)
        {
            var resultado = _gestorPelicula.EliminarPelicula(pelicula);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(resultado.Error);
            }
        }

        public Task<int> ObtenerIdPelicula(string nombre, string director)
        {
            var resultado = _gestorPelicula.ObtenerIdPelicula(nombre, director);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(-1);
            }
        }

        public Task<ListaPeliculasDTO> ObtenerListaPeliculas(int idSucursal)
        {
            var resultado = _gestorPelicula.ObtenerPeliculasSucursal(idSucursal);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(new ListaPeliculasDTO
                {
                    Peliculas = new List<PeliculaDTOs>(),
                    Result = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<PeliculaDTOs> ObtenerPeliculaPorID(int idPelicula)
        {
            var resultado = _gestorPelicula.ObtenerPeliculaPorID(idPelicula);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(new PeliculaDTOs
                {
                   
                });
            }
        }

        public Task<ListaPeliculasDTO> ObtenerPeliculasPorNombre(int idSucursal, string nombre)
        {
            var resultado = _gestorPelicula.ObtenerListaPeliculasPorNombre(idSucursal,nombre);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(new ListaPeliculasDTO
                {
                    Peliculas = new List<PeliculaDTOs>(),
                    Result = new ResultDTO(false, resultado.Error)
                });
            }
        }
    }
}
