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
    internal class GestorAsiento
    {
        private AsientoDAO asientoDAO= new AsientoDAO();
        public GestorAsiento()
        {

        }
        public Result<ListaAsientosDTO> ObtenerAsientosPorFila(int idFila)
        {
            var asientos = asientoDAO.ObtenerAsientosDeFila(idFila);
            if (asientos.EsExitoso)
            {
                var listaAsientos = new ListaAsientosDTO();
                foreach(var asiento in asientos.Valor)
                {
                    var asientoLista = new AsientoDTO();
                    asientoLista.idAsiento = asiento.idAsiento;
                    asientoLista.idFila = asiento.idFila;
                    asientoLista.letraColumna = asiento.letraColumna;
                    asientoLista.estado = asiento.estado;
                    listaAsientos.Asientos.Add(asientoLista);
                }
                return Result<ListaAsientosDTO>.Exito(listaAsientos);
            }
            else
            {
                return Result<ListaAsientosDTO>.Fallo(asientos.Error);
            }
        }
        public Result<string> AgregarAsiento(AsientoDTO asiento)
        {
            var asientoInsertar = new Asiento();
            asientoInsertar.idAsiento = asiento.idAsiento;
            asientoInsertar.idFila = asiento.idFila;
            asientoInsertar.letraColumna = asiento.letraColumna;
            asientoInsertar.estado = asiento.estado;
            var result = asientoDAO.AgregarAsiento(asientoInsertar);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<string> EditarAsiento(AsientoDTO asientoEditado, AsientoDTO asientoOriginal)
        {
            Asiento asientoEditadoEntitie = new Asiento();
            asientoEditadoEntitie.idAsiento = asientoEditado.idAsiento;
            asientoEditadoEntitie.idFila = asientoEditado.idFila;
            asientoEditadoEntitie.letraColumna = asientoEditado.letraColumna;
            asientoEditadoEntitie.estado = asientoEditado.estado;
            Asiento asientoOriginalEntitie = new Asiento();
            asientoOriginalEntitie.idAsiento = asientoOriginal.idAsiento;
            asientoOriginalEntitie.idFila = asientoOriginal.idFila;
            asientoOriginalEntitie.letraColumna = asientoOriginal.letraColumna;
            asientoOriginalEntitie.estado = asientoOriginal.estado;
            var result = asientoDAO.EditarAsiento(asientoEditadoEntitie, asientoOriginalEntitie);
            if (result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor);
            }
            else
            {
                return Result<string>.Fallo(result.Error);
            }
        }
        public Result<string> EliminarAsiento(AsientoDTO asiento)
        {
            Asiento asientoEntitie = new Asiento();
            asientoEntitie.idAsiento = asiento.idAsiento;
            asientoEntitie.idFila = asiento.idFila;
            asientoEntitie.letraColumna = asiento.letraColumna;
            asientoEntitie.estado= asiento.estado;
            var result = asientoDAO.EliminarAsiento(asientoEntitie);
            if(result.EsExitoso)
            {
                return Result<string>.Exito(result.Valor) ;
            }
            else
            {
                return Result<string> .Fallo(result.Error);
            }
        }
        public Result<int> ObtenerIDAsiento(string letraColumna, int idFila)
        {
            var result = asientoDAO.ObtenerIdAsiento(idFila, letraColumna);
            if(result.EsExitoso)
            {
                return Result<int>.Exito(result.Valor) ;
            }
            else
            {
                return Result<int>.Fallo(result.Error);
            }
        }
    }
}
