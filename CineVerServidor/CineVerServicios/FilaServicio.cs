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
    public class FilaServicio : IFilaServicio
    {
        private GestorFila gestorFila = new GestorFila();

        public Task<string> AgregarFila(FilaDTO fila)
        {
            var result = gestorFila.AgregarFila(fila);
            if (result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EditarFila(FilaDTO filaEditada, FilaDTO filaOriginal)
        {
            var result = gestorFila.EditarFila(filaEditada, filaOriginal);
            if(result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<string> EliminarFila(FilaDTO fila)
        {
            var result = gestorFila.EliminarFila(fila);
            if(result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(result.Error);
            }
        }

        public Task<ListaFilasDTO> ObtenerFilasDeSala(int idSala)
        {
            var result = gestorFila.ObetnerFilasDeSala(idSala);
            if(result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(new ListaFilasDTO
                {
                    Filas = new List<FilaDTO>(),
                    Result = new ResultDTO(false, result.Error)
                });
            }
        }

        public Task<int> ObtenerIdFila(int idSala, int numeroFila)
        {
            var result = gestorFila.ObtenerIdFila(idSala, numeroFila);
            if(result.EsExitoso)
            {
                return Task.FromResult(result.Valor);
            }
            else
            {
                return Task.FromResult(-1);
            }
        }
    }
}
