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
        private ClusterContext context;
        public ClusterConnectionFactory(ClusterContext context)
        {
            this.context = context;
        }

        public IClusterConnection Get() {
            return new ClusterConnection(context);
        }
    }
}
