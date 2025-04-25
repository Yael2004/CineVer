using CineVerEntidades;
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
        GestorPelícula gestorPelicula = new GestorPelícula();
        public Task<int> ObtenerIdPelicula(string nombre, string director)
        {
            throw new NotImplementedException();
        }

        public Task<List<Película>> ObtenerListaPeliculas(int idSucursal)
        {
            var resultado = gestorPelicula.ObtenerPeliculasSucursal(idSucursal);
            if (resultado.EsExitoso)
            {
                return 
            }
            else
            {

            }
        }

        public Task<List<Película>> ObtenerPeliculasPorNombre()
        {
            throw new NotImplementedException();
        }
    }
}
