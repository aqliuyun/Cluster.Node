﻿using Cluster.Node.Connection;
using Cluster.Node.Filter;
using Cluster.Node.LoadBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.gRPC
{
    public class GrpcGatewayFilter : IGatewayFilter
    {
        private ConnectionContext context;
        public GrpcGatewayFilter(ConnectionContext context)
        {
            this.context = context;
        }

        public List<ClusterNode> Filter(IConnectionToken token, List<ClusterNode> nodes)
        {
            var list = new List<ClusterNode>();
            foreach (var node in nodes)
            {
                if (node.Details.TryGetValue("name", out string name))
                {
                    if (name.Equals(context.ConnectedServiceName))
                    {
                        list.Add(node);
                    }
                }
            }
            return list;
        }
    }
}
