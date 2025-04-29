using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class SucursalConsultada
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Numero { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }
    }
}
