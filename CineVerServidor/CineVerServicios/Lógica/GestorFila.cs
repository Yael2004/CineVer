using CineVerServicios.DTO;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using CineVerEntidades;

namespace CineVerServicios.Lógica
{
    public class GestorFila
    {
        private FilaDAO FilaDAO = new FilaDAO();
        public Result<ListaFilasDTO> ObetnerFilasDeSala(int idSala)
        {
            var filas = FilaDAO.ObtenerFilasDeSala(idSala);
            if (filas.EsExitoso)
            {
                ListaFilasDTO filasDTO = new ListaFilasDTO();
                foreach(var fila in filas.Valor)
                {
                    var filaDTO = new FilaDTO();
                    filaDTO.númeroFila = fila.númeroFila;
                    filaDTO.numeroAsientos = fila.numeroAsientos;
                    filaDTO.idFila = fila.idFila;
                    filaDTO.idSala = fila.idSala;
                    filasDTO.Filas.Add(filaDTO);
                }
                return Result<ListaFilasDTO>.Exito(filasDTO);
            }
            else
            {
                return Result<ListaFilasDTO>.Fallo(filas.Error);
            }
        }
        public Result<string> AgregarFila(FilaDTO fila)
        {
            var filaEntitie = new Fila();
            filaEntitie.idFila = fila.idFila;
            filaEntitie.idSala = fila.idSala;
            filaEntitie.númeroFila = fila.númeroFila;
            filaEntitie.numeroAsientos = fila.numeroAsientos;
            var result = FilaDAO.AgregarFila(filaEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result <string> EditarFila(FilaDTO filaOriginal, FilaDTO filaEditada)
        {
            var filaEntitieOriginal = new Fila();
            filaEntitieOriginal.idFila = filaOriginal.idFila;
            filaEntitieOriginal.idSala = filaOriginal.idSala;
            filaEntitieOriginal.numeroAsientos = filaOriginal.numeroAsientos;
            filaEntitieOriginal.númeroFila = filaOriginal.númeroFila;
            var filaEntitieEditada = new Fila();
            filaEntitieEditada.idFila = filaEditada.idFila;
            filaEntitieEditada.idSala = filaEditada.idSala;
            filaEntitieEditada.numeroAsientos = filaEditada.numeroAsientos;
            filaEntitieEditada.númeroFila = filaEditada.númeroFila;
            var result = FilaDAO.EditarFila(filaEntitieOriginal, filaEntitieEditada);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<int> ObtenerIdFila(int idSala, int numeroFila)
        {
            var result = FilaDAO.ObtenerIdFila(idSala, numeroFila);
            if (result.EsExitoso)
            {
                return Result<int>.Exito(result.Valor);
            }
            else
            {
                return Result<int>.Fallo(result.Error);
            }
        }
        public Result<string> EliminarFila(FilaDTO fila)
        {
            var filaEntitie = new Fila();
            filaEntitie.idFila = fila.idFila;
            filaEntitie.idSala = fila.idSala;
            filaEntitie.numeroAsientos = fila.numeroAsientos;
            filaEntitie.númeroFila = fila.númeroFila;
            var result = FilaDAO.EliminarFila(filaEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
    }
}
