using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterHost : IDisposable
    {
        private IServiceCollection collection;
        private IServiceProvider serviceProvider;
        private Timer heartbeat;
        public ClusterNode CurrentNode { get; private set; }
        private ClusterContext context;

        public void UseRedisClusterProvider(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public ClusterHost()
        {
            collection = new ServiceCollection();
            context = new ClusterContext();
            CurrentNode = new ClusterNode()
            {                
                LastActiveTime = DateTime.UtcNow
            };
            collection.AddSingleton<ClusterContext>(context);
            collection.AddSingleton<ClusterNode>(CurrentNode);
            collection.AddSingleton<IGatewayProvider, GatewayProvider>();
        }

        public ClusterHost ConfigureServices(Action<IServiceCollection> services)
        {
            services?.Invoke(collection);
            return this;
        }

        public ClusterHost UseConfig<T>(T config) where T : class
        {
            collection.AddSingleton(config);
            return this;
        }

        public void Start()
        {
            serviceProvider = collection.BuildServiceProvider();
            CurrentNode.Address = serviceProvider.GetService<HostOptions>().Address;
            Task.Factory.StartNew(async () =>
            {                
                await serviceProvider.GetService<IGatewayProvider>().Init();
                await serviceProvider.GetService<IClusterNodeProvider>().RegisterClusterNode(CurrentNode);
                await serviceProvider.GetService<IClusterNodeProvider>().GetClusterNodeList();
                heartbeat = new Timer(async (x) =>
                {
                    CurrentNode.LastActiveTime = DateTime.UtcNow;
                    await serviceProvider.GetService<IClusterNodeProvider>().UpdateClusterNode(CurrentNode);
                }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
            });
        }

        public void Dispose()
        {
        }
    }
}
