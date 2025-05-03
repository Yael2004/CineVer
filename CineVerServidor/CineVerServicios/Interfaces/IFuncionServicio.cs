using CineVerServicios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    
    internal interface IFuncionServicio
    {
        [OperationContract]
        ListaFuncionesDTO ObtenerFuncionesPorPeliculaYFecha(int idPelicula, DateTime fecha);
        [OperationContract]
        ListaFuncionesDTO ObtenerFuncionesPorFecha(DateTime fecha);
        [OperationContract]
        ListaFuncionesDTO ObtenerFuncionesPorFechaYSala(int idSala, DateTime fecha);
        [OperationContract]
        string AgregarFuncion(FuncionDTO funcion);
        [OperationContract]
        string EditarFUncion(FuncionDTO funcionOriginal, FuncionDTO funcionEditada);
        [OperationContract]
        string EliminarFuncion(FuncionDTO funcion);

    }
}
