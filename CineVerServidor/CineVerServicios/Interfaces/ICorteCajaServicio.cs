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
    public interface ICorteCajaServicio
    {
        [OperationContract]
        Task<ResultDTO> GuardarCorteCaja(CorteCajaDTO corteCajaDTO);
        [OperationContract]
        Task<CorteCajaInicioFinDTO> ObtenerInicioFinDia(DateTime fecha);
    }
}
