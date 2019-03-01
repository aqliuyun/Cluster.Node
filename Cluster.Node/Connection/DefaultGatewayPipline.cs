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
        private ClusterContext context;
        public DefaultGatewayPipline(ClusterContext context)
        {
            this.context = context;
        }

        public List<ClusterNode> GetAvaliableNodes<T>()
        {
            var nodes = context.ClusterNodes;
            foreach (var filter in filters)
            {
                nodes = filter.Filter<T>(nodes);
            }
            return nodes;
        }

        public void AddFilters(params IGatewayFilter[] filter)
        {
            filters.AddRange(filter);
        }

        public void ClearFilters()
        {
            filters.Clear();
        }
    }
}
