using CineVerCliente.FuncionServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Helpers
{
    public class FuncionVista
    {
        public FuncionDTO Funcion { get; set; }

        public string HoraInicioTexto => Funcion.horaInicio.HasValue
    ? Funcion.horaInicio.Value.ToString(@"hh\:mm")
    : "Sin hora";


        // Si necesitas exponer más propiedades del DTO directamente:
        public TimeSpan HoraInicio => Funcion.horaInicio.Value;

        // Puedes agregar más wrappers según sea necesario
    }

}
