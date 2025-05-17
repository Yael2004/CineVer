using CineVerEntidades;
using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorCuentaFidelidad
    {
        private CuentaFidelidadDAO cuentaFidelidadDAO = new CuentaFidelidadDAO();

        public Result<string> RegistrarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var cuentaFidelidad = new CuentaFidelidad

            {
                idSocio = cuentaFidelidadDTO.IdSocio,
                puntos = cuentaFidelidadDTO.Puntos
            };

            var resultado = cuentaFidelidadDAO.RegistrarCuentaFidelidad(cuentaFidelidad);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Cuenta de fidelidad registrada exitosamente");
            }
        }

        public Result<CuentaFidelidadDTO> ObtenerCuentaFidelidadPorIdSocio(int idSocio)
        {
            var resultado = cuentaFidelidadDAO.ObtenerCuentaFidelidadPorIdSocio(idSocio);

            if (!resultado.EsExitoso)
            {
                return Result<CuentaFidelidadDTO>.Fallo(resultado.Error);
            }

            var cuentaFidelidad = resultado.Valor;

            var cuentaFidelidadDTO = new CuentaFidelidadDTO
            {
                IdCuenta = cuentaFidelidad.idCuenta,
                IdSocio = (int)cuentaFidelidad.idSocio,
                Puntos = (decimal)cuentaFidelidad.puntos
            };

            return Result<CuentaFidelidadDTO>.Exito(cuentaFidelidadDTO);
        }

        public Result<string> ModificarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO)
        {
            var cuentaFidelidad = new CuentaFidelidad
            {
                idCuenta = cuentaFidelidadDTO.IdCuenta,
                idSocio = cuentaFidelidadDTO.IdSocio,
                puntos = cuentaFidelidadDTO.Puntos
            };

            var resultado = cuentaFidelidadDAO.ModificarCuentaFidelidad(cuentaFidelidad);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Cuenta de fidelidad modificada exitosamente");
            }
        }

        public Result<string> InhabilitarCuentaFidelidad(int idCuenta)
        {
            var resultado = cuentaFidelidadDAO.InhabilitarCuentaFidelidad(idCuenta);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Cuenta de fidelidad inhabilitada exitosamente");
            }
        }
    }
}
