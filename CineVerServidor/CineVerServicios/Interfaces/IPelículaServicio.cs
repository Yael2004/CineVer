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
        Task<PeliculaDTOs> ObtenerPeliculaPorID(int idPelicula);
        [OperationContract]
        Task<ListaPeliculasDTO> ObtenerListaPeliculas(int idSucursal);
        [OperationContract]
        Task<ListaPeliculasDTO> ObtenerPeliculasPorNombre(int idSucursal, string nombre);
        [OperationContract]
        Task<int> ObtenerIdPelicula(string nombre, string director);
        [OperationContract]
        Task<string> AgregarPelicula(PeliculaDTOs pelicula);
        [OperationContract]
        Task<string> EditarPelicula(PeliculaDTOs peliculaEditada, PeliculaDTOs peliculaOriginal);
        [OperationContract]
        Task<string> EliminarPelicula(PeliculaDTOs pelicula);

    }
}
