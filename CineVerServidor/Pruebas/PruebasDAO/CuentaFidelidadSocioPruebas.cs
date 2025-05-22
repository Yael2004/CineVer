using CineVerEntidades;
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
    public class CuentaFidelidadPruebas
    {
        private CuentaFidelidadDAO cuentaDAO;
        private SocioDAO socioDAO;

        private int idSocioDePrueba = 99999;
        private int idCuentaInsertada;
        private int idSocioInsertado;

        [TestInitialize]
        public void Setup()
        {
            cuentaDAO = new CuentaFidelidadDAO();
            socioDAO = new SocioDAO();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (idCuentaInsertada != 0)
            {
                cuentaDAO.InhabilitarCuentaFidelidad(idCuentaInsertada);
            }

            if (idSocioInsertado != 0)
            {
                socioDAO.InhabilitarCuentaSocio(idSocioInsertado);
            }
        }

        [TestMethod]
        public void ObtenerCuentaFidelidadPorIdSocio_Exito()
        {
            var resultado = cuentaDAO.ObtenerCuentaFidelidadPorIdSocio(1);

            Assert.IsTrue(resultado.EsExitoso, "No se obtuvo la cuenta por ID de socio: " + resultado.Error);
        }

        [TestMethod]
        public void ObtenerCuentaFidelidadPorIdSocio_NoEncontrado()
        {
            var resultado = cuentaDAO.ObtenerCuentaFidelidadPorIdSocio(-1);
            Assert.IsFalse(resultado.EsExitoso, "Se encontró una cuenta inexistente");
        }

        [TestMethod]
        public void ModificarCuentaFidelidad_Exito()
        {
            var cuenta = cuentaDAO.ObtenerCuentaFidelidadPorIdSocio(1).Valor;
            cuenta.puntos = 20;
            var resultado = cuentaDAO.ModificarCuentaFidelidad(cuenta);

            Assert.IsTrue(resultado.EsExitoso);
        }

        [TestMethod]
        public void ModificarCuentaFidelidad_Fallo_NoExiste()
        {
            var cuenta = new CuentaFidelidad()
            {
                idCuenta = -1,
                idSocio = 123,
                puntos = 10
            };
            var resultado = cuentaDAO.ModificarCuentaFidelidad(cuenta);
            Assert.IsFalse(resultado.EsExitoso, "Se modificó una cuenta inexistente");
        }

        [TestMethod]
        public void InhabilitarCuentaFidelidad_Exito()
        {
            var resultado = cuentaDAO.InhabilitarCuentaFidelidad(3);

            Assert.IsTrue(resultado.EsExitoso, "No se pudo inhabilitar la cuenta: " + resultado.Error);
        }

        [TestMethod]
        public void InhabilitarCuentaFidelidad_Fallo_NoExiste()
        {
            var resultado = cuentaDAO.InhabilitarCuentaFidelidad(-1);
            Assert.IsFalse(resultado.EsExitoso, "Se inhabilitó una cuenta inexistente");
        }

        [TestMethod]
        public void RegistrarSocio_Exito()
        {
            var socio = CrearSocio();
            var resultado = socioDAO.RegistrarSocio(socio);

            Assert.IsTrue(resultado.EsExitoso, "No se pudo registrar el socio: " + resultado.Error);
        }

        [TestMethod]
        public void ObtenerSocioPorId_Exito()
        {
            var resultado = socioDAO.BuscarSocioPorFolio("SCNVX12345");

            Assert.IsTrue(resultado.EsExitoso, "No se obtuvo el socio: " + resultado.Error);
        }

        [TestMethod]
        public void ObtenerSocioPorId_NoExiste()
        {
            var resultado = socioDAO.BuscarSocioPorFolio("kkkkkk");
            Assert.IsFalse(resultado.EsExitoso, "Se obtuvo un socio inexistente");
        }

        private CuentaFidelidad CrearCuentaFidelidad()
        {
            return new CuentaFidelidad()
            {
                idSocio = idSocioDePrueba,
                puntos = 100
            };
        }

        private Socio CrearSocio()
        {
            return new Socio
            {
                nombre = "Carlos",
                apellido = "Ramírez",
                correoElectronico = "carlos@gmail.com",
                sexo = "M",
                numeroTelefono = "5551234567",
                calle = "Av xalapa",
                numeroCasa = "123",
                codigoPostal = "45678",
                fechaNacimiento = new DateTime(1990, 5, 15),
                folio = "F123456",
                afiliado = true
            };
        }
    }
}
