using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CineVerServicios.Interfaces;

namespace CineVerServicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DulceriaServicio : IDulceriaServicio
    {
        public string AgregarProductoDulceria()
        {
            return "Hola gente, soy el server";
        }
    }
}
