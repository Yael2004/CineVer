using CineVerEntidades;
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
    public class FuncionServicio : IFuncionServicio
    {
        GestorFuncion gestorFuncion = new GestorFuncion();
        public string AgregarFuncion(FuncionDTO funcion)
        {
            var result = gestorFuncion.AgregarFuncion(funcion);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return result.Error;
            }
        }

        public string EditarFUncion(FuncionDTO funcionOriginal, FuncionDTO funcionEditada)
        {
            var result = gestorFuncion.EditarFuncion(funcionOriginal,funcionEditada);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return result.Error;
            }
        }

        public string EliminarFuncion(FuncionDTO funcion)
        {
            var result = gestorFuncion.EliminarFuncion(funcion);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return result.Error;
            }
        }

        public ListaFuncionesDTO ObtenerFuncionesPorFecha(DateTime fecha)
        {
            var result = gestorFuncion.ObetenerFuncionesPorFecha(fecha);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return new ListaFuncionesDTO
                {
                    funciones = new List<FuncionDTO>(),
                    result = new ResultDTO(false, result.Error)
                };
            }
        }

        public ListaFuncionesDTO ObtenerFuncionesPorFechaYSala(int idSala, DateTime fecha)
        {
            var result = gestorFuncion.ObtenerFuncionesPorSalaYFecha(idSala,fecha);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return new ListaFuncionesDTO
                {
                    funciones = new List<FuncionDTO>(),
                    result = new ResultDTO(false, result.Error)
                };
            }
        }

        public ListaFuncionesDTO ObtenerFuncionesPorPeliculaYFecha(int idPelicula, DateTime fecha)
        {
            var result = gestorFuncion.ObtenerFUncionesPorPeliculaYFecha(idPelicula,fecha);
            if (result.EsExitoso)
            {
                return result.Valor;
            }
            else
            {
                return new ListaFuncionesDTO
                {
                    funciones = new List<FuncionDTO>(),
                    result = new ResultDTO(false, result.Error)
                };
            }
        }
    }
}
