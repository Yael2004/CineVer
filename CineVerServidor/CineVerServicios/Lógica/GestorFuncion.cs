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
    public class GestorFuncion
    {
        private FuncionDAO funcionDAO = new FuncionDAO();
        public Result<ListaFuncionesDTO> ObtenerFUncionesPorPeliculaYFecha(int idPelicula, DateTime fecha)
        {
            var result = funcionDAO.ObtenerFuncionesPorPeliculaYFecha(idPelicula, fecha);
            if (result.EsExitoso)
            {
                var listaFunciones = new ListaFuncionesDTO();
                foreach(var funcion in result.Valor)
                {
                    var funcionInsertar = new FuncionDTO();

                    SalaDTO salaDTO = new SalaDTO();
                    salaDTO.idSala = funcion.Sala.idSala;
                    salaDTO.nombre = funcion.Sala.nombre;
                    salaDTO.descripcion = funcion.Sala.descripcion;
                    salaDTO.estadoSala = funcion.Sala.estadoSala;
                    salaDTO.numeroFilas = funcion.Sala.numeroFilas;
                    funcionInsertar.Sala = salaDTO;

                    funcionInsertar.idSala = funcion.idSala;
                    funcionInsertar.idPelicula = funcion.idPelicula;
                    funcionInsertar.idFuncion = funcion.idFuncion;

                    PeliculaDTOs peliculaDTO = new PeliculaDTOs();
                    peliculaDTO.idPelicula = funcion.Película.idPelicula;
                    peliculaDTO.nombre = funcion.Película.nombre;
                    peliculaDTO.duracion = funcion.Película.duracion;
                    peliculaDTO.genero = funcion.Película.genero;
                    peliculaDTO.sinopsis = funcion.Película.sinopsis;
                    peliculaDTO.director = funcion.Película.director;
                    peliculaDTO.poster = funcion.Película.poster;
                    funcionInsertar.Película = peliculaDTO;
                    

                    funcionInsertar.fecha = funcion.fecha;
                    funcionInsertar.precioBoleto = funcion.precioBoleto;
                    funcionInsertar.horaInicio = funcion.horaInicio;
                    listaFunciones.funciones.Add(funcionInsertar);
                }
                return Result<ListaFuncionesDTO>.Exito(listaFunciones);
            }
            else
            {
                return Result<ListaFuncionesDTO>.Fallo(result.Error);
            }
        }
        public Result<ListaFuncionesDTO> ObetenerFuncionesPorFecha(DateTime fecha)
        {
            var result = funcionDAO.ObtenerFUncionesPorFecha(fecha);
            if (result.EsExitoso)
            {
                var listaFunciones = new ListaFuncionesDTO();
                foreach (var funcion in result.Valor)
                {
                    var funcionInsertar = new FuncionDTO();

                    SalaDTO salaDTO = new SalaDTO();
                    salaDTO.idSala = funcion.Sala.idSala;
                    salaDTO.nombre = funcion.Sala.nombre;
                    salaDTO.descripcion = funcion.Sala.descripcion;
                    salaDTO.estadoSala = funcion.Sala.estadoSala;
                    salaDTO.numeroFilas = funcion.Sala.numeroFilas;
                    funcionInsertar.Sala = salaDTO;
                    funcionInsertar.idSala = funcion.idSala;
                    funcionInsertar.idPelicula = funcion.idPelicula;
                    funcionInsertar.idFuncion = funcion.idFuncion;


                    PeliculaDTOs peliculaDTO = new PeliculaDTOs();
                    peliculaDTO.idPelicula = funcion.Película.idPelicula;
                    peliculaDTO.nombre = funcion.Película.nombre;
                    peliculaDTO.duracion = funcion.Película.duracion;
                    peliculaDTO.genero = funcion.Película.genero;
                    peliculaDTO.sinopsis = funcion.Película.sinopsis;
                    peliculaDTO.director = funcion.Película.director;
                    peliculaDTO.poster = funcion.Película.poster;
                    funcionInsertar.Película = peliculaDTO;

                    funcionInsertar.fecha = funcion.fecha;
                    funcionInsertar.precioBoleto = funcion.precioBoleto;
                    funcionInsertar.horaInicio = funcion.horaInicio;
                    listaFunciones.funciones.Add(funcionInsertar);
                }
                return Result<ListaFuncionesDTO>.Exito(listaFunciones);
            }
            else
            {
                return Result<ListaFuncionesDTO>.Fallo(result.Error);
            }
        }
        public Result<ListaFuncionesDTO> ObtenerFuncionesPorSalaYFecha(int idSala, DateTime fecha)
        {
            var result = funcionDAO.ObtenerFuncionesPorSalaYFecha(idSala, fecha);
            if (result.EsExitoso)
            {
                var listaFunciones = new ListaFuncionesDTO();
                foreach (var funcion in result.Valor)
                {
                    var funcionInsertar = new FuncionDTO();

                    SalaDTO salaDTO = new SalaDTO();
                    salaDTO.idSala = funcion.Sala.idSala;
                    salaDTO.nombre = funcion.Sala.nombre;
                    salaDTO.descripcion = funcion.Sala.descripcion;
                    salaDTO.estadoSala = funcion.Sala.estadoSala;
                    salaDTO.numeroFilas = funcion.Sala.numeroFilas;
                    funcionInsertar.Sala = salaDTO;

                    funcionInsertar.idSala = funcion.idSala;
                    funcionInsertar.idPelicula = funcion.idPelicula;
                    funcionInsertar.idFuncion = funcion.idFuncion;

                    PeliculaDTOs peliculaDTO = new PeliculaDTOs();
                    peliculaDTO.idPelicula = funcion.Película.idPelicula;
                    peliculaDTO.nombre = funcion.Película.nombre;
                    peliculaDTO.duracion = funcion.Película.duracion;
                    peliculaDTO.genero = funcion.Película.genero;
                    peliculaDTO.sinopsis = funcion.Película.sinopsis;
                    peliculaDTO.director = funcion.Película.director;
                    peliculaDTO.poster = funcion.Película.poster;
                    funcionInsertar.Película = peliculaDTO;

                    funcionInsertar.fecha = funcion.fecha;
                    funcionInsertar.precioBoleto = funcion.precioBoleto;
                    funcionInsertar.horaInicio = funcion.horaInicio;
                    listaFunciones.funciones.Add(funcionInsertar);
                }
                return Result<ListaFuncionesDTO>.Exito(listaFunciones);
            }
            else
            {
                return Result<ListaFuncionesDTO>.Fallo(result.Error);
            }
        }
        public Result<string> AgregarFuncion(FuncionDTO funcion)
        {
            Función funcionEntitie = new Función();
            funcionEntitie.idFuncion = funcion.idFuncion;
            funcionEntitie.idSala = funcion.idSala;
            funcionEntitie.idPelicula = funcion.idPelicula;
            
            funcionEntitie.horaInicio = funcion.horaInicio;
            funcionEntitie.precioBoleto = funcion.precioBoleto;
            funcionEntitie.fecha = funcion.fecha;
            var result = funcionDAO.AgregarFuncion(funcionEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<string> EliminarFuncion(FuncionDTO funcion)
        {
            Función funcionEntitie = new Función();
            funcionEntitie.idFuncion = funcion.idFuncion;
            funcionEntitie.idSala = funcion.idSala;
            funcionEntitie.idPelicula = funcion.idPelicula;


            
            funcionEntitie.horaInicio = funcion.horaInicio;
            funcionEntitie.precioBoleto = funcion.precioBoleto;
            funcionEntitie.fecha = funcion.fecha;
            var result = funcionDAO.EliminarFuncion(funcionEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<string> EditarFuncion (FuncionDTO funcionOriginal,FuncionDTO funcionEditada)
        {
            Función funcionEntitieOriginal = new Función();
            funcionEntitieOriginal.idFuncion = funcionOriginal.idFuncion;
            funcionEntitieOriginal.idSala = funcionOriginal.idSala;
            funcionEntitieOriginal.idPelicula = funcionOriginal.idPelicula;
            
            funcionEntitieOriginal.horaInicio = funcionOriginal.horaInicio;
            funcionEntitieOriginal.precioBoleto = funcionOriginal.precioBoleto;
            funcionEntitieOriginal.fecha = funcionOriginal.fecha;

            Función funcionEntitieEditada = new Función();
            funcionEntitieEditada.idFuncion = funcionEditada.idFuncion;
            funcionEntitieEditada.idSala = funcionEditada.idSala;
            funcionEntitieEditada.idPelicula = funcionEditada.idPelicula;
            
            funcionEntitieEditada.horaInicio = funcionEditada.horaInicio;
            funcionEntitieEditada.precioBoleto = funcionEditada.precioBoleto;
            funcionEntitieEditada.fecha = funcionEditada.fecha;

            var result = funcionDAO.EditarFuncion(funcionEntitieOriginal, funcionEntitieEditada);
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
