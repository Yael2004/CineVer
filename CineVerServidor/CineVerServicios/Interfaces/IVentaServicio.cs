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
    public interface IVentaServicio
    {
        [OperationContract]
        Task<ResultDTO> RealizarPagoDulceria(VentaDTO venta);
        [OperationContract]
        Task<ResultDTO> RealizarPagoBoletos(VentaDTO venta);
        [OperationContract]
        Task<ListaPromocionesDTO> ObtenerPromociones(int idSucursal);
        [OperationContract]
        Task<ListaPromocionesDTO> ObtenerPromocionesDulceria(int idSucursal);
        [OperationContract]
        Task<ListaPromocionesDTO> ObtenerPromocionesBoletos(int idSucursal);
        [OperationContract]
        Task<ResultDTO> ActualizarPromocion(PromocionDTO promocion);
        [OperationContract]
        Task<ResultDTO> RegistrarPromocion(int idSucursal, PromocionDTO promocion);
        [OperationContract]
        Task<ListaVentasDTO> ObtenerVentasPorAnio(int anio, int idSucursal);
        [OperationContract]
        Task<ListaVentasDTO> ObtenerVentasPorMes(int mes, int anio, int idSucursal);
        [OperationContract]
        Task<ResultDTO> ObtenerVentaPorFolio(string folio);
        [OperationContract]
        Task<ResultDTO> VerificarFechaVentaParaDevolucion(string folio);
        [OperationContract]
        Task<VentaTipoResponseDTO> ObtenerVentasDeBoletosDelDia(int idSucursal);
        [OperationContract]
        Task<VentaTipoResponseDTO> ObtenerVentasDeDulceriaDelDia(int idSucursal);
        [OperationContract]
        Task<VentaTipoResponseDTO> ObtenerVentasEnEfectivoDelDia(int idSucursal);
    }
}
