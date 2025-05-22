using CineVerEntidades;
using DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class SucursalPruebas
    {
        private const string NombrePrueba = "Sucursal de prueba";
        private SucursalDAO dao;

        [TestInitialize]
        public void Setup()
        {
            dao = new SucursalDAO();
        }

        [TestMethod]
        public void ObtenerSucursales_DeberiaDevolverLista()
        {
            var resultado = dao.ObtenerSucursales();
            Assert.IsTrue(resultado.EsExitoso, $"No se pudo obtener sucursales: {resultado.Error}");
        }

        [TestMethod]
        public void AgregarSucursal_DeberiaAgregarCorrectamente()
        {
            var sucursal = new Sucursal
            {
                nombre = "Sucursal de Otates",
                estado = "Veracruz",
                ciudad = "Xalapa",
                calle = "Av. Xalapa",
                numeroEnLaCalle = "123",
                codigoPostal = "91110",
                horaApertura = TimeSpan.FromHours(8),
                horaCierre = TimeSpan.FromHours(22),
                estadoSucursal = "Abierta"
            };

            var resultado = dao.AgregarSucursal(sucursal);
            Assert.IsTrue(resultado.EsExitoso, $"Falló al agregar sucursal: {resultado.Error}");
        }

        [TestMethod]
        public void ActualizarSucursal_DeberiaActualizarCorrectamente()
        {
            var sucursal = new Sucursal
            {
                nombre = "Sucursal temporal",
                estado = "CDMX",
                ciudad = "Xalapa",
                calle = "Calle bonita",
                numeroEnLaCalle = "42",
                codigoPostal = "80085",
                horaApertura = TimeSpan.FromHours(9),
                horaCierre = TimeSpan.FromHours(21),
                estadoSucursal = "Abierta"
            };

            var addResult = dao.AgregarSucursal(sucursal);

            int id = 0;
            using (var context = new CineVerEntities())
            {
                id = context.Sucursal.OrderByDescending(s => s.idSucursal).First().idSucursal;
            }

            sucursal.nombre = "Sucursal actualizada";
            var updateResult = dao.ActualizarSucursal(id, sucursal);
            Assert.IsTrue(updateResult.EsExitoso, $"No se pudo actualizar la sucursal: {updateResult.Error}");
        }

        [TestMethod]
        public void CerrarSucursal_DeberiaCerrarCorrectamente()
        {
            var sucursal = new Sucursal
            {
                nombre = "Sucursal a cerrar",
                estado = "Puebla",
                ciudad = "Pueblito Otates",
                calle = "Ernesto",
                numeroEnLaCalle = "404",
                codigoPostal = "12345",
                horaApertura = TimeSpan.FromHours(10),
                horaCierre = TimeSpan.FromHours(22),
                estadoSucursal = "Abierta"
            };

            var addResult = dao.AgregarSucursal(sucursal);

            int id = 0;
            using (var context = new CineVerEntities())
            {
                id = context.Sucursal.OrderByDescending(s => s.idSucursal).First().idSucursal;
            }

            var closeResult = dao.CerrarSucursal(id);
            Assert.IsTrue(closeResult.EsExitoso, $"No se pudo cerrar la sucursal: {closeResult.Error}");
        }

        [TestMethod]
        public void ActualizarSucursal_IdInvalido_DeberiaFallar()
        {
            var sucursal = new Sucursal
            {
                nombre = NombrePrueba + " Error Actualizar",
                estado = "Estado",
                ciudad = "Ciudad",
                calle = "Calle",
                numeroEnLaCalle = "1",
                codigoPostal = "00000",
                horaApertura = TimeSpan.FromHours(8),
                horaCierre = TimeSpan.FromHours(20),
                estadoSucursal = "Abierta"
            };

            var resultado = dao.ActualizarSucursal(-1, sucursal);
            Assert.IsFalse(resultado.EsExitoso, "Debería fallar al actualizar una sucursal con ID inválido");
        }

        [TestMethod]
        public void CerrarSucursal_IdInvalido_DeberiaFallar()
        {
            var resultado = dao.CerrarSucursal(-9999);
            Assert.IsFalse(resultado.EsExitoso, "Debería fallar al cerrar una sucursal con ID inválido");
        }

        [TestMethod]
        public void ObtenerSucursales_SinSucursalesAbiertas_DeberiaFallar()
        {
            using (var context = new CineVerEntities())
            {
                var todas = context.Sucursal.ToList();
                foreach (var suc in todas)
                {
                    suc.estadoSucursal = "Cerrada";
                }
                context.SaveChanges();
            }

            var resultado = dao.ObtenerSucursales();
            Assert.IsFalse(resultado.EsExitoso, "Debería fallar si no hay sucursales abiertas");
        }

        [TestMethod]
        public void ObtenerAsientosPorFila_SalaConFilas_DeberiaRegresarFilas()
        {
            int idSala = 1;
            var resultado = dao.ObtenerAsientosPorFila(idSala);

            Assert.IsTrue(resultado.EsExitoso, "Se esperaban filas registradas");
        }


        [TestMethod]
        public void ObtenerAsientosPorFila_SalaSinFilas_DeberiaFallar()
        {
            int idSalaSinFilas = 9999;

            var resultado = dao.ObtenerAsientosPorFila(idSalaSinFilas);

            Assert.IsFalse(resultado.EsExitoso, "Debería fallar si no hay filas");
        }


        [TestCleanup]
        public void Cleanup()
        {
            using (var context = new CineVerEntities())
            {
                var sucursales = context.Sucursal
                    .Where(s => s.nombre.StartsWith(NombrePrueba))
                    .ToList();

                if (sucursales.Any())
                {
                    context.Sucursal.RemoveRange(sucursales);
                    context.SaveChanges();
                }
            }
        }
    }
}
