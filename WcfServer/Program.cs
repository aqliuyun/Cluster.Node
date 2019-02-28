using Cluster.Node;
using Cluster.Node.Provider.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WcfServer
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                
            }
            
            // Create the ServiceHost.
            using (ServiceHost host = new ServiceHost(typeof(Test)))
            {
                host.Open();
                var cluster = new ClusterHost();
                cluster.UseConfig<HostOptions>(new HostOptions()
                {
                    Address = host.BaseAddresses[0].ToString()
                });
                cluster.UseConfig<ClusterOptions>(new ClusterOptions()
                {
                    ClusterID = "test"
                });
                cluster.UseRedisClusterProvider(x =>
                {
                    x.ConnectionString = "localhost:7000,localhost:7001,localhost:7002,syncTimeout=30000,asyncTimeout=30000,allowAdmin=True,connectTimeout=5000,responseTimeout=5000,password=rdc!234";
                });
                cluster.Start();
                Console.WriteLine("The service is ready at {0}", host.BaseAddresses[0]);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}
