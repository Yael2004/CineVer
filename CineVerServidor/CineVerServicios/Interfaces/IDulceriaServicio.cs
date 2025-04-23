using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CineVerServicios.Interfaces
{
    [ServiceContract]
    public interface IDulceriaServicio
    {
        [OperationContract]
        string AgregarProductoDulceria();
    }
}
