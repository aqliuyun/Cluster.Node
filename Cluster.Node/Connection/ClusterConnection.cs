using Cluster.Node.LoadBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ClusterConnection : IClusterConnection
    {
        private IGatewaySelector gatewaySelector;
        private IClusterConnectionFactory factory;
        protected ClusterContext context;
        protected string gateway;

        public ClusterConnection(ClusterContext context)
        {
            this.context = context;
        }

        public bool Connect()
        {
            return this.Connect(gateway);
        }

        public virtual bool Connect(string gateway)
        {
            return true;
        }       

        public void Dispose()
        {            
        }
    }
}
