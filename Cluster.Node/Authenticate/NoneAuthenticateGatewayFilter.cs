using Cluster.Node.Connection;
using Cluster.Node.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Authenticate
{
    public class NoneAuthenticateGatewayFilter : IGatewayFilter
    {
        private readonly IAuthenticateService authenticateService;

        public NoneAuthenticateGatewayFilter(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            var list = new List<ClusterNode>();
            foreach (var node in nodes)
            {
                if (!node.Details.ContainsKey("Authorization"))
                {
                    list.Add(node);
                }
            }
            return list;
        }
    }
}
