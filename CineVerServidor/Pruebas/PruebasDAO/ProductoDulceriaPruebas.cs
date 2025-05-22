using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class ProductoDulceriaPruebas
    {
        private ProductoDulceriaDAO dao;
        private List<int> productosDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new ProductoDulceriaDAO();
            productosDePrueba = new List<int>();
        }


        [TestMethod]
        public void ObtenerListaProductosDulceria_Exito()
        {
            var resultado = dao.ObtenerListaProductosDulceria();
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Count > 0);
        }

        [TestMethod]
        public void ObtenerListaProductosDulceria_SinProductos()
        {
            var resultado = dao.ObtenerListaProductosDulceria();
            Assert.IsFalse(resultado.Valor.Count == 0);
        }

        [TestMethod]
        public void AgregarInventario_Exito()
        {
            var inventario = new Dictionary<int, int>();

            var productoPrueba = CrearProductoPrueba();
            var resAdd = dao.AgregarProductoDulceria(productoPrueba);
            Assert.IsTrue(resAdd.EsExitoso);
            productosDePrueba.Add(productoPrueba.idProducto);

            inventario.Add(productoPrueba.idProducto, 50);
            var resultado = dao.AgregarInventario(inventario);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Inventario actualizado correctamente", resultado.Valor);
        }

        [TestMethod]
        public void AgregarInventario_ProductoNoExiste()
        {
            var inventario = new Dictionary<int, int> { { -1, 10 } };
            var resultado = dao.AgregarInventario(inventario);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void AgregarProductoDulceria_Exito()
        {
            var producto = CrearProductoPrueba();
            var resultado = dao.AgregarProductoDulceria(producto);
            Assert.IsTrue(resultado.EsExitoso);
            productosDePrueba.Add(producto.idProducto);
        }

        [TestMethod]
        public void AgregarProductoDulceria_DatosFaltantes()
        {
            var producto = new ProductoDulceria
            {
                idSucursal = 1,
                cantidadInventario = 10
            };
            var resultado = dao.AgregarProductoDulceria(producto);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerProductoDulceria_Exito()
        {
            var producto = CrearProductoPrueba();
            dao.AgregarProductoDulceria(producto);
            productosDePrueba.Add(producto.idProducto);

            var resultado = dao.ObtenerProductoDulceria(producto.idProducto);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsNotNull(resultado.Valor);
            Assert.AreEqual(producto.idProducto, resultado.Valor.idProducto);
        }

        [TestMethod]
        public void ObtenerProductoDulceria_NoExiste()
        {
            var resultado = dao.ObtenerProductoDulceria(-1);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("No se encontró el producto", resultado.Error);
        }

        [TestMethod]
        public void ActualizarProductoDulceria_Exito()
        {
            var producto = CrearProductoPrueba();
            dao.AgregarProductoDulceria(producto);
            productosDePrueba.Add(producto.idProducto);

            producto.cantidadInventario = 99;
            var resultado = dao.ActualizarProductoDulceria(producto);

            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ActualizarProductoDulceria_NoExiste()
        {
            var producto = CrearProductoPrueba();
            producto.idProducto = -1;

            var resultado = dao.ActualizarProductoDulceria(producto);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("No se encontró el producto", resultado.Error);
        }

        [TestMethod]
        public void ReportarMerma_Exito()
        {
            var producto = CrearProductoPrueba();
            dao.AgregarProductoDulceria(producto);
            productosDePrueba.Add(producto.idProducto);

            int merma = 2;
            var resultado = dao.ReportarMerma(producto.idProducto, merma);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ReportarMerma_NoExisteProducto()
        {
            var resultado = dao.ReportarMerma(-1, 5);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("No se encontró el producto", resultado.Error);
        }

        [TestMethod]
        public void ObtenerNombreProductos_Exito()
        {
            var producto = CrearProductoPrueba();
            dao.AgregarProductoDulceria(producto);
            productosDePrueba.Add(producto.idProducto);

            var resultado = dao.ObtenerNombreProductos((int)producto.idSucursal);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Contains(producto.nombre));
        }

        [TestMethod]
        public void ObtenerNombreProductos_SinResultados()
        {
            var resultado = dao.ObtenerNombreProductos(99999);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("No hay productos en la sucursal especificada", resultado.Error);
        }

        private ProductoDulceria CrearProductoPrueba()
        {
            return new ProductoDulceria
            {
                nombre = "Dulce Test",
                idSucursal = 2,
                cantidadInventario = 10,
                precioVenta = 2,
                costoUnitario = 4
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in productosDePrueba)
                {
                    var producto = context.ProductoDulceria.FirstOrDefault(p => p.idProducto == id);
                    if (producto != null)
                    {
                        context.ProductoDulceria.Remove(producto);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
