using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class HaConnection : ClusterConnection
    {

        private int _index = 0;
        
        public HaConnection(ClusterContext context):base(context)
        {
            var rand = new Random();
            _index = rand.Next(context.Gateways.Count);
            this.gateway = context.Gateways[_index];
        }

        public virtual bool ChooseNextGateway()
        {
            _index++;
            gateway = context.Gateways[_index % context.Gateways.Count];
            return this.Connect(gateway);
        }
    }
}
