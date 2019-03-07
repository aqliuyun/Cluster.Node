using Cluster.Node.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public interface IGatewayPipeline
    {
        List<ClusterNode> GetAvaliableNodes(IConnectionToken token);

        void AddFilters(params IGatewayFilter[] filter);

        void ClearFilters();
    }
}
