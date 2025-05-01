using CineVerEntidades;
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
    public class GestorDulceria
    {
        private static ProductoDulceriaDAO productoDulceriaDAO = new ProductoDulceriaDAO();

        public static Task<ResultDTO> AgregarProductoDulceria(ProductoDulceriaDTO producto)
        {
            ProductoDulceria productoDulceria = new ProductoDulceria
            {
                nombre = producto.Nombre,
                cantidadInventario = producto.CantidadInventario,
                costoUnitario = producto.CostoUnitario,
                precioVenta = producto.PrecioVentaUnitario,
                imagen = producto.Imagen,
                idSucursal = producto.IdSucursal
            };

            var resultado = productoDulceriaDAO.AgregarProductoDulceria(productoDulceria);
            if (resultado.EsExitoso == true)
            {
                return Task.FromResult(ResultDTO.Exito());
            }
            else
            {
                return Task.FromResult(ResultDTO.Fallo(resultado.Error));
            }
        }

        public static Task<ResultDTO> ActualizarProductoDulceria(ProductoDulceriaDTO producto)
        {
            ProductoDulceria productoDulceria = new ProductoDulceria
            {
                nombre = producto.Nombre,
                cantidadInventario = producto.CantidadInventario,
                costoUnitario = producto.CostoUnitario,
                precioVenta = producto.PrecioVentaUnitario,
                imagen = producto.Imagen,
                idSucursal = producto.IdSucursal
            };

            var resultado = productoDulceriaDAO.ActualizarProductoDulceria(productoDulceria);
            if (resultado.EsExitoso == true)
            {
                return Task.FromResult(ResultDTO.Exito());
            }
            else
            {
                return Task.FromResult(ResultDTO.Fallo(resultado.Error));
            }
        }

        public static Task<Result<ProductoDulceriaDTO>> ObtenerProductoDulceria(int idProducto)
        {
            var resultado = productoDulceriaDAO.ObtenerProductoDulceria(idProducto);
            if (resultado.EsExitoso == true)
            {
                ProductoDulceriaDTO productoDulceriaDTO = new ProductoDulceriaDTO
                {
                    IdProducto = resultado.Valor.idProducto,
                    Nombre = resultado.Valor.nombre,
                    CantidadInventario = resultado.Valor.cantidadInventario ?? 0,
                    CostoUnitario = resultado.Valor.costoUnitario ?? 0,
                    PrecioVentaUnitario = resultado.Valor.precioVenta ?? 0,
                    Imagen = resultado.Valor.imagen,
                    IdSucursal = resultado.Valor.idSucursal ?? 0
                };
                return Task.FromResult(Result<ProductoDulceriaDTO>.Exito(productoDulceriaDTO));
            }
            else
            {
                return Task.FromResult(Result<ProductoDulceriaDTO>.Fallo("No se encontró el producto con el ID especificado."));
            }
        }

        public static Task<ListaProductosDulceriaDTO> ObtenerProductosDulceria()
        {
            var resultado = productoDulceriaDAO.ObtenerListaProductosDulceria();
            if (resultado.EsExitoso == true)
            {
                ListaProductosDulceriaDTO listaProductosDulceriaDTO = new ListaProductosDulceriaDTO
                {
                    Productos = resultado.Valor.Select(p => new ProductoDulceriaDTO
                    {
                        IdProducto = p.idProducto,
                        Nombre = p.nombre,
                        CantidadInventario = p.cantidadInventario ?? 0,
                        CostoUnitario = p.costoUnitario ?? 0,
                        PrecioVentaUnitario = p.precioVenta ?? 0,
                        Imagen = p.imagen,
                        IdSucursal = p.idSucursal ?? 0
                    }).ToList(),

                    ResultDTO = ResultDTO.Exito()
                };
                return Task.FromResult(listaProductosDulceriaDTO);
            }
            else
            {
                return Task.FromResult(new ListaProductosDulceriaDTO
                {
                    Productos = new List<ProductoDulceriaDTO>(),
                    ResultDTO = ResultDTO.Fallo(resultado.Error)
                });
            }
        }

        public static Task<ResultDTO> ReportarMerma(int idProducto, int cantidadMerma)
        {
            var resultado = productoDulceriaDAO.ReportarMerma(idProducto, cantidadMerma);
            if (resultado.EsExitoso == true)
            {
                return Task.FromResult(ResultDTO.Exito());
            }
            else
            {
                return Task.FromResult(ResultDTO.Fallo(resultado.Error));
            }
        }

        public static Task<ResultDTO> AgregarInventario(Dictionary<int, int> inventario)
        {
            var resultado = productoDulceriaDAO.AgregarInventario(inventario);
            if (resultado.EsExitoso == true)
            {
                return Task.FromResult(ResultDTO.Exito());
            }
            else
            {
                return Task.FromResult(ResultDTO.Fallo(resultado.Error));
            }
        }
    }
}
