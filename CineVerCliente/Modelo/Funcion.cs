using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class Funcion
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public decimal Precio { get; set; }
        public int IdSala { get; set; }
    }
}
