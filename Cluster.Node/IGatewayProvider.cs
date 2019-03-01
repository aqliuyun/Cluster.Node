using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public interface IGatewayProvider
    {
        Task Init();

        List<string> GetGateways();

        ClusterNode GetClusterNode(string gateway);
    }
}
