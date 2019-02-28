using Cluster.Node.Connection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class SmartConnectionFactory : IClusterConnectionFactory
    {
        private ClusterContext context;
        public SmartConnectionFactory(ClusterContext context)
        {
            this.context = context;
        }

        public IClusterConnection Get()
        {            
            return new SmartConnection(context);
        }
    }
}
