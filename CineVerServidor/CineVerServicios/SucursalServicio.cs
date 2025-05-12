using CineVerEntidades;
using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    public class SucursalServicio : ISucursalServicio
    {
        private GestorSucursal _gestorSucursal = new GestorSucursal();
        public Task<ResultDTO> ActualizarSucursal(int idSucursal, SucursalDTO sucursalDTO)
        {
            var resultado = _gestorSucursal.ActualizarSucursal(idSucursal, sucursalDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> CerrarSucursal(int idSucursal)
        {
            var resultado = _gestorSucursal.CerrarSucursal(idSucursal);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> GuardarSucursal(SucursalDTO sucursalDTO)
        {
            var resultado = _gestorSucursal.AgregarSucursal(sucursalDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ListaSucursalesDTO> ObtenerSucursales()
        {
            var resultado = _gestorSucursal.ObtenerSucursales();

            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(new ListaSucursalesDTO
                {
                    Result = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<ListaFilasDTO> ObtenerAsientosPorFila(int idSala)
        {
            var resultado = _gestorSucursal.ObtenerAsientosPorFila(idSala);
            if (resultado.EsExitoso)
            {
                return Task.FromResult(resultado.Valor);
            }
            else
            {
                return Task.FromResult(new ListaFilasDTO
                {
                    Result = new ResultDTO(false, resultado.Error)
                });
            }
        }
    }
}
