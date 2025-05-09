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
    public class GestorSala
    {
        private SalaDAO salaDAO = new SalaDAO();
        public GestorSala() { }
        public Result<ListaSalaDTO> ObtenerListaSalasPorSucursal(int idScurusal)
        {
            var result = salaDAO.ObtenerSalasPorSucursal(idScurusal);
            if (!result.EsExitoso)
            {
                return Result<ListaSalaDTO>.Fallo(result.Error);
            }
            else
            {
                var listaSalas = new ListaSalaDTO();
                foreach(Sala sala in result.Valor)
                {
                    var salaDTO = new SalaDTO();
                    salaDTO.estadoSala = sala.estadoSala;
                    salaDTO.idSala = sala.idSala;
                    salaDTO.idSucursal = sala.idSucursal;
                    salaDTO.descripcion = sala.descripcion;
                    salaDTO.nombre = sala.nombre;
                    salaDTO.numeroFilas = sala.numeroFilas;
                    listaSalas.Salas.Add(salaDTO);
                }
                return Result<ListaSalaDTO>.Exito(listaSalas);
            }
        }
        public Result<string> AgregarSala(SalaDTO salaDTO)
        {
            var salaEntitie = new Sala();
            salaEntitie.idSala = salaDTO.idSala;
            salaEntitie.nombre = salaDTO.nombre;
            salaEntitie.estadoSala = salaDTO.estadoSala;
            salaEntitie.idSucursal = salaDTO.idSucursal;
            salaEntitie.descripcion = salaDTO.descripcion;
            salaEntitie.numeroFilas = salaDTO.numeroFilas;
            var result = salaDAO.AgregarSala(salaEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<string> EditarSala(SalaDTO salaEditada, SalaDTO salaOriginal)
        {
            var salaEntitieEditada = new Sala();
            salaEntitieEditada.idSala = salaEditada.idSala;
            salaEntitieEditada.nombre = salaEditada.nombre;
            salaEntitieEditada.estadoSala = salaEditada.estadoSala;
            salaEntitieEditada.idSucursal = salaEditada.idSucursal;
            salaEntitieEditada.descripcion = salaEditada.descripcion;
            salaEntitieEditada.numeroFilas = salaEditada.numeroFilas;

            var salaEntitieOriginal = new Sala();
            salaEntitieOriginal.idSala = salaOriginal.idSala;
            salaEntitieOriginal.nombre = salaOriginal.nombre;
            salaEntitieOriginal.estadoSala = salaOriginal.estadoSala;
            salaEntitieOriginal.idSucursal = salaOriginal.idSucursal;
            salaEntitieOriginal.descripcion = salaOriginal.descripcion;
            salaEntitieOriginal.numeroFilas = salaOriginal.numeroFilas;

            var result = salaDAO.EditarSala(salaEntitieEditada,salaEntitieOriginal);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<ListaSalaDTO> ObtenerSalasPorNombreYSucursal(int idSucursal, string nombreSala)
        {
            var result = salaDAO.ObtenerSalasPorSucursalYNombre(idSucursal, nombreSala);
            if (!result.EsExitoso)
            {
                return Result<ListaSalaDTO>.Fallo(result.Error);
            }
            else
            {
                var listaSalas = new ListaSalaDTO();
                foreach (Sala sala in result.Valor)
                {
                    var salaDTO = new SalaDTO();
                    salaDTO.estadoSala = sala.estadoSala;
                    salaDTO.idSala = sala.idSala;
                    salaDTO.idSucursal = sala.idSucursal;
                    salaDTO.descripcion = sala.descripcion;
                    salaDTO.nombre = sala.nombre;
                    salaDTO.numeroFilas = sala.numeroFilas;
                    listaSalas.Salas.Add(salaDTO);
                }
                return Result<ListaSalaDTO>.Exito(listaSalas);
            }
        }

        public Result<SalaDTO> ObtenerSalaPorID(int idSala)
        {
            var result = salaDAO.ObtenerSalaPorId(idSala);
            if (!result.EsExitoso)
            {
                return Result<SalaDTO>.Fallo(result.Error);
            }
            else
            {
                var salaDTO = new SalaDTO();
                salaDTO.estadoSala = result.Valor.estadoSala;
                salaDTO.idSala = result.Valor.idSala;
                salaDTO.idSucursal = result.Valor.idSucursal;
                salaDTO.descripcion = result.Valor.descripcion;
                salaDTO.nombre = result.Valor.nombre;
                salaDTO.numeroFilas = result.Valor.numeroFilas;
                return Result<SalaDTO>.Exito(salaDTO);
            }
        }
        public Result<string> EliminarSala(SalaDTO salaDTO)
        {
            var salaEntitie = new Sala();
            salaEntitie.idSala = salaDTO.idSala;
            salaEntitie.nombre = salaDTO.nombre;
            salaEntitie.estadoSala = salaDTO.estadoSala;
            salaEntitie.idSucursal = salaDTO.idSucursal;
            salaEntitie.descripcion = salaDTO.descripcion;
            salaEntitie.numeroFilas = salaDTO.numeroFilas;
            var result = salaDAO.EliminarSala(salaEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<int> ObtenerIdSala(int idScurusal, string nombre)
        {
            var result = salaDAO.ObtenerIDSala(idScurusal, nombre);
            if(result.EsExitoso)
            {
                return Result<int>.Exito(result.Valor);
            }
            else
            {
                return Result<int>.Fallo(result.Error);
            }
        }
    }
}
