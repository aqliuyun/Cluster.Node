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
    public class ClusterClient
    {
        private IServiceCollection collection;
        private IServiceProvider serviceProvider;
        private Timer heartbeat;
        private ClusterContext context;
        public Action OnReady;
        public ClusterClient()
        {
            collection = new ServiceCollection();
            context = new ClusterContext();            
            collection.AddSingleton<ClusterContext>(context);
            collection.AddSingleton<IGatewaySelector, RandomSelector>();
            collection.AddSingleton<IConnectionManage, DefaultConnectionManage>();
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

        public void Start()
        {
            serviceProvider = collection.BuildServiceProvider();
            Task.Factory.StartNew(async () => {
                await serviceProvider.GetService<IGatewayProvider>().Init();
                context.Gateways = await serviceProvider.GetService<IGatewayProvider>().GetGateways();                
                heartbeat = new Timer(async (x) => {
                    context.Gateways = await serviceProvider.GetService<IGatewayProvider>().GetGateways();
                }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
                OnReady?.Invoke();
            });            
        }

        public IClusterConnection GetConnection() {
            return serviceProvider.GetService<IConnectionManage>().GetConnection();
        }
    }
}
