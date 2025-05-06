using CineVerServicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    public interface IGastoServicio
    {
        [OperationContract]
        Task<ResultDTO> RegistrarGasto(GastoDTO gastoDTO);
        [OperationContract]
        Task<ListaGastosDTO> ObtenerGastosDelDia(DateTime fecha);
    }
}
