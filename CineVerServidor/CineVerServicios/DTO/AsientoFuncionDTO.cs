using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.DTO
{
    public class AsientoFuncionDTO
    {
        public int idAsiento { get; set; }
        public int idFila { get; set; }
        public string letraColumna { get; set; }
        public string estado { get; set; }
    }

}
