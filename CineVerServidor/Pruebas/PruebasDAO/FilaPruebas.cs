using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class FilaPruebas
    {
        private FilaDAO dao;
        private List<int> filasDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new FilaDAO();
            filasDePrueba = new List<int>();
        }

        [TestMethod]
        public void AgregarFila_Exito()
        {
            var fila = CrearFilaPrueba();
            var resultado = dao.AgregarFila(fila);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Fila agregada exitosamente", resultado.Valor);

            var id = dao.ObtenerIdFila(fila.idSala.Value, fila.númeroFila.Value).Valor;
            filasDePrueba.Add(id);
        }

        [TestMethod]
        public void ObtenerIdFila_Exito()
        {
            var fila = CrearFilaPrueba();
            dao.AgregarFila(fila);
            var id = dao.ObtenerIdFila(fila.idSala.Value, fila.númeroFila.Value).Valor;
            filasDePrueba.Add(id);

            var resultado = dao.ObtenerIdFila(fila.idSala.Value, fila.númeroFila.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual(id, resultado.Valor);
        }

        [TestMethod]
        public void ObtenerIdFila_NoExiste()
        {
            var resultado = dao.ObtenerIdFila(-999, -999);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("Fila no encontrada", resultado.Error);
        }

        [TestMethod]
        public void EditarFila_Exito()
        {
            var filaOriginal = CrearFilaPrueba();
            dao.AgregarFila(filaOriginal);
            var id = dao.ObtenerIdFila(filaOriginal.idSala.Value, filaOriginal.númeroFila.Value).Valor;
            filasDePrueba.Add(id);
            filaOriginal.idFila = id;

            var filaEditada = new Fila
            {
                númeroFila = filaOriginal.númeroFila + 1,
                numeroAsientos = 20
            };

            var resultado = dao.EditarFila(filaEditada, filaOriginal);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Fila editada exitosamente", resultado.Valor);
        }

        [TestMethod]
        public void EliminarFila_Exito()
        {
            var fila = CrearFilaPrueba();
            dao.AgregarFila(fila);
            var id = dao.ObtenerIdFila(fila.idSala.Value, fila.númeroFila.Value).Valor;
            fila.idFila = id;
            filasDePrueba.Add(id);

            var resultado = dao.EliminarFila(fila);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Fila eliminada exitosamente", resultado.Valor);

            filasDePrueba.Remove(id); // Ya fue eliminada
        }

        [TestMethod]
        public void ObtenerFilasDeSala_Exito()
        {
            var fila = CrearFilaPrueba();
            dao.AgregarFila(fila);
            var id = dao.ObtenerIdFila(fila.idSala.Value, fila.númeroFila.Value).Valor;
            filasDePrueba.Add(id);

            var resultado = dao.ObtenerFilasDeSala(fila.idSala.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(f => f.idFila == id));
        }

        private Fila CrearFilaPrueba()
        {
            return new Fila
            {
                númeroFila = 1,
                numeroAsientos = 10,
                idSala = 1 // Asegúrate de que esta sala exista en la base de datos
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in filasDePrueba)
                {
                    var fila = context.Fila.FirstOrDefault(f => f.idFila == id);
                    if (fila != null)
                    {
                        context.Fila.Remove(fila);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
