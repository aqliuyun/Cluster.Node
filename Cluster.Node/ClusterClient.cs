using Cluster.Node.Connection;
using Cluster.Node.Extension;
using Cluster.Node.LoadBalance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterClient : IDisposable
    {
        protected IServiceCollection collection;
        protected IServiceProvider serviceProvider;
        private Timer heartbeat;
        protected ClusterContext context;
        public Action OnReady;
        public ClusterClient()
        {
            collection = new ServiceCollection();
            context = new ClusterContext();
            collection.AddSingleton<ClusterContext>(context);
            collection.AddSingleton<IGatewaySelector, RandomSelector>();
            collection.AddSingleton<IConnectionManage, DefaultConnectionManage>();
            collection.AddSingleton<IGatewayProvider, GatewayProvider>();
            collection.AddSingleton<IGatewayPipeline, DefaultGatewayPipline>();
        }

        public ClusterClient ConfigureServices(Action<IServiceCollection> config)
        {
            config?.Invoke(collection);
            return this;
        }

        public ClusterClient UseConfig<T>(T config) where T : class
        {
            collection.AddSingleton(config);
            return this;
        }

        public void Build()
        {
            serviceProvider = collection.BuildServiceProvider();
        }

        public virtual void Start()
        {
            Task.Factory.StartNew(async () =>
            {
                await serviceProvider.GetService<IGatewayProvider>().Init();
                context.ClusterNodes = await serviceProvider.GetService<IClusterNodeProvider>().GetClusterNodeList();
                heartbeat = new Timer(async (x) =>
                {
                    context.ClusterNodes = await serviceProvider.GetService<IClusterNodeProvider>().GetClusterNodeList();
                }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
                OnReady?.Invoke();
            });
        }

        public void Dispose()
        {
        }
    }
}
