using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios
{
    public class CorteCajaServicio : ICorteCajaServicio
    {
        private GestorCorteCaja _gestorCorteCaja = new GestorCorteCaja();
        public Task<ResultDTO> GuardarCorteCaja(CorteCajaDTO corteCajaDTO)
        {
            var resultado = _gestorCorteCaja.GuardarCorteCaja(corteCajaDTO);
            
            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<CorteCajaInicioFinDTO> ObtenerInicioFinDia(DateTime fecha)
        {
            var resultado = _gestorCorteCaja.ObtenerInicioFinDia(fecha);

            if (resultado.Result.ResultDTO.EsExitoso)
            {
                var corteCaja = new CorteCajaInicioFinDTO
                {
                    InicioDia = (decimal)resultado.Result.InicioDia,
                    VentaTotal = (decimal)resultado.Result.VentaTotal
                };
                return Task.FromResult(corteCaja);
            }
            else
            {
                return Task.FromResult<CorteCajaInicioFinDTO>(null);
            }
        }
    }
}
