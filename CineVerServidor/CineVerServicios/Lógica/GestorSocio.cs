using CineVerEntidades;
using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios.Lógica
{
    public class GestorSocio
    {
        private SocioDAO socioDAO = new SocioDAO();

        public Result<string> RegistrarSocio(SocioDTO socioDTO)
        {
            var socio = new Socio

            {
                nombre = socioDTO.Nombres,
                apellido = socioDTO.Apellidos,
                correoElectronico = socioDTO.Correo,
                Sexo = socioDTO.Sexo,
                numeroTelefono = socioDTO.NumeroTelefono,
                calle = socioDTO.Calle,
                numeroCasa = socioDTO.NumeroCasa,
                codigoPostal = socioDTO.CodigoPostal,
                fechaNacimiento = socioDTO.FechaNacimiento,
                folio = socioDTO.Folio,
                afiliado = socioDTO.Afiliado
            };

            var resultado = socioDAO.RegistrarSocio(socio);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Socio registrado exitosamente");
            }
        }

        public Result<string> ModificarSocio(SocioDTO socioDTO)
        {
            var socio = new Socio
            {
                idSocio = socioDTO.IdSocio,
                nombre = socioDTO.Nombres,
                apellido = socioDTO.Apellidos,
                correoElectronico = socioDTO.Correo,
                Sexo = socioDTO.Sexo,
                numeroTelefono = socioDTO.NumeroTelefono,
                calle = socioDTO.Calle,
                numeroCasa = socioDTO.NumeroCasa,
                codigoPostal = socioDTO.CodigoPostal,
                fechaNacimiento = socioDTO.FechaNacimiento,
                folio = socioDTO.Folio,
                afiliado = socioDTO.Afiliado
            };

            var resultado = socioDAO.ModificarSocio(socio);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Socio modificado exitosamente");
            }
        }

        public Result<string> InhabilitarCuentaSocio(int idSocio)
        {
            var resultado = socioDAO.InhabilitarCuentaSocio(idSocio);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Cuenta de socio inhabilitada exitosamente");
            }
        }

        public Result<List<SocioDTO>> ObtenerSocios()
        {
            var resultado = socioDAO.ObtenerSocios();

            if (!resultado.EsExitoso)
            {
                return Result<List<SocioDTO>>.Fallo(resultado.Error);
            }
            else
            {
                var listaSocios = new ListaSociosDTO();

                foreach (var socio in resultado.Valor)
                {
                    var socioDTO = new SocioDTO
                    {
                        IdSocio = socio.idSocio,
                        Nombres = socio.nombre,
                        Apellidos = socio.apellido,
                        Correo = socio.correoElectronico,
                        Sexo = socio.Sexo,
                        NumeroTelefono = socio.numeroTelefono,
                        Calle = socio.calle,
                        NumeroCasa = socio.numeroCasa,
                        CodigoPostal = socio.codigoPostal,
                        FechaNacimiento = (DateTime)socio.fechaNacimiento,
                        Folio = socio.folio,
                        Afiliado = (bool)socio.afiliado
                    };
                    listaSocios.Socios.Add(socioDTO);
                }

                return Result<List<SocioDTO>>.Exito(listaSocios.Socios);
            }
        }

        public Result<SocioDTO> BuscarSocioPorFolio(string folio)
        {
            var resultado = socioDAO.BuscarSocioPorFolio(folio);

            if (!resultado.EsExitoso)
            {
                return Result<SocioDTO>.Fallo(resultado.Error);
            }
            else
            {
                var socio = resultado.Valor;

                var socioDTO = new SocioDTO
                {
                    IdSocio = socio.idSocio,
                    Nombres = socio.nombre,
                    Apellidos = socio.apellido,
                    Correo = socio.correoElectronico,
                    Sexo = socio.Sexo,
                    NumeroTelefono = socio.numeroTelefono,
                    Calle = socio.calle,
                    NumeroCasa = socio.numeroCasa,
                    CodigoPostal = socio.codigoPostal,
                    FechaNacimiento = (DateTime)socio.fechaNacimiento,
                    Folio = socio.folio,
                    Afiliado = (bool)socio.afiliado
                };

                return Result<SocioDTO>.Exito(socioDTO);
            }
        }

        public Result<bool> ExisteSocio(string folio)
        {
            var resultado = socioDAO.ExisteSocio(folio);

            if (!resultado.EsExitoso)
            {
                return Result<bool>.Fallo(resultado.Error);
            }
            else
            {
                return Result<bool>.Exito(resultado.Valor);
            }
        }
    }
}
