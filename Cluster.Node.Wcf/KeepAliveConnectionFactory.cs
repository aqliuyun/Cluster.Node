using Cluster.Node.Connection;
using Cluster.Node.LoadBalance;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class KeepAliveConnectionFactory : IClusterConnectionFactory
    {
        public ConcurrentDictionary<string, IClusterConnection> _address = new ConcurrentDictionary<string, IClusterConnection>();
        private IGatewaySelector gatewaySelector;
        private ClusterContext context;
        public KeepAliveConnectionFactory(ClusterContext context)
        {
            this.context = context;
        }

        public IClusterConnection Get()
        {
            var key = gatewaySelector.GetGateway();
            IClusterConnection connection;
            if (this._address.TryGetValue(key, out connection))
            {
                return connection;
            }
            connection = new KeepAliveConnection(context, key);
            this._address.TryAdd(key, connection);
            return connection;
        }
    }
}
