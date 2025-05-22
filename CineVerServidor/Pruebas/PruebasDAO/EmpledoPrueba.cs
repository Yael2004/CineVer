using CineVerEntidades;
using DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pruebas.PruebasDAO
{
    [TestClass]
    public class EmpledoPrueba
    {
        private EmpleadoDAO dao;
        private string matriculaDePrueba = "EMP2025001";

        [TestInitialize]
        public void Setup()
        {
            dao = new EmpleadoDAO();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var empleado = dao.BuscarEmpleadoPorMatricula(matriculaDePrueba);
            if (empleado.EsExitoso)
            {
                dao.InhabilitarEmpleado(empleado.Valor.idEmpleado);
            }
        }

        [TestMethod]
        public void RegistrarEmpleado_Exito()
        {
            var empleado = CrearEmpleado();
            var resultado = dao.RegistrarEmpleado(empleado);
            Assert.IsTrue(resultado.EsExitoso, "Error al registrar el empleado: " + resultado.Error);
        }

        [TestMethod]
        public void BuscarEmpleadoPorMatricula_DeberiaEncontrarlo()
        {
            dao.RegistrarEmpleado(CrearEmpleado());
            var resultado = dao.BuscarEmpleadoPorMatricula(matriculaDePrueba);
            Assert.IsTrue(resultado.EsExitoso, "Empleado no encontrado: " + resultado.Error);
        }

        [TestMethod]
        public void ObtenerEmpleados_DeberiaDevolverLista()
        {
            dao.RegistrarEmpleado(CrearEmpleado());
            var resultado = dao.ObtenerEmpleados();
            Assert.IsTrue(resultado.EsExitoso, "No se pudieron obtener los empleados: " + resultado.Error);
            Assert.IsTrue(resultado.Valor.Any(e => e.matriculaEmpleado == matriculaDePrueba));
        }

        [TestMethod]
        public void ModificarEmpleado_DeberiaActualizarDatos()
        {
            dao.RegistrarEmpleado(CrearEmpleado());
            var empleado = dao.BuscarEmpleadoPorMatricula(matriculaDePrueba).Valor;
            empleado.numeroTelefono = "5559876543";
            var resultado = dao.ModificarEmpleado(empleado);
            Assert.IsTrue(resultado.EsExitoso, "No se pudo modificar el empleado: " + resultado.Error);
        }

        [TestMethod]
        public void ExisteEmpleado_DeberiaRetornarTrue()
        {
            dao.RegistrarEmpleado(CrearEmpleado());
            var resultado = dao.ExisteEmpleado(matriculaDePrueba);
            Assert.IsTrue(resultado.EsExitoso && resultado.Valor, "El empleado no fue encontrado: " + resultado.Error);
        }

        [TestMethod]
        public void VerificarInicioSesion_DeberiaValidarCredenciales()
        {
            var empleado = CrearEmpleado();
            dao.RegistrarEmpleado(empleado);
            var resultado = dao.VerificarInicioSesion(matriculaDePrueba, empleado.contraseña);
            Assert.IsTrue(resultado.EsExitoso && resultado.Valor, "Inicio de sesión fallido: " + resultado.Error);
        }

        [TestMethod]
        public void InhabilitarEmpleado_DeberiaActualizarEstado()
        {
            dao.RegistrarEmpleado(CrearEmpleado());
            var empleado = dao.BuscarEmpleadoPorMatricula(matriculaDePrueba).Valor;
            var resultado = dao.InhabilitarEmpleado(empleado.idEmpleado);
            Assert.IsTrue(resultado.EsExitoso, "No se pudo inhabilitar el empleado: " + resultado.Error);
        }

        [TestMethod]
        public void BuscarEmpleadoPorMatricula_NoDeberiaEncontrarlo()
        {
            var resultado = dao.BuscarEmpleadoPorMatricula("EMP_NO_EXISTE");
            Assert.IsFalse(resultado.EsExitoso, "Se esperaba que no se encontrara el empleado.");
        }

        [TestMethod]
        public void VerificarInicioSesion_ContrasenaIncorrecta_DeberiaFallar()
        {
            var empleado = CrearEmpleado();
            dao.RegistrarEmpleado(empleado);
            var resultado = dao.VerificarInicioSesion(matriculaDePrueba, HashContraseña("ContraseñaMala"));
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void VerificarInicioSesion_EmpleadoInexistente_DeberiaFallar()
        {
            var resultado = dao.VerificarInicioSesion("EMP_INVENTADO", HashContraseña("cualquiera"));
            Assert.IsFalse(resultado.EsExitoso, "Se esperaba que fallara la verificación de inicio de sesión por empleado inexistente.");
        }

        [TestMethod]
        public void ExisteEmpleado_DeberiaRetornarFalse()
        {
            var resultado = dao.ExisteEmpleado("EMP_NO_EXISTE");
            Assert.IsFalse(resultado.EsExitoso);
        }

        [TestMethod]
        public void ModificarEmpleado_Inexistente_DeberiaFallar()
        {
            var empleadoFalso = CrearEmpleado();
            empleadoFalso.idEmpleado = -999;
            var resultado = dao.ModificarEmpleado(empleadoFalso);
            Assert.IsFalse(resultado.EsExitoso, "No debería poder modificarse un empleado inexistente.");
        }

        [TestMethod]
        public void InhabilitarEmpleado_Inexistente_DeberiaFallar()
        {
            var resultado = dao.InhabilitarEmpleado(-999);
            Assert.IsFalse(resultado.EsExitoso, "No debería poder inhabilitarse un empleado inexistente.");
        }


        private Empleado CrearEmpleado()
        {
            var contra = HashContraseña("Jesus_777");
            return new Empleado()
            {
                nombre = "Jesus",
                apellido = "Diaz",
                nss = "12345678901",
                rol = "Gerente",
                fechaDeNacimiento = new DateTime(1995, 6, 15),
                sexo = "M",
                numeroTelefono = "5551234567",
                correoElectronico = "jesus@gmail.com",
                calle = "Av. Reforma",
                numeroDeCasa = "102",
                codigoPostal = "06400",
                rfc = "RARL950615HDF",
                matriculaEmpleado = "EMP2025001",
                foto = null,
                contratado = true,
                contraseña = contra,
                idSucursal = 2
            };
        }

        private byte[] HashContraseña(string contraseña)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] contraseñaBytes = Encoding.UTF8.GetBytes(contraseña);
                return sha256.ComputeHash(contraseñaBytes);
            }
        }

    }
}
