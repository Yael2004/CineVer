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
    public interface IPelículaServicio
    {
        [OperationContract]
        Task<ListaPeliculasDTO> ObtenerListaPeliculas(int idSucursal);
        [OperationContract]
        Task<ListaPeliculasDTO> ObtenerPeliculasPorNombre();
        [OperationContract]
        Task<int> ObtenerIdPelicula(string nombre, string director);

    }
}
