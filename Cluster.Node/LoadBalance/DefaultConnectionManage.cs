using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;

namespace Cluster.Node.LoadBalance
{
    public class DefaultConnectionManage : IConnectionManage
    {
        private IClusterConnectionFactory factory;

        public DefaultConnectionManage(IClusterConnectionFactory factory)
        {
            this.factory = factory;
        }

        public IClusterConnection GetConnection()
        {
            return factory.Get();
        }
    }
}
