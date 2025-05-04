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
        Task<Result<string>> RegistrarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO);
        [OperationContract]
        Task<Result<CuentaFidelidadDTO>> ObtenerCuentaFidelidadPorIdSocio(int idSocio);
        [OperationContract]
        Task<Result<string>> ModificarCuentaFidelidad(CuentaFidelidadDTO cuentaFidelidadDTO);
        [OperationContract]
        Task<Result<string>> InhabilitarCuentaFidelidad(int idSocio);
    }
}
