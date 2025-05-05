using CineVerServicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    public interface ICuentaFidelidadServicio
    {
        [OperationContract]
        Task<ResultDTO> RegistrarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO);
        [OperationContract]
        Task<CuentaFidelidadResponseDTO> ObtenerCuentaFidelidadPorIdSocio(int idSocio);
        [OperationContract]
        Task<ResultDTO> ModificarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO);
        [OperationContract]
        Task<ResultDTO> InhabilitarCuentaFidelidad(int idSocio);
    }
}
