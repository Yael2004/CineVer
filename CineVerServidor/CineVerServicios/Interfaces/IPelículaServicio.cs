using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CineVerEntidades;

namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    public interface IPelículaServicio
    {
        [OperationContract]
        Task<List<Película>> ObtenerListaPeliculas(int idSucursal);
        [OperationContract]
        Task<List<Película>> ObtenerPeliculasPorNombre();
        [OperationContract]
        Task<int> ObtenerIdPelicula(string nombre, string director);

    }
}
