﻿using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios
{
    public class EmpleadoServicio : IEmpleadoServicio
    {
        private GestorEmpleado _gestorEmpleado = new GestorEmpleado();

        public Task<ResultDTO> RegistrarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var resultado = _gestorEmpleado.RegistrarEmpleado(empleadoDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> ModificarEmpleado(EmpleadoDTO empleadoDTO)
        {
            var resultado = _gestorEmpleado.ModificarEmpleado(empleadoDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> InhabilitarEmpleado(int idEmpleado)
        {
            var resultado = _gestorEmpleado.InhabilitarEmpleado(idEmpleado);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ListaEmpleadosDTO> ObtenerEmpleados()
        {
            var resultado = _gestorEmpleado.ObtenerEmpleados();

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ListaEmpleadosDTO
                {
                    Empleados = resultado.Valor,
                    Result = new ResultDTO(true, string.Empty)
                });
            }
            else
            {
                return Task.FromResult(new ListaEmpleadosDTO
                {
                    Result = new ResultDTO(false, resultado.Error)
                });
            }
        }

        public Task<EmpleadoResponseDTO> BuscarEmpleadoPorMatricula(string matricula)
        {
            var resultado = _gestorEmpleado.BuscarEmpleadoPorMatricula(matricula);

            if (!resultado.EsExitoso)
            {
                return Task.FromResult(new EmpleadoResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }

            var empleado = resultado.Valor;

            var empleadoDTO = new EmpleadoDTO
            {
                IdEmpleado = empleado.IdEmpleado,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                Nss = empleado.Nss,
                Rol = empleado.Rol,
                FechaNacimiento = empleado.FechaNacimiento,
                Sexo = empleado.Sexo,
                NumeroTelefono = empleado.NumeroTelefono,
                Correo = empleado.Correo,
                Calle = empleado.Calle,
                NumeroCasa = empleado.NumeroCasa,
                CodigoPostal = empleado.CodigoPostal,
                RFC = empleado.RFC,
                Matricula = empleado.Matricula,
                Foto = empleado.Foto,
                Contratado = empleado.Contratado,
                Contraseña = empleado.Contraseña,
                IdSucursal = (int)empleado.IdSucursal
            };

            return Task.FromResult(new EmpleadoResponseDTO
            {
                empleado = empleadoDTO,
                ResultDTO = new ResultDTO(true, string.Empty)
            });
        }

        public Task<ResultDTO> ExisteEmpleado(string matricula)
        {
            var resultado = _gestorEmpleado.ExisteEmpleado(matricula);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> VerificarInicioSesion(string matricula, byte[] contraseña)
        {
            var resultado = _gestorEmpleado.VerificarInicioSesion(matricula, contraseña);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }
    }
}
