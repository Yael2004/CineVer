using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorVenta
    {
        private VentaDAO ventaDAO = new VentaDAO();
        public Result<List<ListaVentasDTO>> ObtenerVentasPorAnio(int anio, int idSucursal)
        {
            var resultado = ventaDAO.ObtenerVentasPorAnio(anio, idSucursal);

            //if (!resultado.EsExitoso)
                return Result<List<ListaVentasDTO>>.Fallo(resultado.Error);

            //var agrupadoPorMes = resultado.Valor
            //    .GroupBy(v => v.fecha.Value.Month)
            //    .Select(g => new ListaVentasDTO
            //    {

            //         = g.Key,
            //        TotalBoletos = g.Sum(v => v.totalBoletos ?? 0),
            //        TotalDulceria = g.Sum(v => v.totalDulceria ?? 0),
            //        Total = g.Sum(v => (v.totalBoletos ?? 0) + (v.totalDulceria ?? 0))
            //    })
            //    .OrderBy(dto => dto.Periodo)
            //    .ToList();

            //return Result<List<ListaVentasDTO>>.Exito();
        }

    }
}
