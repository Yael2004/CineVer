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
    public interface IEmpleadoServicio
    {
        [OperationContract]
        Task<ResultDTO> RegistrarEmpleado(EmpleadoDTO empleadoDTO);
        [OperationContract]
        Task<ResultDTO> ModificarEmpleado(EmpleadoDTO empleadoDTO);
        [OperationContract]
        Task<ResultDTO> InhabilitarEmpleado(int idEmpleado);
        [OperationContract]
        Task<ListaEmpleadosDTO> ObtenerEmpleados();
        [OperationContract]
        Task<EmpleadoResponseDTO> BuscarEmpleadoPorMatricula(string matricula);
        [OperationContract]
        Task<ResultDTO> ExisteEmpleado(string matriucla);
        [OperationContract]
        Task<ResultDTO> VerificarInicioSesion(string matricula, byte[] contrasena);
    }
}
