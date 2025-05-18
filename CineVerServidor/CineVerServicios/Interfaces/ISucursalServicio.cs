using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CineVerEntidades;
using CineVerServicios.DTO;

namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    public interface ISucursalServicio
    {
        [OperationContract]
        Task<ResultDTO> GuardarSucursal(SucursalDTO sucursalDTO);

        [OperationContract]
        Task<ResultDTO> ActualizarSucursal(int idSucursal, SucursalDTO sucursalDTO);

        [OperationContract]
        Task<ResultDTO> CerrarSucursal(int idSucursal);

        [OperationContract]
        Task<ListaSucursalesDTO> ObtenerSucursales();
        [OperationContract]
        Task<ListaFilasAsientosDTO> ObtenerAsientosPorFila(int idSala);
    }
}
