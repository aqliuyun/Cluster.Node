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
            using (var cluster = new ServiceManager())
            {
                try
                {
                    cluster.ConfigureServices(x =>
                    {
                        x.AddSingleton<IClusterConnectionFactory, WcfConnectionFactory>();
                    });
                    cluster.UseConfig<ClusterOptions>(new ClusterOptions()
                    {
                        ClusterID = "test"
                    });
                    cluster.OnReady += () =>
                    {
                        try
                        {
                            cluster.Register<ITestB>("WcfClient");
                            Console.WriteLine(cluster.Service<ITestB>().Get());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    };
                    cluster.UseRedisClusterProvider(x =>
                    {
                        x.ConnectionString = "localhost:7000,localhost:7001,localhost:7002,syncTimeout=30000,asyncTimeout=30000,allowAdmin=True,connectTimeout=5000,responseTimeout=5000,password=rdc!234";
                    });
                    cluster.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }
            Console.ReadKey();
        }
    }
}
