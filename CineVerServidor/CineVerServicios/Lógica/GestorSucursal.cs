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
    public class GestorSucursal
    {
        private SucursalDAO sucursalDAO = new SucursalDAO();

        public Result<string> AgregarSucursal(SucursalDTO sucursalDTO)
        {
            var sucursal = new Sucursal
            {
                nombre = sucursalDTO.Nombre,
                estado = sucursalDTO.Estado,
                ciudad = sucursalDTO.Ciudad,
                calle = sucursalDTO.Calle,
                numeroEnLaCalle = sucursalDTO.NumeroEnLaCalle,
                codigoPostal = sucursalDTO.CodigoPostal,
                horaApertura = sucursalDTO.HoraApertura,
                horaCierre = sucursalDTO.HoraCierre,
                estadoSucursal = "Abierta"
            };

            var resultado = sucursalDAO.AgregarSucursal(sucursal);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Sucursal agregada correctamente");
            }
        }

        public Result<ListaSucursalesDTO> ObtenerSucursales()
        {
            var resultado = sucursalDAO.ObtenerSucursales();

            if (!resultado.EsExitoso)
            {
                return Result<ListaSucursalesDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaSucursales = new ListaSucursalesDTO();
                foreach (var sucursal in resultado.Valor)
                {
                    var sucursalDTO = new SucursalDTO
                    {
                        IdSucursal = sucursal.idSucursal,
                        Nombre = sucursal.nombre,
                        Estado = sucursal.estado,
                        Ciudad = sucursal.ciudad,
                        Calle = sucursal.calle,
                        NumeroEnLaCalle = sucursal.numeroEnLaCalle,
                        CodigoPostal = sucursal.codigoPostal,
                        HoraApertura = (TimeSpan)sucursal.horaApertura,
                        HoraCierre = (TimeSpan)sucursal.horaCierre,
                        EstadoSucursal = sucursal.estadoSucursal

                    };
                    listaSucursales.Sucursales.Add(sucursalDTO);
                }
                return Result<ListaSucursalesDTO>.Exito(listaSucursales);
            }
        }

        public Result<string> CerrarSucursal(int sucursalId)
        {
            var resultado = sucursalDAO.CerrarSucursal(sucursalId);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Sucursal cerrada correctamente");
            }
        }

        public Result<string> ActualizarSucursal(int idSucursal, SucursalDTO sucursalDTO)
        {
            var sucursal = new Sucursal
            {
                nombre = sucursalDTO.Nombre,
                estado = sucursalDTO.Estado,
                ciudad = sucursalDTO.Ciudad,
                calle = sucursalDTO.Calle,
                numeroEnLaCalle = sucursalDTO.NumeroEnLaCalle,
                codigoPostal = sucursalDTO.CodigoPostal,
                horaApertura = sucursalDTO.HoraApertura,
                horaCierre = sucursalDTO.HoraCierre,
                estadoSucursal = sucursalDTO.EstadoSucursal
            };

            var resultado = sucursalDAO.ActualizarSucursal(idSucursal, sucursal);

            if (!resultado.EsExitoso)
            {
                return Result<string>.Fallo(resultado.Error);
            }
            else
            {
                return Result<string>.Exito("Sucursal actualizada correctamente");
            }
        }

        public Result<ListaFilasAsientosDTO> ObtenerAsientosPorFila(int idSala)
        {
            var resultado = sucursalDAO.ObtenerAsientosPorFila(idSala);

            if (!resultado.EsExitoso)
            {
                return Result<ListaFilasAsientosDTO>.Fallo(resultado.Error);
            }
            else
            {
                var listaFilas = new ListaFilasAsientosDTO();
                foreach (var fila in resultado.Valor)
                {
                    var filaDTO = new FilasPruebaDTO
                    {
                        NumeroFila = (int)fila.númeroFila,
                        CantidadAsientos = fila.numeroAsientos ?? 0,
                        Asientos = fila.Asiento.Select(a => new AsientoDTO
                        {
                            idAsiento = a.idAsiento,
                            letraColumna = a.letraColumna,
                            estado = a.estado
                        }).ToList()


                    };
                    listaFilas.Filas.Add(filaDTO);
                }
                return Result<ListaFilasAsientosDTO>.Exito(listaFilas);
            }
        }
    }
}
