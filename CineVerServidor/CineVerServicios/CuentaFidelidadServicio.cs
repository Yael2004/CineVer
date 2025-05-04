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
    public class CuentaFidelidadServicio : ICuentaFidelidadServicio
    {
        private GestorCuentaFidelidad _gestorCuentaFidelidad = new GestorCuentaFidelidad();

        public Task<Result<string>> RegistrarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var resultado = _gestorCuentaFidelidad.RegistrarCuentaFidelidad(cuentaFidelidadDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new Result<string>(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new Result<string>(false, resultado.Error));
            }
        }

        public Task<Result<CuentaFidelidadDTO>> ObtenerCuentaFidelidadPorIdSocio(int idSocio)
        {
            var resultado = _gestorCuentaFidelidad.ObtenerCuentaFidelidadPorIdSocio(idSocio);

            if (!resultado.EsExitoso)
            {
                return Task.FromResult(Result<CuentaFidelidadDTO>.Fallo(resultado.Error));
            }

            var cuentaFidelidad = resultado.Valor;

            var cuentaFidelidadDTO = new CuentaFidelidadDTO
            {
                IdCuenta = cuentaFidelidad.IdCuenta,
                IdSocio = (int)cuentaFidelidad.IdSocio,
                Puntos = (int)cuentaFidelidad.Puntos
            };

            return Task.FromResult(Result<CuentaFidelidadDTO>.Exito(cuentaFidelidadDTO));
        }

        public Task<Result<string>> ModificarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var resultado = _gestorCuentaFidelidad.ModificarCuentaFidelidad(cuentaFidelidadDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new Result<string>(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new Result<string>(false, resultado.Error));
            }
        }

        public Task<Result<string>> InhabilitarCuentaFidelidad(int idCuenta)
        {
            var resultado = _gestorCuentaFidelidad.InhabilitarCuentaFidelidad(idCuenta);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new Result<string>(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new Result<string>(false, resultado.Error));
            }
        }
    }
}
