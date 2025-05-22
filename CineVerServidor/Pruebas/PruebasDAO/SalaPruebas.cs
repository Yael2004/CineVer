using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class SalaPruebas
    {
        private SalaDAO dao;
        private List<int> salasDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new SalaDAO();
            salasDePrueba = new List<int>();
        }

        [TestMethod]
        public void AgregarSala_Exito()
        {
            var sala = CrearSalaPrueba();
            var resultado = dao.AgregarSala(sala);
            Assert.IsTrue(resultado.EsExitoso);
            salasDePrueba.Add(sala.idSala);
        }

        [TestMethod]
        public void AgregarSala_Duplicada()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala); // Agregamos una vez
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.AgregarSala(sala); // Intentamos agregarla de nuevo
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("Ya se agregó una sala con ese nombre", resultado.Error);
        }

        [TestMethod]
        public void ObtenerSalasPorSucursal_Exito()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.ObtenerSalasPorSucursal(sala.idSucursal.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(s => s.nombre == sala.nombre));
        }

        [TestMethod]
        public void ObtenerSalaPorId_Exito()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.ObtenerSalaPorId(sala.idSala);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual(sala.nombre, resultado.Valor.nombre);
        }

        [TestMethod]
        public void ObtenerIDSala_Exito()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.ObtenerIDSala(sala.idSucursal, sala.nombre);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual(sala.idSala, resultado.Valor);
        }

        [TestMethod]
        public void VerificarExistenciaSala_True()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.VerificarExistenciaSala(sala.idSucursal, sala.nombre);
            Assert.IsTrue(resultado.Valor);
        }

        [TestMethod]
        public void VerificarExistenciaSala_False()
        {
            var resultado = dao.VerificarExistenciaSala(99999, "SalaInexistente");
            Assert.IsFalse(resultado.Valor);
        }

        [TestMethod]
        public void EditarSala_Exito()
        {
            var original = CrearSalaPrueba();
            dao.AgregarSala(original);
            salasDePrueba.Add(original.idSala);

            var editada = new Sala
            {
                idSucursal = original.idSucursal,
                nombre = "Sala Editada",
                numeroFilas = 15,
                estadoSala = "Activa",
                descripcion = "Modificada"
            };

            var resultado = dao.EditarSala(editada, original);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void EliminarSala_Exito()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);

            var resultado = dao.EliminarSala(sala);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerSalasPorSucursalYNombre_Exito()
        {
            var sala = CrearSalaPrueba();
            dao.AgregarSala(sala);
            salasDePrueba.Add(sala.idSala);

            var resultado = dao.ObtenerSalasPorSucursalYNombre(sala.idSucursal.Value, "Test");
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(s => s.nombre.Contains("Test")));
        }

        private Sala CrearSalaPrueba()
        {
            return new Sala
            {
                nombre = "Sala Test " + System.Guid.NewGuid().ToString().Substring(0, 4),
                idSucursal = 1,
                estadoSala = "Activa",
                numeroFilas = 10,
                descripcion = "Sala de prueba"
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in salasDePrueba)
                {
                    var sala = context.Sala.FirstOrDefault(s => s.idSala == id);
                    if (sala != null)
                    {
                        context.Sala.Remove(sala);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
