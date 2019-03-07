using Cluster.Node.Connection;
using Cluster.Node.Filter;
using Cluster.Node.LoadBalance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class WcfGatewayFilter : IGatewayFilter
    {
        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            var list = new List<ClusterNode>();
            foreach (var node in nodes)
            {
                if (node.Details.TryGetValue("contract", out string supports))
                {
                    if(supports.IndexOf(token.Name()) >= 0)
                    {
                        list.Add(node);
                    }
                }
            }
            return list;
        }
    }
}
