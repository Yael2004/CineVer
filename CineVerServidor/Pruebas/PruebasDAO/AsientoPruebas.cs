using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class AsientoPruebas
    {
        private AsientoDAO dao;
        private List<int> asientosDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new AsientoDAO();
            asientosDePrueba = new List<int>();
        }

        [TestMethod]
        public void AgregarAsiento_Exito()
        {
            var asiento = CrearAsientoPrueba();
            var resultado = dao.AgregarAsiento(asiento);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Asiento agregado exitosamente", resultado.Valor);

            var id = dao.ObtenerIdAsiento(asiento.idFila.Value, asiento.letraColumna).Valor;
            asientosDePrueba.Add(id);
        }

        [TestMethod]
        public void ObtenerAsientosDeFila_Exito()
        {
            var asiento = CrearAsientoPrueba();
            dao.AgregarAsiento(asiento);
            var id = dao.ObtenerIdAsiento(asiento.idFila.Value, asiento.letraColumna).Valor;
            asientosDePrueba.Add(id);

            var resultado = dao.ObtenerAsientosDeFila(asiento.idFila.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(a => a.idAsiento == id));
        }

        [TestMethod]
        public void ObtenerIdAsiento_Exito()
        {
            var asiento = CrearAsientoPrueba();
            dao.AgregarAsiento(asiento);
            var id = dao.ObtenerIdAsiento(asiento.idFila.Value, asiento.letraColumna).Valor;
            asientosDePrueba.Add(id);

            var resultado = dao.ObtenerIdAsiento(asiento.idFila.Value, asiento.letraColumna);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual(id, resultado.Valor);
        }

        [TestMethod]
        public void ObtenerIdAsiento_NoExiste()
        {
            var resultado = dao.ObtenerIdAsiento(-999, "Z");
            Assert.IsFalse(resultado.EsExitoso); 
        }

        [TestMethod]
        public void EditarAsiento_Exito()
        {
            var asientoOriginal = CrearAsientoPrueba();
            dao.AgregarAsiento(asientoOriginal);
            var id = dao.ObtenerIdAsiento(asientoOriginal.idFila.Value, asientoOriginal.letraColumna).Valor;
            asientosDePrueba.Add(id);
            asientoOriginal.idAsiento = id;

            var asientoEditado = new Asiento
            {
                letraColumna = "B",
                estado = "Ocupado"
            };

            var resultado = dao.EditarAsiento(asientoEditado, asientoOriginal);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Asiento Editado exitosamente", resultado.Valor);
        }

        [TestMethod]
        public void EliminarAsiento_Exito()
        {
            var asiento = CrearAsientoPrueba();
            dao.AgregarAsiento(asiento);
            var id = dao.ObtenerIdAsiento(asiento.idFila.Value, asiento.letraColumna).Valor;
            asiento.idAsiento = id;
            asientosDePrueba.Add(id);

            var resultado = dao.EliminarAsiento(asiento);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Asiento eliminado exitosamente", resultado.Valor);

            asientosDePrueba.Remove(id); 
        }

        private Asiento CrearAsientoPrueba()
        {
            return new Asiento
            {
                letraColumna = "A",
                estado = "Disponible",
                idFila = 1 
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in asientosDePrueba)
                {
                    var asiento = context.Asiento.FirstOrDefault(a => a.idAsiento == id);
                    if (asiento != null)
                    {
                        context.Asiento.Remove(asiento);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
