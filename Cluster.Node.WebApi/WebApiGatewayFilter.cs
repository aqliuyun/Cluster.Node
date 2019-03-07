using Cluster.Node.Connection;
using Cluster.Node.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.WebApi
{
    public class WebApiGatewayFilter : IGatewayFilter
    {

        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            var list = new List<ClusterNode>();
            foreach (var node in nodes)
            {
                if (node.Details.TryGetValue("name", out string name))
                {
                    if (name.Equals(token.Name()))
                    {
                        list.Add(node);
                    }
                }
            }
            return list;
        }
    }
}
