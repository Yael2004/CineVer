using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VentaServicio : IVentaServicio
    {
        public Task<ResultDTO> ActualizarPromocion(PromocionDTO promocion)
        {
            throw new NotImplementedException();
        }

        public Task<ListaPromocionesDTO> ObtenerPromociones(int idSucursal)
        {
            throw new NotImplementedException();
        }

        public Task<ListaPromocionesDTO> ObtenerPromocionesBoletos(int idSucursal)
            {
            throw new NotImplementedException();
        }

        public Task<ListaPromocionesDTO> ObtenerPromocionesDulceria(int idSucursal)
                {
            throw new NotImplementedException();
            }

        public Task<ResultDTO> RealizarPagoBoletos(VentaDTO venta)
            {
            throw new NotImplementedException();
        }

        public Task<ResultDTO> RealizarPagoDulceria(VentaDTO venta)
                {
            throw new NotImplementedException();
            }

        public Task<ResultDTO> RegistrarPromocion(int idSucursal, PromocionDTO promocion)
        {
            throw new NotImplementedException();
        }
    }
}
