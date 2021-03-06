﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace Cluster.Node.LoadBalance
{
    public class RandomSelector : IGatewaySelector
    {
        protected Random random;
        protected ConnectionContext context;
        protected IClusterConnectionFactory connectionFactory;
        public RandomSelector(IClusterConnectionFactory connectionFactory, ConnectionContext context)
        {
            this.connectionFactory = connectionFactory;
            random = new Random();
            this.context = context;
        }

        public virtual string GetGateway(List<ClusterNode> nodes)
        {
            var gateways = nodes.Select(x => x.Address).ToList();
            if (gateways.Count == 0) return null;
            return gateways[random.Next(gateways.Count)];
        }
    }
}
