using Cluster.Node;
using Cluster.Node.Extension;
using Cluster.Node.Wcf;
using Cluster.Node.Provider.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;
using Microsoft.Extensions.DependencyInjection;
using Cluster.Node.Connection;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceManager manager;
                var client = new ClusterClient();
                client.ConfigureServices(x =>
                {
                    x.AddSingleton<IClusterConnectionFactory, SmartConnectionFactory>();
                });
                client.UseConfig<ClusterOptions>(new ClusterOptions() {
                    ClusterID = "test"
                });
                client.OnReady += () =>
                {
                    try
                    {
                        manager = new ServiceManager(client);
                        manager.Register<ITest>("WcfClient");
                        Console.WriteLine(manager.Service<ITest>().Get());
                    }
                    catch(Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                };
                client.UseRedisClusterProvider(x =>
                {
                    x.ConnectionString = "localhost:7000,localhost:7001,localhost:7002,syncTimeout=30000,asyncTimeout=30000,allowAdmin=True,connectTimeout=5000,responseTimeout=5000,password=rdc!234";
                });
                client.Start();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
