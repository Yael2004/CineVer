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
        public void RealizarPagoDulceria_DeberiaRegistrarVenta()
        {
            var venta = new Venta
            {
                idSocio = 1,
                total = 100
            };

            var productos = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 }
            };

            var resultado = dao.RealizarPagoDulceria(venta, productos);
            Assert.IsTrue(resultado.EsExitoso, $"Error en venta de dulcería: {resultado.Error}");
        }

        [TestMethod]
        public void RealizarPagoBoletos_DeberiaRegistrarVenta()
        {
            var venta = new Venta
            {
                idSocio = 1,
                total = 150
            };

            var boletos = new List<int>
            {
                10 
            };

            var resultado = dao.RealizarPagoBoletos(venta, boletos);

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
