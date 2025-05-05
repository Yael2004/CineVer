using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios
{
    public class CuentaFidelidadServicio : ICuentaFidelidadServicio
    {
        private GestorCuentaFidelidad _gestorCuentaFidelidad = new GestorCuentaFidelidad();

        public Task<ResultDTO> RegistrarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var resultado = _gestorCuentaFidelidad.RegistrarCuentaFidelidad(cuentaFidelidadDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<CuentaFidelidadResponseDTO> ObtenerCuentaFidelidadPorIdSocio(int idSocio)
        {
            var resultado = _gestorCuentaFidelidad.ObtenerCuentaFidelidadPorIdSocio(idSocio);

            if (!resultado.EsExitoso)
            {
                return Task.FromResult(new CuentaFidelidadResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }

            var cuentaFidelidad = resultado.Valor;

            var cuentaFidelidadDTO = new CuentaFidelidadDTO
            {
                IdCuenta = cuentaFidelidad.IdCuenta,
                IdSocio = (int)cuentaFidelidad.IdSocio,
                Puntos = (int)cuentaFidelidad.Puntos
            };

            return Task.FromResult(new CuentaFidelidadResponseDTO
            {
                cuenta = cuentaFidelidadDTO,
                ResultDTO = new ResultDTO(true, string.Empty)
            });
        }

        public Task<ResultDTO> ModificarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var resultado = _gestorCuentaFidelidad.ModificarCuentaFidelidad(cuentaFidelidadDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> InhabilitarCuentaFidelidad(int idCuenta)
        {
            var resultado = _gestorCuentaFidelidad.InhabilitarCuentaFidelidad(idCuenta);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }
    }
}
