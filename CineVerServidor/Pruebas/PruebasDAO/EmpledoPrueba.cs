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

        [TestInitialize]
        public void Setup()
        {
            dao = new EmpleadoDAO();
        }

        [TestMethod]
        public void RegistrarEmpleado_Exito()
        {
            var empleado = CrearEmpleado();
            var resultado = dao.RegistrarEmpleado(empleado);
            Assert.IsTrue(resultado.EsExitoso, "Error al registrar el empleado: " + resultado.Error);
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
