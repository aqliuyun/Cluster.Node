using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.LoadBalance;
using System.Collections.Concurrent;
using System.Threading;

namespace Cluster.Node.Connection
{
    public class DefaultConnectionManage : IConnectionManage
    {
        public ConcurrentDictionary<string, IClusterConnection> Connections = new ConcurrentDictionary<string, IClusterConnection>();
        private IClusterConnectionFactory factory;
        private IGatewayPipeline gatewayFilter;
        private IGatewaySelector gatewaySelector;
        private IGatewayProvider gatewayProvider;
        private IClusterNodeProvider clusterNodeProvider;
        private ClusterOptions options;
        public DefaultConnectionManage(IClusterConnectionFactory factory,IClusterNodeProvider clusterNodeProvider,IGatewayPipeline gatewayFilter,IGatewaySelector gatewaySelector,ClusterOptions options)
        {
            this.factory = factory;
            this.clusterNodeProvider = clusterNodeProvider;
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
            connection.OnRetryConnect += (gateway) =>
            {
                connection.AcquireLock();
                Task.Factory.StartNew(async () =>
                {
                    var node = this.gatewayProvider.GetClusterNode(gateway);
                    await this.clusterNodeProvider.UpdateClusterNode(node,(x)=> { x.NoReply++; });
                    await Task.Delay(TimeSpan.FromSeconds(options.RetryInterval)).ContinueWith(x =>
                    {
                        try
                        {
                            if (connection.RetryTimes > options.MaxRetryTimes)
                            {
                                Connections.TryRemove(typeof(T).FullName, out value);
                                return;
                            }                            
                            connection.UseGateway(gatewaySelector.GetGateway(gatewayFilter.GetAvaliableNodes<T>()));
                            connection.ReConnect();
                        }
                        catch { }
                        finally {
                            connection.ReleaseLock();
                        }
                    });
                });
            };
            return connection;
        }
    }
}
