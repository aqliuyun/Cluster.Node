using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.LoadBalance;
using System.Collections.Concurrent;

namespace Cluster.Node.Connection
{
    public class DefaultConnectionManage : IConnectionManage
    {
        public ConcurrentDictionary<string, IClusterConnection> Connections = new ConcurrentDictionary<string, IClusterConnection>();
        private IClusterConnectionFactory factory;
        private IGatewayPipeline gatewayFilter;
        private IGatewaySelector gatewaySelector;
        private ClusterOptions options;
        public DefaultConnectionManage(IClusterConnectionFactory factory,IGatewayPipeline gatewayFilter,IGatewaySelector gatewaySelector,ClusterOptions options)
        {
            this.factory = factory;
            this.gatewayFilter = gatewayFilter;
            this.gatewaySelector = gatewaySelector;
            this.options = options;
        }

        public IClusterConnection GetConnection<T>()
        {
            if (Connections.TryGetValue(typeof(T).FullName, out IClusterConnection value))
            {
                return value;
            }
            var nodes = gatewayFilter.GetAvaliableNodes<T>();
            var key = gatewaySelector.GetGateway(nodes);
            var connection = factory.Get(key) as ClusterConnection;
            connection.OnDisconnected += () =>
            {
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(options.RetryInterval).ContinueWith(x =>
                    {
                        try
                        {
                            if (connection.RetryTimes > options.MaxRetryTimes)
                            {
                                Connections.TryRemove(typeof(T).FullName,out value);
                                return;
                            }
                            connection.RetryTimes++;
                            connection.UseGateway(gatewaySelector.GetGateway(nodes));
                            connection.Connect();
                        }
                        catch { }
                    });
                });
            };
            return connection;
        }
    }
}
