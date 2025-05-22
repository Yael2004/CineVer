using CineVerEntidades;
using CineVerServicios.DTO;
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
    public class GastoPruebas
    {
        [TestMethod]
        public void RegistrarGasto_Exito()
        {
            GastoDAO gastoDAO = new GastoDAO();
            Gasto gasto = new Gasto
            {
                monto = 100,
                motivo = "Motivo",
                fecha = DateTime.Now,
                idSucursal = 2,
                idEmpleado = 3
            };

            var resultado = gastoDAO.RegistrarGasto(gasto);

            Assert.IsTrue(resultado.EsExitoso, "Se esperaba un registro exitoso del gasto.");
        }

        [TestMethod]
        public void RegistrarGasto_Fallo()
        {
            GastoDAO gastoDAO = new GastoDAO();
            Gasto gasto = new Gasto
            {
                monto = 100,
                motivo = "Motivo",
                fecha = DateTime.Now,
                idSucursal = 2,
                idEmpleado = 3
            };

            var resultado = gastoDAO.RegistrarGasto(gasto);

            Assert.IsFalse(!resultado.EsExitoso, "Se esperaba un registro exitoso del gasto.");
        }

        [TestMethod]
        public void ObtenerGastosDelDia_Exito()
        {
            GastoDAO gastoDAO = new GastoDAO();
            DateTime fechaHoy = DateTime.Now;
            int idSucursal = 2;

            var resultado = gastoDAO.ObtenerGastosDelDia(fechaHoy, idSucursal);

            Assert.IsTrue(resultado.EsExitoso, "Se esperaba obtener gastos del día.");
        }

        [TestMethod]
        public void ObtenerGastosDelDia_Fallo()
        {
            GastoDAO gastoDAO = new GastoDAO();
            DateTime fechaFutura = DateTime.Now.AddYears(10);
            int idSucursal = 1;

            var resultado = gastoDAO.ObtenerGastosDelDia(fechaFutura, idSucursal);

            Assert.IsFalse(resultado.EsExitoso, "Se esperaba un fallo al no encontrar gastos.");
        }
    }
}
