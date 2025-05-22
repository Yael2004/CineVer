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
        [TestMethod]
        public void EditarPelicula_Exito()
        {
            var peliculaOriginal = CrearPeliculaPrueba();
            dao.AgregarPelicula(peliculaOriginal);

            var id = dao.ObtenerIdPelicula(peliculaOriginal.nombre, peliculaOriginal.director).Valor;
            peliculasDePrueba.Add(id);

            var peliculaEditada = new Película
            {
                nombre = "Pelicula Test Editada",
                director = "Director Test Editado",
                genero = "Acción",
                duracion = new System.TimeSpan(2, 0, 0),
                sinopsis = "Sinopsis actualizada",
                idSucursal = 1,
                poster = new byte[] { 0x01 }
            };

            var resultado = dao.EditarPelicula(peliculaEditada, peliculaOriginal);

            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual("Pelicula editada exitosamente", resultado.Valor);

            var peliculaBD = dao.ObtenerPeliculaPorID(id).Valor;
            Assert.AreEqual("Pelicula Test Editada", peliculaBD.nombre);
            Assert.AreEqual("Director Test Editado", peliculaBD.director);
        }

        [TestMethod]
        public void ObtenerPeliculasPorNombre_Exito()
        {
            var pelicula = CrearPeliculaPrueba();
            dao.AgregarPelicula(pelicula);

            var id = dao.ObtenerIdPelicula(pelicula.nombre, pelicula.director).Valor;
            peliculasDePrueba.Add(id);

            var resultado = dao.ObtenerPeliculasPorNombre(1, "Pelicula Test");
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor.Any());
            Assert.IsTrue(resultado.Valor.Any(p => p.nombre.Contains("Pelicula Test")));
        }

        [TestMethod]
        public void ObtenerPeliculasPorNombre_SinCoincidencias()
        {
            var resultado = dao.ObtenerPeliculasPorNombre(1, "NombreQueNoExiste");
            Assert.IsTrue(resultado.EsExitoso);
            Assert.AreEqual(0, resultado.Valor.Count);
        }

        [TestMethod]
        public void ExistePelicula_Existe()
        {
            var pelicula = CrearPeliculaPrueba();
            dao.AgregarPelicula(pelicula);

            var id = dao.ObtenerIdPelicula(pelicula.nombre, pelicula.director).Valor;
            peliculasDePrueba.Add(id);

            var resultado = dao.ExistePelicula(pelicula.nombre, pelicula.director);
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsTrue(resultado.Valor);
        }

        [TestMethod]
        public void ExistePelicula_NoExiste()
        {
            var resultado = dao.ExistePelicula("No existe", "Tampoco");
            Assert.IsTrue(resultado.EsExitoso);
            Assert.IsFalse(resultado.Valor);
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
