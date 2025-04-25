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
                    var peliculaDTO = new PeliculaDTOs
                    {
                        Nombre = pelicula.nombre
                    };
                    listaPeliculas.Peliculas.Add(peliculaDTO);
                }
                return Result<ListaPeliculasDTO>.Exito(listaPeliculas);
            }
        }
    }
}
