using CineVerEntidades;
using DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class VentaPruebas
    {
        private VentaDAO dao;

        [TestInitialize]
        public void Setup()
        {
            dao = new VentaDAO();
        }


        [TestMethod]
        public void CambiarEstadoAsiento_AsientoExistente_DeberiaActualizarEstado()
        {
            int idAsiento = 2;
            string nuevoEstado = "OCUPADO";

            var resultado = dao.CambiarEstadoAsiento(idAsiento, nuevoEstado);

            Assert.IsTrue(resultado.EsExitoso, $"No se pudo cambiar estado: {resultado.Error}");
        }

        [TestMethod]
        public void ObtenerVentasPorAnio_ConDatos_DeberiaDevolverLista()
        {
            int anio = 2025;
            int idSucursal = 1;

            var resultado = dao.ObtenerVentasPorAnio(anio, idSucursal);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void ObtenerVentasPorMes_ConDatos_DeberiaDevolverLista()
        {
            int mes = 5;
            int anio = 2025;
            int idSucursal = 1;

            var resultado = dao.ObtenerVentasPorMes(mes, anio, idSucursal);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void ObtenerVentaPorFolio_Existe_DeberiaDevolverExito()
        {
            string folio = "00000022";

            var resultado = dao.ObtenerVentaPorFolio(folio);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void VerificarFechaVentaParaDevolucion_VentaReciente_DeberiaPermitir()
        {
            string folio = "00000032";

            var resultado = dao.VerificarFechaVentaParaDevolucion(folio);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void ObtenerVentasDeBoletosDelDia_DeberiaFuncionar()
        {
            int idSucursal = 1;

            var resultado = dao.ObtenerVentasDeBoletosDelDia(idSucursal);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void ObtenerVentasDeDulceriaDelDia_DeberiaFuncionar()
        {
            int idSucursal = 1;

            var resultado = dao.ObtenerVentasDeDulceriaDelDia(idSucursal);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void ObtenerVentasEnEfectivoDelDia_DeberiaFuncionar()
        {
            int idSucursal = 1;

            var resultado = dao.ObtenerVentasEnEfectivoDelDia(idSucursal);

            Assert.IsTrue(resultado.EsExitoso, resultado.Error);
        }

        [TestMethod]
        public void RealizarPagoDulceria_DeberiaRegistrarVenta()
        {
            var venta = new Venta
            {
                idSocio = 1,
                total = 10
            };

            var productos = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 }
            };

            var resultado = dao.RealizarPagoDulceria(venta, productos, 0);
            Assert.IsTrue(resultado.EsExitoso, $"Error en venta de dulcería: {resultado.Error}");
        }

        [TestMethod]
        public void RealizarPagoBoletos_DeberiaRegistrarVenta()
        {
            var venta = new Venta
            {
                idSocio = 1,
                total = 15
            };

            var boletos = new List<int>
            {
                11
            };

            var resultado = dao.RealizarPagoBoletos(venta, boletos, 0);

            Assert.IsTrue(resultado.EsExitoso, $"Error en venta de boletos: {resultado.Error}");
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var entities = new CineVerEntities())
            {
                var ultimaVenta = entities.Venta.OrderByDescending(v => v.idVenta).FirstOrDefault();
                if (ultimaVenta != null)
                {
                    var boletos = entities.Boleto.Where(b => b.idVenta == ultimaVenta.idVenta).ToList();
                    foreach (var boleto in boletos)
                    {
                        boleto.Asiento.estado = "DISPONIBLE";
                        boleto.idVenta = null;
                    }

                    entities.Venta.Remove(ultimaVenta);
                    entities.SaveChanges();
                }
            }
        }

    }
}
