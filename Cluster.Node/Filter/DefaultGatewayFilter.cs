using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Filter
{
    public class DefaultGatewayFilter : IGatewayFilter
    {
        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            return nodes;
        }
    }
}
