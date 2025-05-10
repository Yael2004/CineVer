using CineVerServicios.DTO;
using CineVerEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorEmpleado
    {
        private EmpleadoDAO empleadoDAO = new EmpleadoDAO();

        public Result<string> RegistrarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleado = new Empleado

            {
                nombre = empleadoDTO.Nombres,
                apellido = empleadoDTO.Apellidos,
                nss = empleadoDTO.Nss,
                rol = empleadoDTO.Rol,
                fechaDeNacimiento = empleadoDTO.FechaNacimiento,
                sexo = empleadoDTO.Sexo,
                numeroTelefono = empleadoDTO.NumeroTelefono,
                correoElectronico = empleadoDTO.Correo,
                calle = empleadoDTO.Calle,
                numeroDeCasa = empleadoDTO.NumeroCasa,
                codigoPostal = empleadoDTO.CodigoPostal,
                rfc = empleadoDTO.RFC,
                matriculaEmpleado = empleadoDTO.Matricula,
                foto = empleadoDTO.Foto,
                contratado = empleadoDTO.Contratado,
                contraseña = empleadoDTO.Contraseña,
                idSucursal = empleadoDTO.IdSucursal
            };

            var resultado = empleadoDAO.RegistrarEmpleado(empleado);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Empleado registrado exitosamente");
            }
        }

        public Result<string> ModificarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleado = new Empleado

            {
                idEmpleado = empleadoDTO.IdEmpleado,
                nombre = empleadoDTO.Nombres,
                apellido = empleadoDTO.Apellidos,
                nss = empleadoDTO.Nss,
                rol = empleadoDTO.Rol,
                fechaDeNacimiento = empleadoDTO.FechaNacimiento,
                sexo = empleadoDTO.Sexo,
                numeroTelefono = empleadoDTO.NumeroTelefono,
                correoElectronico = empleadoDTO.Correo,
                calle = empleadoDTO.Calle,
                numeroDeCasa = empleadoDTO.NumeroCasa,
                codigoPostal = empleadoDTO.CodigoPostal,
                rfc = empleadoDTO.RFC,
                matriculaEmpleado = empleadoDTO.Matricula,
                foto = empleadoDTO.Foto,
                contratado = empleadoDTO.Contratado,
                contraseña = empleadoDTO.Contraseña,
                idSucursal = empleadoDTO.IdSucursal
            };

            var resultado = empleadoDAO.ModificarEmpleado(empleado);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Empleado modificado exitosamente");
            }
        }

        public Result<string> InhabilitarEmpleado(int idEmpleado)
        {
            var resultado = empleadoDAO.InhabilitarEmpleado(idEmpleado);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Empleado inhabilitado exitosamente");
            }
        }

        public Result<List<EmpleadoDTO>> ObtenerEmpleados()
        {
            var resultado = empleadoDAO.ObtenerEmpleados();

            if (!resultado.EsExitoso)
            {
                return Result<List<EmpleadoDTO>>.Fallo(resultado.Error);
            }
            else
            {
                var listaEmpleados = new ListaEmpleadosDTO();

                foreach (var empleado in resultado.Valor)
                {
                    var empleadoDTO = new EmpleadoDTO
                    {
                        IdEmpleado = empleado.idEmpleado,
                        Nombres = empleado.nombre,
                        Apellidos = empleado.apellido,
                        Nss = empleado.nss,
                        Rol = empleado.rol,
                        FechaNacimiento = (DateTime)empleado.fechaDeNacimiento,
                        Sexo = empleado.sexo,
                        NumeroTelefono = empleado.numeroTelefono,
                        Correo = empleado.correoElectronico,
                        Calle = empleado.calle,
                        NumeroCasa = empleado.numeroDeCasa,
                        CodigoPostal = empleado.codigoPostal,
                        RFC = empleado.rfc,
                        Matricula = empleado.matriculaEmpleado,
                        Foto = empleado.foto,
                        Contratado = (bool)empleado.contratado,
                        Contraseña = empleado.contraseña,
                        IdSucursal = (int)empleado.idSucursal
                    };
                    listaEmpleados.Empleados.Add(empleadoDTO);
                }

                return Result<List<EmpleadoDTO>>.Exito(listaEmpleados.Empleados);
            }
        }

        public Result<EmpleadoDTO> BuscarEmpleadoPorMatricula(string matricula)
        {
            var resultado = empleadoDAO.BuscarEmpleadoPorMatricula(matricula);

            if (!resultado.EsExitoso)
            {
                return Result<EmpleadoDTO>.Fallo(resultado.Error);
            }
            else
            {
                var empleado = resultado.Valor;

                var empleadoDTO = new EmpleadoDTO
                {
                    IdEmpleado = empleado.idEmpleado,
                    Nombres = empleado.nombre,
                    Apellidos = empleado.apellido,
                    Nss = empleado.nss,
                    Rol = empleado.rol,
                    FechaNacimiento = (DateTime)empleado.fechaDeNacimiento,
                    Sexo = empleado.sexo,
                    NumeroTelefono = empleado.numeroTelefono,
                    Correo = empleado.correoElectronico,
                    Calle = empleado.calle,
                    NumeroCasa = empleado.numeroDeCasa,
                    CodigoPostal = empleado.codigoPostal,
                    RFC = empleado.rfc,
                    Matricula = empleado.matriculaEmpleado,
                    Foto = empleado.foto,
                    Contratado = (bool)empleado.contratado,
                    Contraseña = empleado.contraseña,
                    IdSucursal = (int)empleado.idSucursal
                };

                return Result<EmpleadoDTO>.Exito(empleadoDTO);
            }
        }

        public Result<bool> ExisteEmpleado(string matricula)
        {
            var resultado = empleadoDAO.ExisteEmpleado(matricula);

            if (resultado.EsExitoso)
            {
                return Result<bool>.Exito(resultado.Valor);
            }
            else
            {
                return Result<bool>.Fallo(resultado.Error);
            }
        }

        public Result<bool> VerificarInicioSesion(string matricula, byte[] contraseña)
        {
            var resultado = empleadoDAO.VerificarInicioSesion(matricula, contraseña);

            if (resultado.EsExitoso)
            {
                return Result<bool>.Exito(resultado.Valor);
            }
            else
            {
                return Result<bool>.Fallo(resultado.Error);
            }
        }
    }
}
