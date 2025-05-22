using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class FuncionPruebas
    {
        private FuncionDAO dao;
        private List<int> funcionesDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new FuncionDAO();
            funcionesDePrueba = new List<int>();
        }

        [TestMethod]
        public void AgregarFuncion_Exito()
        {
            var funcion = CrearFuncionPrueba();
            var resultado = dao.AgregarFuncion(funcion);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Función agregada con éxito", resultado.Valor);

            using (var context = new CineVerEntities())
            {
                var id = context.Función
                    .Where(f => f.idSala == funcion.idSala &&
                                f.idPelicula == funcion.idPelicula &&
                                f.fecha == funcion.fecha &&
                                f.horaInicio == funcion.horaInicio)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();
                funcionesDePrueba.Add(id);
            }
        }

        [TestMethod]
        public void ObtenerFuncionesPorPeliculaYFecha_Exito()
        {
            var funcion = CrearFuncionPrueba();
            dao.AgregarFuncion(funcion);

            int idFuncion;
            using (var context = new CineVerEntities())
            {
                idFuncion = context.Función
                    .Where(f => f.idSala == funcion.idSala && f.idPelicula == funcion.idPelicula && f.fecha == funcion.fecha)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();
                funcionesDePrueba.Add(idFuncion);
            }

            var resultado = dao.ObtenerFuncionesPorPeliculaYFecha(funcion.idPelicula.Value, funcion.fecha.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(f => f.idFuncion == idFuncion));
        }

        [TestMethod]
        public void ObtenerFuncionesPorFecha_Exito()
        {
            var funcion = CrearFuncionPrueba();
            dao.AgregarFuncion(funcion);

            int idFuncion;
            using (var context = new CineVerEntities())
            {
                idFuncion = context.Función
                    .Where(f => f.fecha == funcion.fecha)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();
                funcionesDePrueba.Add(idFuncion);
            }

            var resultado = dao.ObtenerFUncionesPorFecha(funcion.fecha.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(f => f.idFuncion == idFuncion));
        }

        [TestMethod]
        public void ObtenerFuncionesPorSalaYFecha_Exito()
        {
            var funcion = CrearFuncionPrueba();
            dao.AgregarFuncion(funcion);

            int idFuncion;
            using (var context = new CineVerEntities())
            {
                idFuncion = context.Función
                    .Where(f => f.idSala == funcion.idSala && f.fecha == funcion.fecha)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();
                funcionesDePrueba.Add(idFuncion);
            }

            var resultado = dao.ObtenerFuncionesPorSalaYFecha(funcion.idSala.Value, funcion.fecha.Value);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any(f => f.idFuncion == idFuncion));
        }

        [TestMethod]
        public void EditarFuncion_Exito()
        {
            var funcionOriginal = CrearFuncionPrueba();
            dao.AgregarFuncion(funcionOriginal);

            using (var context = new CineVerEntities())
            {
                funcionOriginal.idFuncion = context.Función
                    .Where(f => f.idSala == funcionOriginal.idSala &&
                                f.idPelicula == funcionOriginal.idPelicula &&
                                f.fecha == funcionOriginal.fecha &&
                                f.horaInicio == funcionOriginal.horaInicio)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();

                funcionesDePrueba.Add(funcionOriginal.idFuncion);
            }

            var funcionEditada = new Función
            {
                horaInicio = TimeSpan.FromHours(18),
                idSala = funcionOriginal.idSala,
                idPelicula = funcionOriginal.idPelicula,
                precioBoleto = 90
            };

            var resultado = dao.EditarFuncion(funcionOriginal, funcionEditada);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Función editada con éxito", resultado.Valor);
        }

        [TestMethod]
        public void EliminarFuncion_Exito()
        {
            var funcion = CrearFuncionPrueba();
            dao.AgregarFuncion(funcion);

            using (var context = new CineVerEntities())
            {
                funcion.idFuncion = context.Función
                    .Where(f => f.idSala == funcion.idSala &&
                                f.idPelicula == funcion.idPelicula &&
                                f.fecha == funcion.fecha &&
                                f.horaInicio == funcion.horaInicio)
                    .Select(f => f.idFuncion)
                    .FirstOrDefault();
                funcionesDePrueba.Add(funcion.idFuncion);
            }

            var resultado = dao.EliminarFuncion(funcion);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Función eliminada con éxito", resultado.Valor);

            funcionesDePrueba.Remove(funcion.idFuncion); // Ya fue eliminada
        }

        private Función CrearFuncionPrueba()
        {
            return new Función
            {
                idSala = 1, // Asegúrate de tener esta sala en tu base de datos
                idPelicula = 1, // Asegúrate de tener esta película en tu base de datos
                fecha = DateTime.Today,
                horaInicio = TimeSpan.FromHours(14),
                precioBoleto = 85
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in funcionesDePrueba)
                {
                    var funcion = context.Función.FirstOrDefault(f => f.idFuncion == id);
                    if (funcion != null)
                    {
                        context.Función.Remove(funcion);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
