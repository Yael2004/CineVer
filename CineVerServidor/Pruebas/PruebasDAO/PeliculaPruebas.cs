using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAO;
using CineVerEntidades;
using System.Collections.Generic;
using System.Linq;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class PeliculaDAOPruebas
    {
        private PelículaDAO dao;
        private List<int> peliculasDePrueba;

        [TestInitialize]
        public void Setup()
        {
            dao = new PelículaDAO();
            peliculasDePrueba = new List<int>();
        }

        [TestMethod]
        public void AgregarPelicula_Exito()
        {
            var pelicula = CrearPeliculaPrueba();
            var resultado = dao.AgregarPelicula(pelicula);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Pelicula agregada exitosamente", resultado.Valor);

            var id = dao.ObtenerIdPelicula(pelicula.nombre, pelicula.director).Valor;
            peliculasDePrueba.Add(id);
        }

        [TestMethod]
        public void AgregarPelicula_Duplicada()
        {
            var pelicula = CrearPeliculaPrueba();
            dao.AgregarPelicula(pelicula);

            var resultado = dao.AgregarPelicula(pelicula);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("La película ya ha sido agregada previamente", resultado.Error);

            var id = dao.ObtenerIdPelicula(pelicula.nombre, pelicula.director).Valor;
            peliculasDePrueba.Add(id);
        }

        [TestMethod]
        public void ObtenerPeliculasPorSucursal_Exito()
        {
            var resultado = dao.ObtenerPeliculasPorSucursal(1);
            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ObtenerPeliculasPorSucursal_IdInvalido()
        {
            var resultado = dao.ObtenerPeliculasPorSucursal(-1);
            Assert.IsTrue(resultado.EsExitoso); // Puede regresar lista vacía
            Assert.AreEqual(0, resultado.Valor.Count);
        }

        [TestMethod]
        public void EliminarPelicula_Exito()
        {
            var pelicula = CrearPeliculaPrueba();
            dao.AgregarPelicula(pelicula);
            var id = dao.ObtenerIdPelicula(pelicula.nombre, pelicula.director).Valor;
            peliculasDePrueba.Add(id);

            var resultado = dao.EliminarPelicula(pelicula);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Pelicula eliminada exitosamente", resultado.Valor);

            peliculasDePrueba.Remove(id); // Ya fue eliminada
        }

        [TestMethod]
        public void ObtenerPeliculaPorID_NoExiste()
        {
            var resultado = dao.ObtenerPeliculaPorID(-999);
            Assert.IsFalse(resultado.EsExitoso);
            Assert.AreEqual("Pelicula no encontrada", resultado.Error);
        }

        private Película CrearPeliculaPrueba()
        {
            return new Película
            {
                nombre = "Pelicula Test",
                director = "Director Test",
                genero = "Drama",
                duracion = new System.TimeSpan(1, 30, 0),
                sinopsis = "Una película de prueba",
                idSucursal = 1,
                poster = new byte[] { 0x00 } // Simula una imagen
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new CineVerEntities())
            {
                foreach (var id in peliculasDePrueba)
                {
                    var pelicula = context.Película.FirstOrDefault(p => p.idPelicula == id);
                    if (pelicula != null)
                    {
                        context.Película.Remove(pelicula);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
