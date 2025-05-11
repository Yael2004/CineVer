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
            using (ServiceHost host4 = new ServiceHost(typeof(EmpleadoServicio)))
            using (ServiceHost host5 = new ServiceHost(typeof(SocioServicio)))
            using (ServiceHost host6 = new ServiceHost(typeof(CuentaFidelidadServicio)))
            //using (ServiceHost host7 = new ServiceHost(typeof(GastoServicio)))
            using (ServiceHost host8 = new ServiceHost(typeof(VentaServicio)))
            using (ServiceHost host9 = new ServiceHost(typeof(CorteCajaServicio)))
            {
                try
                {
                    host.Open();
                    host2.Open();
                    host3.Open();
                    host4.Open();
                    host5.Open();
                    host6.Open();
                    //host7.Open();
                    host8.Open();
                    host9.Open();
                    Console.WriteLine("Servicio del CineVer en ejecucion...");
                    Console.ReadLine();
                    host.Close();
                    host2.Close();
                    host3.Close();
                    host4.Close();
                    host5.Close();
                    host6.Close();
                    //host7.Close();
                    host8.Close();
                    host9.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar el servicio: " + ex.Message);
                    host.Abort();
                    host2.Abort();
                    host3.Abort();
                    host4.Abort();
                    host5.Abort();
                    host6.Abort();
                    //host7.Abort();
                    host8.Abort();
                    host9.Abort();
                }
            }
        }
    }
}
