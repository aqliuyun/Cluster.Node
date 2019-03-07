using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Filter
{
    public interface IGatewayFilter
    {
        List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes);
    }
}
