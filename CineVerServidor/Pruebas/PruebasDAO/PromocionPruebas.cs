using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PromocionPruebas
    {
        private PromocionDAO dao;
        private List<int> promocionesDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new PromocionDAO();
            promocionesDePrueba = new List<int>();
        }

        [TestMethod]
        public void ObtenerPromociones_Exito()
        {
            var resultado = dao.ObtenerPromociones(2);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsNotNull(resultado.Valor);
        }

        [TestMethod]
        public void ObtenerPromociones_Fallo_IdInvalido()
        {
            var resultado = dao.ObtenerPromociones(-1);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromociones_SinResultados()
        {
            var resultado = dao.ObtenerPromociones(99999);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesDulceria_Exito()
        {
            var resultado = dao.ObtenerPromocionesDulceria(2);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesDulceria_SinResultados()
        {
            var resultado = dao.ObtenerPromocionesDulceria(99999);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesDulceria_IdNegativo()
        {
            var resultado = dao.ObtenerPromocionesDulceria(-10);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesBoletos_Exito()
        {
            var resultado = dao.ObtenerPromocionesBoletos(2);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesBoletos_IdInvalido()
        {
            var resultado = dao.ObtenerPromocionesBoletos(-1);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPromocionesBoletos_SinResultados()
        {
            var resultado = dao.ObtenerPromocionesBoletos(99999);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void RegistrarPromocion_Exito()
        {
            var promo = CrearPromocionPrueba();
            promo.idSucursal = 2;
            var resultado = dao.RegistrarPromocion(promo);
            if (resultado.EsExitoso)
                promocionesDePrueba.Add(promo.idPromocion);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void RegistrarPromocion_DatosFaltantes()
        {
            var promo = new Promocion
            {
                idSucursal = 1,
                nombre = "Promo sin tipo"
            };
            var resultado = dao.RegistrarPromocion(promo);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ActualizarPromocion_Exito()
        {
            var promo = CrearPromocionPrueba();
            promo.idSucursal = 2;
            dao.RegistrarPromocion(promo);
            promocionesDePrueba.Add(promo.idPromocion);

            promo.nombre = "Promoción Actualizada";
            var resultado = dao.ActualizarPromocion(promo);

            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ActualizarPromocion_NoExiste()
        {
            var promo = new Promocion { idPromocion = -1 };
            var resultado = dao.ActualizarPromocion(promo);
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ActualizarPromocion_CamposInvalidos()
        {
            var promo = CrearPromocionPrueba();
            dao.RegistrarPromocion(promo);
            promocionesDePrueba.Add(promo.idPromocion);

            promo.tipo = null;
            var resultado = dao.ActualizarPromocion(promo);
            Assert.IsFalse(resultado.EsExitoso);
        }

        private Promocion CrearPromocionPrueba()
        {
            return new Promocion
            {
                idSucursal = 1,
                nombre = "Promo Test",
                tipo = "Dulcería",
                producto = "Palomitas",
                numeroProductosNecesarios = 2,
                numeroProductosPagar = 1,
                lunesAplica = true,
                martesAplica = true,
                miercolesAplica = true,
                juevesAplica = true,
                viernesAplica = true,
                sabadoAplica = false,
                domingoAplica = false
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in promocionesDePrueba)
                {
                    var promo = context.Promocion.FirstOrDefault(p => p.idPromocion == id);
                    if (promo != null)
                    {
                        context.Promocion.Remove(promo);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
