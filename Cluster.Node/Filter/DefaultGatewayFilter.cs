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
        private ConnectionContext _context;
        public DefaultGatewayFilter(ConnectionContext context)
        {
            this._context = context;
        }

        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            var list = new List<ClusterNode>();
            foreach (var node in nodes)
            {
                if(!_context.BlackList.Contains(node))
                {
                    list.Add(node);
                }
            }
            return list;
        }
    }
}
