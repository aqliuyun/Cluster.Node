using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class GatewayProvider : IGatewayProvider
    {
        private IClusterNodeProvider provider;
        private ClusterContext context;
      
        public GatewayProvider(IClusterNodeProvider provider,ClusterContext context)
        {
            this.provider = provider;
            this.context = context;
        }

        public List<string> GetGateways()
        {            
            var nodes = context.ClusterNodes;
            return nodes.Select(x => x.Address).ToList();
        }

        public ClusterNode GetClusterNode(string gateway)
        {       
            return context.ClusterNodes.Find(x => x.Address == gateway);
        }        

        public Task Init()
        {
            return Task.Factory.StartNew(async() => { context.ClusterNodes = await provider.GetClusterNodeList(); });
        }
    }
}
