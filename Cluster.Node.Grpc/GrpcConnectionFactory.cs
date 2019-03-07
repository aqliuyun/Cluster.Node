using Cluster.Node.Connection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.gRPC
{
    public class GrpcConnectionFactory : IClusterConnectionFactory
    {
        private IGatewayProvider gatewayProvider;
        private ClusterContext context;
        public GrpcConnectionFactory(IGatewayProvider gatewayProvider,ClusterContext context)
        {
            this.gatewayProvider = gatewayProvider;
            this.context = context;
        }

        public IClusterConnection Get(string gateway)
        {            
            var connection = new GrpcConnection(this.gatewayProvider, context);
            connection.UseGateway(gateway);            
            return connection;
        }
    }
}
