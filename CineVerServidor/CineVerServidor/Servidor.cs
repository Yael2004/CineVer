using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CineVerServicios;

namespace CineVerServidor
{
    internal class Servidor
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DulceriaServicio)))
            using (ServiceHost host2 = new ServiceHost(typeof(PelículaServicio)))
            using (ServiceHost host3 = new ServiceHost(typeof(SucursalServicio)))
            using (ServiceHost host4 = new ServiceHost(typeof(VentaServicio)))
            {
                try
                {
                    host.Open();
                    host2.Open();
                    host3.Open();
                    host4.Open();
                    Console.WriteLine("Servicio del CineVer en ejecucion...");
                    Console.ReadLine();
                    host.Close();
                    host2.Close();
                    host3.Close();
                    host4.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar el servicio: " + ex.Message);
                    host.Abort();
                    host2.Abort();
                    host3.Abort();
                    host4.Abort();
                }
            }
        }
    }
}
