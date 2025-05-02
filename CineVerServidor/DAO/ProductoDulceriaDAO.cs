using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DAO
{
    public class ProductoDulceriaDAO
    {
        public Result<List<ProductoDulceria>> ObtenerListaProductosDulceria()
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var productos = (from p in entities.ProductoDulceria
                                         select p).ToList();
                    
                    if (productos.Count > 0)
                    {
                        return Result<List<ProductoDulceria>>.Exito(productos);
                    }

                    return Result<List<ProductoDulceria>>.Fallo("No hay películas agregadas a la sucursal que consulta");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<List<ProductoDulceria>>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<List<ProductoDulceria>>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<List<ProductoDulceria>>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> AgregarInventario(Dictionary<int, int> inventario)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try 
                {
                    foreach (var item in inventario)
                    {
                        var producto = entities.ProductoDulceria.Find(item.Key);
                        if (producto != null)
                        {
                            producto.cantidadInventario = item.Value;
                        }
                    }
                    entities.SaveChanges();
                    return Result<string>.Exito("Inventario actualizado correctamente");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> AgregarProductoDulceria(ProductoDulceria producto)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    entities.ProductoDulceria.Add(producto);
                    entities.SaveChanges();
                    return Result<string>.Exito("Producto agregado correctamente");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }

        public Result<ProductoDulceria> ObtenerProductoDulceria(int idProducto)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var producto = entities.ProductoDulceria.Find(idProducto);
                    if (producto != null)
                    {
                        return Result<ProductoDulceria>.Exito(producto);
                    }
                    return Result<ProductoDulceria>.Fallo("No se encontró el producto");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<ProductoDulceria>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<ProductoDulceria>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<ProductoDulceria>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> ActualizarProductoDulceria(ProductoDulceria producto)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var productoExistente = entities.ProductoDulceria.Find(producto.idProducto);
                    if (productoExistente != null)
                    {
                        entities.Entry(productoExistente).CurrentValues.SetValues(producto);
                        entities.SaveChanges();
                        return Result<string>.Exito("Producto actualizado correctamente");
                    }
                    return Result<string>.Fallo("No se encontró el producto");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }

        public Result<string> ReportarMerma(int idProducto, int cantidadMerma)
        {
            using (CineVerEntities entities = new CineVerEntities())
            {
                try
                {
                    var producto = entities.ProductoDulceria.Find(idProducto);
                    if (producto != null)
                    {
                        producto.cantidadInventario -= cantidadMerma;
                        entities.SaveChanges();
                        return Result<string>.Exito("Merma reportada correctamente");
                    }
                    return Result<string>.Fallo("No se encontró el producto");
                }
                catch (DbEntityValidationException ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
                catch (SqlException sqlEx)
                {
                    return Result<string>.Fallo(sqlEx.Message);
                }
                catch (Exception ex)
                {
                    return Result<string>.Fallo(ex.Message);
                }
            }
        }
    }
}
