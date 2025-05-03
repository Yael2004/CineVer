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
    public interface IAsientoServicio
    {
        [OperationContract]
        Task<string> AgregarAsiento(AsientoDTO asientoDTO);
        [OperationContract]
        Task<string> EditarAsiento(AsientoDTO asientoEditado, AsientoDTO asientoOriginal);
        [OperationContract]
        Task<string> EliminarAsiento(AsientoDTO asiento);
        [OperationContract]
        Task<int> ObtenerIdAsiento(string letraColumna, int idFila);
        [OperationContract]
        Task<ListaAsientosDTO> ObtenerListaAsientosPorFila(int idFila);
    }
}
