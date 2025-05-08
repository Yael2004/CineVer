using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class Sucursal
    {
        public string NombreSucursal { get; set; }
        public char CodigoPosta {  get; set; }
        public string Estado {  get; set; }
        public string Ciudad { get; set; }
        public string Calle {  get; set; }
        public int Numero {  get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }
}
