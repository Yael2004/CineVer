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
    public interface ISalaServicio
    {
        [OperationContract]
        Task<ListaSalaDTO> ObtenerSalasPorSucursal(int idSucursal);
        [OperationContract]
        Task<string> AgregarSala(SalaDTO sala);
        [OperationContract]
        Task<string> EditarSala(SalaDTO salaEditada, SalaDTO salaOriginal);
        [OperationContract]
        Task<string> EliminarSala(SalaDTO sala);
        [OperationContract]
        Task<int> ObtenerIdSala(int idSucursal, string nombre);

    }
}
