using System;
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
        private IServiceProvider serviceProvider;
        private Random random;
        private ClusterContext context;
        private IClusterConnectionFactory connectionFactory;
        public RandomSelector(IClusterConnectionFactory connectionFactory, ClusterContext context)
        {
            this.connectionFactory = connectionFactory;
            random = new Random();
            this.context = context;
        }

        public virtual string GetGateway()
        {
            var gateways = context.Gateways;
            return gateways[random.Next(gateways.Count)];
        }
    }
}
