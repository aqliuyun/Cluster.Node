using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ClusterConnectionFactory
    {
        private ConnectionContext context;
        public ClusterConnectionFactory(ConnectionContext context)
        {
            this.context = context;
        }

        public IClusterConnection Get(string gateway) {
            return new ClusterConnection(context);
        }
    }
}
