using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.WebApi
{
    public class WebApiConnectionFactory : IClusterConnectionFactory
    {
        private IGatewayProvider _gatewayProvider;
        private ClusterContext _context;
        public WebApiConnectionFactory(IGatewayProvider gatewayProvider, ClusterContext context)
        {
            this._gatewayProvider = gatewayProvider;
            this._context = context;
        }

        public IClusterConnection Get(string gateway)
        {
            var connection = new WebApiConnection(this._gatewayProvider, _context);
            connection.UseGateway(gateway);
            return connection;
        }
    }
}
