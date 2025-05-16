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
    public interface IFilaServicio
    {
        [OperationContract]
        Task<int> ObtenerIdFila(int idSala, int numeroFila);
        [OperationContract]
        Task<string> AgregarFila(FilaDTO fila);
        [OperationContract]
        Task<string> EditarFila(FilaDTO filaEditada, FilaDTO filaOriginal);
        [OperationContract]
        Task<string> EliminarFila(FilaDTO fila);
        [OperationContract]
        Task<ListaFilasDTO> ObtenerFilasDeSala(int idSala);
    }
}
