using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using CineVerEntidades;
using Utilidades;

namespace CineVerServicios.Lógica
{
    internal class GestorPelícula
    {
        private PelículaDAO peliculaDAO = new PelículaDAO();
        public Result<List<Película>> ObtenerPeliculasSucursal(int idSucursal)
        {
            var result = peliculaDAO.ObtenerPeliculasPorSucursal(idSucursal);
            if (!result.EsExitoso)
            {
                return Result<List<Película>>.Fallo(result.Error);
            }
            else
            {
                return result;
            }
        }
    }
}
