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
        public double InicioDia { get; set; }
        public double FinDia { get; set; }
        public double VentasTotales { get; set; }
    }
}
