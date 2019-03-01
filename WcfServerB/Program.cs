using Cluster.Node;
using Cluster.Node.Wcf;
using Cluster.Node.Provider.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfServerB
{
    class Program
    {
        static void Main(string[] args)
        {
            var cluster = new ClusterHost();
            cluster.UseConfig<ClusterOptions>(new ClusterOptions()
            {
                ClusterID = "test"
            });
            cluster.UseRedisClusterProvider(x =>
            {
                x.ConnectionString = "localhost:7000,localhost:7001,localhost:7002,syncTimeout=30000,asyncTimeout=30000,allowAdmin=True,connectTimeout=5000,responseTimeout=5000,password=rdc!234";
            });
            cluster.StartWcfService();
            cluster.Start();
            Console.WriteLine("start");
            Console.ReadLine();
            cluster.StopWcfService();
        }
    }
}
