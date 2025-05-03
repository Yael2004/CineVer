using CineVerServicios.DTO;
using CineVerServicios.Interfaces;
using CineVerServicios.Lógica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios
{
    internal class SalaServicio : ISalaServicio
    {
        private GestorSala gestorSala = new GestorSala();
        public Task<string> AgregarSala(SalaDTO sala)
        {
            var result = gestorSala.AgregarSala(sala);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EditarSala(SalaDTO salaEditada, SalaDTO salaOriginal)
        {
            var result = gestorSala.EditarSala(salaEditada, salaOriginal);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EliminarSala(SalaDTO sala)
        {
            var result = gestorSala.EliminarSala(sala);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<int> ObtenerIdSala(int idSucursal, string nombre)
        {
            var result = gestorSala.ObtenerIdSala(idSucursal, nombre);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(-1);
            }
        }

        public Task<ListaSalaDTO> ObtenerSalasPorSucursal(int idSucursal)
        {
            var salas = gestorSala.ObtenerListaSalasPorSucursal(idSucursal);
            if (salas.EsExitoso)
            {
                return Task.FromResult(salas.Valor);
            }
            else
            {
                return Task.FromResult(new ListaSalaDTO
                {
                    Salas = new List<SalaDTO>(),
                    Result = new ResultDTO(false, salas.Error)
                });
            }
        }
    }
}
