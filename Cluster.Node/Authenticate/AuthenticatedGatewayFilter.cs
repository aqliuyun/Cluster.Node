using Cluster.Node.Connection;
using Cluster.Node.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Authenticate
{
    public class AuthenticatedGatewayFilter : IGatewayFilter
    {
        private readonly IAuthenticateService authenticateService;

        public AuthenticatedGatewayFilter(IAuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            IAuthenticatedConnectionToken authtoken = token as IAuthenticatedConnectionToken;
            var list = new List<ClusterNode>();
            foreach (var item in nodes)
            {
                if(this.authenticateService.Authenticate(item, authtoken))
                {
                    list.Add(item);
                }
            }            
            return list;
        }
    }
}
