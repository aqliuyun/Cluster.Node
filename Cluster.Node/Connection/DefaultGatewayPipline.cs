using Cluster.Node.Authenticate;
using Cluster.Node.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class DefaultGatewayPipline : IGatewayPipeline
    {
        private List<IGatewayFilter> filters = new List<IGatewayFilter>();
        private ConnectionContext context;
        public DefaultGatewayPipline(ConnectionContext context)
        {
            this.context = context;
        }

        public List<ClusterNode> GetAvaliableNodes(IConnectionToken token)
        {
            var nodes = context.ClusterNodes;
            var isAuthenticated = token is IAuthenticatedConnectionToken;
            foreach (var filter in filters)
            {
                if (filter is NoneAuthenticateGatewayFilter && isAuthenticated) {
                    continue;
                }
                nodes = filter.Filter(token, nodes);
            }
            return nodes;
        }

        public void AddFilters(params IGatewayFilter[] filter)
        {
            filters.AddRange(filter);
        }

        public void RemoveFilter(IGatewayFilter filter)
        {
            filters.Remove(filter);
        }

        public void ClearFilters()
        {
            filters.Clear();
        }
    }
}
