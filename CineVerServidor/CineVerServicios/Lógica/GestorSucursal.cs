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
                horaCierre = sucursalDTO.HoraCierre
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
                        HoraCierre = (TimeSpan)sucursal.horaCierre

                    };
                    listaSucursales.Sucursales.Add(sucursalDTO);
                }
                return Result<ListaSucursalesDTO>.Exito(listaSucursales);
            }
        }
    }
}
