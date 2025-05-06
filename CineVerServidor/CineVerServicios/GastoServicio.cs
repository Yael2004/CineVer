using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    public class GastoServicio : IGastoServicio
    {
        private GestorGasto _gestorGasto = new GestorGasto();

        public Task<ResultDTO> RegistrarGasto(GastoDTO gastoDTO)
        {
            var resultado = _gestorGasto.RegistrarGasto(gastoDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ListaGastosDTO> ObtenerGastosDelDia(DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }
}
