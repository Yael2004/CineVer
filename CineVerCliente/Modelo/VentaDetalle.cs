using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerCliente.Modelo
{
    public class VentaDetalle
    {
        public int Dia { get; set; }
        public string Tipo { get; set; }
        public DateTime InicioDia { get; set; }
        public DateTime FinDia { get; set; }
        public decimal VentasTotales { get; set; }
        public int IdEmpleado { get; set; }
        public int IdSocio { get; set; }
        public int IdSucursal { get; set; }
        public decimal Total { get; set; }
        public string MetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public string FolioVenta { get; set; }
    }
}
