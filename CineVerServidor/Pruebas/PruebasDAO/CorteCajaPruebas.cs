using CineVerEntidades;
using DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class CorteCajaPruebas
    {
        private CorteCajaDAO dao;
        private int idSucursalPrueba = 1;
        private DateTime fechaPrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new CorteCajaDAO();
            fechaPrueba = DateTime.Now.Date;

            using (var entities = new CineVerEntities())
            {
                var anteriores = entities.CorteCaja
                    .Where(c => c.fechaCorte == fechaPrueba && c.idSucursal == idSucursalPrueba)
                    .ToList();
                entities.CorteCaja.RemoveRange(anteriores);
                entities.SaveChanges();
            }
        }

        [TestMethod]
        public void GuardarCorteCaja_DeberiaGuardarCorrectamente()
        {
            var corte = new CorteCaja
            {
                idSucursal = idSucursalPrueba,
                fechaCorte = fechaPrueba,
                inicioDia = 500,
                ventaTotal = 1500,
                diferenciaEfectivo = 0,
                efectivoCaja = 500,
                efectivoEsperado = 500,
                ganancias = 1000,
                gastos = 0,
                idCorteCaja = 500
            };

            var resultado = dao.GuardarCorteCaja(corte);
            Assert.IsTrue(resultado.EsExitoso, $"Falló al guardar el corte de caja: {resultado.Error}");
        }

        [TestMethod]
        public void GuardarCorteCaja_SiFechaYaExiste_DeberiaFallar()
        {
            var corteExistente = new CorteCaja
            {
                idSucursal = idSucursalPrueba,
                fechaCorte = fechaPrueba,
                inicioDia = 500,
                ventaTotal = 1000,
                diferenciaEfectivo = 0,
                efectivoCaja = 500,
                efectivoEsperado = 500,
                ganancias = 500,
                gastos = 0,
                idCorteCaja = 600
            };

            dao.GuardarCorteCaja(corteExistente);

            var corteDuplicado = new CorteCaja
            {
                idSucursal = idSucursalPrueba,
                fechaCorte = fechaPrueba,
                inicioDia = 700,
                ventaTotal = 2000,
                diferenciaEfectivo = 0,
                efectivoCaja = 700,
                efectivoEsperado = 700,
                ganancias = 1300,
                gastos = 0,
                idCorteCaja = 601
            };

            var resultado = dao.GuardarCorteCaja(corteDuplicado);
            Assert.IsFalse(resultado.EsExitoso, "El guardado debería fallar por corte duplicado");
        }

        [TestMethod]
        public void ObtenerMontoInicioDia_DeberiaObtenerMontoCorrecto()
        {
            DateTime fechaAyer = DateTime.Now.AddDays(-1).Date;
            using (var entities = new CineVerEntities())
            {
                if (!entities.CorteCaja.Any(c => c.fechaCorte == fechaAyer && c.idSucursal == idSucursalPrueba))
                {
                    entities.CorteCaja.Add(new CorteCaja
                    {
                        idSucursal = idSucursalPrueba,
                        fechaCorte = fechaAyer,
                        inicioDia = 777,
                        ventaTotal = 1234
                    });
                    entities.SaveChanges();
                }
            }

            var resultado = dao.ObtenerMontoInicioDia(idSucursalPrueba);
            Assert.AreEqual(777, resultado.Valor);
        }

        [TestMethod]
        public void ObtenerMontoInicioDia_SiNoExiste_DeberiaFallar()
        {
            DateTime fechaAyer = DateTime.Now.AddDays(-1).Date;
            using (var entities = new CineVerEntities())
            {
                var borrar = entities.CorteCaja
                    .Where(c => c.fechaCorte == fechaAyer && c.idSucursal == 999)
                    .ToList();
                entities.CorteCaja.RemoveRange(borrar);
                entities.SaveChanges();
            }

            var resultado = dao.ObtenerMontoInicioDia(999);
            Assert.AreEqual("No se encontró el corte de caja para la fecha especificada.", resultado.Error);
        }

        [TestCleanup]
        public void Cleanup()
        {
            DateTime fechaHoy = DateTime.Now.Date;
            DateTime fechaAyer = fechaHoy.AddDays(-1);

            using (var entities = new CineVerEntities())
            {
                var cortes = entities.CorteCaja
                    .Where(c => (c.fechaCorte == fechaHoy || c.fechaCorte == fechaAyer) &&
                                (c.idSucursal == idSucursalPrueba || c.idSucursal == 999))
                    .ToList();

                entities.CorteCaja.RemoveRange(cortes);
                entities.SaveChanges();
            }
        }

    }
}
