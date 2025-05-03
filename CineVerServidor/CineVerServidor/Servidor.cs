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
            using (ServiceHost host5 = new ServiceHost(typeof(AsientoServicio)))
            using (ServiceHost host6 = new ServiceHost(typeof(FilaServicio)))
            using (ServiceHost host7 = new ServiceHost(typeof(FuncionServicio)))
            using (ServiceHost host8 = new ServiceHost(typeof(SalaServicio)))
            {
                try
                {
                    host.Open();
                    host2.Open();
                    host3.Open();
                    
                    host5.Open();
                    host6.Open();
                    host7.Open();
                    host8.Open();
                    Console.WriteLine("Servicio del CineVer en ejecucion...");
                    Console.ReadLine();
                    host.Close();
                    host2.Close();
                    host3.Close();
                    
                    host5.Close();
                    host6.Close();
                    host7.Close();
                    host8.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar el servicio: " + ex.Message);
                    host.Abort();
                    host2.Abort();
                    host3.Abort();
                    
                    host5.Abort();
                    host6.Abort();
                    host7.Abort();
                    host8.Abort();
                }
            }
        }
    }
}
