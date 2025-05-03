using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    public class AsientoServicio : IAsientoServicio
    {
        private GestorAsiento gestorAsiento = new GestorAsiento();
        public Task<string> AgregarAsiento(AsientoDTO asientoDTO)
        {
            var result = gestorAsiento.AgregarAsiento(asientoDTO);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EditarAsiento(AsientoDTO asientoEditado, AsientoDTO asientoOriginal)
        {
            var result = gestorAsiento.EditarAsiento(asientoEditado,asientoOriginal);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EliminarAsiento(AsientoDTO asiento)
        {
            var result = gestorAsiento.EliminarAsiento(asiento);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<int> ObtenerIdAsiento(string letraColumna, int idFila)
        {
            var result = gestorAsiento.ObtenerIDAsiento(letraColumna,idFila);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(-1);
            }
        }

        public Task<ListaAsientosDTO> ObtenerListaAsientosPorFila(int idFila)
        {
            var result = gestorAsiento.ObtenerAsientosPorFila(idFila);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(new ListaAsientosDTO
                {
                    Asientos = new List<AsientoDTO>(),
                    Result = new ResultDTO(false,result.Error)
                });
            }
        }
    }
}
