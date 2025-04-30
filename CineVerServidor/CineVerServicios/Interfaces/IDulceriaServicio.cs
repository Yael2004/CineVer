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
    public interface IDulceriaServicio
    {
        [OperationContract]
        Task<ListaProductosDulceriaDTO> ObtenerProductosDulceria();

        [OperationContract]
        Task<ResultDTO> AgregarInventario(Dictionary<int, int> inventario);

        [OperationContract]
        Task<ResultDTO> AgregarProductoDulceria(ProductoDulceriaDTO producto);

        [OperationContract]
        Task<ResultDTO> ObtenerProductoDulceria(int idProducto);

        [OperationContract]
        Task<ResultDTO> ActualizarProductoDulceria(ProductoDulceriaDTO producto);

        [OperationContract]
        Task<ResultDTO> ReportarMerma(int idProducto, int cantidadMerma);
    }
}
