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
        public Task<int> ObtenerIdPelicula(string nombre, string director)
        {
            throw new NotImplementedException();
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

        public Task<ListaPeliculasDTO> ObtenerPeliculasPorNombre()
        {
            throw new NotImplementedException();
        }
    }
}
