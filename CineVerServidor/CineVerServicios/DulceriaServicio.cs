using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using Utilidades;

namespace CineVerServicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DulceriaServicio : IDulceriaServicio
    {
        public async Task<ResultDTO> ActualizarProductoDulceria(ProductoDulceriaDTO producto)
        {
            var resultado = await GestorDulceria.ActualizarProductoDulceria(producto);
            return resultado;
        }

        public async Task<ResultDTO> AgregarInventario(Dictionary<int, int> inventario)
        {
            var resultado = await GestorDulceria.AgregarInventario(inventario);
            return resultado;
        }

        public async Task<ResultDTO> AgregarProductoDulceria(ProductoDulceriaDTO producto)
        {
            var resultado = await GestorDulceria.AgregarProductoDulceria(producto);
            return resultado;
        }

        public async Task<ProductoDulceriaResponseDTO> ObtenerProductoDulceria(int idProducto)
        {
            var resultado = await GestorDulceria.ObtenerProductoDulceria(idProducto);

            if (resultado.EsExitoso)
            {
                return new ProductoDulceriaResponseDTO
                {
                    Producto = resultado.Valor,
                    ResultDTO = ResultDTO.Exito()
                };
            }
            else
            {
                return new ProductoDulceriaResponseDTO
                {
                    ResultDTO = ResultDTO.Fallo(resultado.Error)
                };
            }
        }

        public async Task<ListaProductosDulceriaDTO> ObtenerProductosDulceria()
        {
            var resultado = await GestorDulceria.ObtenerProductosDulceria();
            return resultado;
        }

        public async Task<ResultDTO> ReportarMerma(int idProducto, int cantidadMerma)
        {
            var resultado = await GestorDulceria.ReportarMerma(idProducto, cantidadMerma);
            return resultado;
        }
    }
}
