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
            throw new NotImplementedException();
        }

        public Task<ResultDTO> CerrarSucursal(int idSucursal)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
