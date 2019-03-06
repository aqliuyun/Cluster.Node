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
        protected ClusterContext context;
        protected string gateway;
        public int RetryTimes { get; set; }
        public Action<object> OnConnected;
        public Action<string> OnDisconnected;
        public ClusterConnection(ClusterContext context)
        {
            this.context = context;
        }

        public void UseGateway(string gateway)
        {
            this.gateway = gateway;
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
