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
    public interface ISocioServicio
    {
        [OperationContract]
        Task<ResultDTO> RegistrarSocio(SocioDTO socioDTO);
        [OperationContract]
        Task<ResultDTO> ModificarSocio(SocioDTO socioDTO);
        [OperationContract]
        Task<ResultDTO> InhabilitarCuentaSocio(int idSocio);
        [OperationContract]
        Task<ListaSociosDTO> ObtenerSocios();
        [OperationContract]
        Task<ListaSociosDTO> BuscarSocioPorFolio(string folio);
    }
}
