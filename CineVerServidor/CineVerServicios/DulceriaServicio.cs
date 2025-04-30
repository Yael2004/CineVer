using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CineVerServicios.DTO;
using CineVerServicios.Interfaces;

namespace CineVerServicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DulceriaServicio : IDulceriaServicio
    {
        public Task<ResultDTO> ActualizarProductoDulceria(ProductoDulceriaDTO producto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> AgregarInventario(Dictionary<int, int> inventario)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> AgregarProductoDulceria(ProductoDulceriaDTO producto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> ObtenerProductoDulceria(int idProducto)
        {
            throw new NotImplementedException();
        }

        public Task<ListaProductosDulceriaDTO> ObtenerProductosDulceria()
        {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> ReportarMerma(int idProducto, int cantidadMerma)
        {
            throw new NotImplementedException();
        }
    }
}
