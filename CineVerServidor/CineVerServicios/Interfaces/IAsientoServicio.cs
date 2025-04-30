using CineVerServicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.Interfaces
{
    public interface IAsientoServicio
    {
        Task<string> AgregarAsiento(AsientoDTO asientoDTO);
        Task<string> EditarAsiento(AsientoDTO asientoEditado, AsientoDTO asientoOriginal);
        Task<string> EliminarAsiento(AsientoDTO asiento);
        Task<int> ObtenerIdAsiento(string letraColumna, int idFila);
        Task<ListaAsientosDTO> ObtenerListaAsientosPorFila(int idFila);
    }
}
