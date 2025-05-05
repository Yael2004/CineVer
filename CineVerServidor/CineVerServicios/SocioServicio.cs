using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace CineVerServicios
{
    public class SocioServicio : ISocioServicio
    {
        private GestorSocio _gestorSocio = new GestorSocio();

        public Task<ResultDTO> RegistrarSocio(SocioDTO socioDTO)
        {
            var resultado = _gestorSocio.RegistrarSocio(socioDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> ModificarSocio(SocioDTO socioDTO)
        {
            var resultado = _gestorSocio.ModificarSocio(socioDTO);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ResultDTO> InhabilitarCuentaSocio(int idSocio)
        {
            var resultado = _gestorSocio.InhabilitarCuentaSocio(idSocio);

            if (resultado.EsExitoso)
            {
                return Task.FromResult(new ResultDTO(true, string.Empty));
            }
            else
            {
                return Task.FromResult(new ResultDTO(false, resultado.Error));
            }
        }

        public Task<ListaSociosDTO> ObtenerSocios()
        {
            throw new NotImplementedException();
        }

        public Task<SocioResponseDTO> BuscarSocioPorFolio(string folio)
        {
            var resultado = _gestorSocio.BuscarSocioPorFolio(folio);

            if (!resultado.EsExitoso)
            {
                return Task.FromResult(new SocioResponseDTO
                {
                    ResultDTO = new ResultDTO(false, resultado.Error)
                });
            }

            var socio = resultado.Valor;

            var socioDTO = new SocioDTO
            {
                IdSocio = socio.IdSocio,
                Nombres = socio.Nombres,
                Apellidos = socio.Apellidos,
                Correo = socio.Correo,
                Sexo = socio.Sexo,
                NumeroTelefono = socio.NumeroTelefono,
                Calle = socio.Calle,
                NumeroCasa = socio.NumeroCasa,
                CodigoPostal = socio.CodigoPostal,
                FechaNacimiento = (DateTime)socio.FechaNacimiento,
                Folio = socio.Folio
            };

            return Task.FromResult(new SocioResponseDTO
            {
                socio = socioDTO,
                ResultDTO = new ResultDTO(true, string.Empty)
            });
        }

        public Task<ResultDTO> ExisteSocio(string folio)
        {
            var resultado = _gestorSocio.ExisteSocio(folio);

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
