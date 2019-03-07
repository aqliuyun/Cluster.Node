using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.gRPC
{
    public static class HostExtension
    {
        private static Server server;

        public static void StartgRPCService<T>(this ClusterHost cluster, T t) where T : ServerServiceDefinition
        {
            var addr = new Uri(cluster.GetConfig<HostOptions>().Address);
            server = new Server
            {
                Services = { t },
                Ports = { new ServerPort(addr.Host, addr.Port, ServerCredentials.Insecure) }
            };
            server.Start();
        }

        public static void StopWcfService(this ClusterHost cluster)
        {
            server.ShutdownAsync().Wait();
        }
    }
}
