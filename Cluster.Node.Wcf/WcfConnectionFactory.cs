using Cluster.Node.Connection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class WcfConnectionFactory : IClusterConnectionFactory
    {
        private IGatewayProvider gatewayProvider;
        private ConnectionContext context;
        public WcfConnectionFactory(IGatewayProvider gatewayProvider,ConnectionContext context)
        {
            this.gatewayProvider = gatewayProvider;
            this.context = context;
        }

        public IClusterConnection Get(string gateway)
        {            
            var connection = new WcfConnection(this.gatewayProvider, context);
            connection.UseGateway(gateway);            
            return connection;
        }
    }
}
