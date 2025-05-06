using CineVerEntidades;
using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorCorteCaja
    {
        private CorteCajaDAO corteCajaDAO = new CorteCajaDAO();
        public Result<string> GuardarCorteCaja(CorteCajaDTO corteCajaDTO)
        {
            var corteCaja = new CorteCaja
            {
                idSucursal = corteCajaDTO.IdSucursal,
                fechaCorte = corteCajaDTO.FechaCorte,
                inicioDia = corteCajaDTO.InicioDia,
                ventaTotal = corteCajaDTO.VentaTotal,
                efectivoEsperado = corteCajaDTO.EfectivoEsperado,
                efectivoCaja = corteCajaDTO.EfectivoCaja,
                gastos = corteCajaDTO.Gastos,
                ganancias = corteCajaDTO.Ganancias,
                diferenciaEfectivo = corteCajaDTO.DiferenciaEfectivo,
            };

            var resultado = corteCajaDAO.GuardarCorteCaja(corteCaja);
            
            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Corte de caja guardado correctamente");
            }
        }

        public Task<CorteCajaInicioFinDTO> ObtenerInicioFinDia(DateTime fecha)
        {
            var resultado = corteCajaDAO.ObtenerInicioYFinDia(fecha);

            if (resultado.EsExitoso)
            {
                var corteCaja = new CorteCajaInicioFinDTO
                {
                    InicioDia = (decimal)resultado.Valor.inicioDia,
                    VentaTotal = (decimal)resultado.Valor.ventaTotal
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
