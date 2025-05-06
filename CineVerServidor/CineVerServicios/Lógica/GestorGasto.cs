using CineVerEntidades;
using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorGasto
    {
        private GastoDAO gastoDAO = new GastoDAO();

        public Result<string> RegistrarGasto(GastoDTO gastoDTO)
        {
            var gasto = new Gasto
            {
                monto = gastoDTO.Monto,
                motivo = gastoDTO.Motivo,
                fecha = gastoDTO.Fecha,
                idSucursal = gastoDTO.IdSucursal,
                idEmpleado = gastoDTO.IdEmpleado
            };

            var resultado = gastoDAO.RegistrarGasto(gasto);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Gasto registrado exitosamente");
            }
        }

        public Result<List<GastoDTO>> ObtenerGastosDelDia(DateTime fecha)
        {
            var resultado = gastoDAO.ObtenerGastosDelDia(fecha);
            
            if (!resultado.EsExitoso)
            {
                return Result<List<GastoDTO>>.Fallo(resultado.Error);
            }
            else
            {
                var listaGastos = new ListaGastosDTO();

                foreach (var gasto in resultado.Valor)
                {
                    var gastoDTO = new GastoDTO
                    {
                        IdGasto = gasto.idGasto,
                        Monto = (decimal)gasto.monto,
                        Motivo = gasto.motivo,
                        Fecha = (DateTime)gasto.fecha,
                        IdSucursal = (int)gasto.idSucursal,
                        IdEmpleado = (int)gasto.idEmpleado
                    };
                    listaGastos.Gastos.Add(gastoDTO);
                }

                return Result<List<GastoDTO>>.Exito(listaGastos.Gastos);
            }
        }
    }
}
