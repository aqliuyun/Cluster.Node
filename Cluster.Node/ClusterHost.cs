using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterHost
    {
        private IServiceCollection collection;
        private IServiceProvider serviceProvider;
        private Timer heartbeat;
        public ClusterNode CurrentNode { get; private set; }
        private ClusterContext context;
        public ClusterHost()
        {
            collection = new ServiceCollection();
            context = new ClusterContext();
            collection.AddSingleton<ClusterContext>(context);
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
            CurrentNode = new ClusterNode()
            {
                Address = serviceProvider.GetService<HostOptions>().Address,
                LastActiveTime = DateTime.UtcNow
            };
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
    }
}
