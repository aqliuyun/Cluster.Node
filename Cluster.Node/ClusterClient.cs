using Cluster.Node.Authenticate;
using Cluster.Node.Connection;
using Cluster.Node.Extension;
using Cluster.Node.Filter;
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
        protected ConnectionContext context;
        public Action OnReady;
        public Action<IServiceProvider> OnStart;

        public ClusterClient()
        {
            collection = new ServiceCollection();
            context = new ConnectionContext();
            collection.AddSingleton<ConnectionContext>(context);
            collection.AddSingleton<IGatewaySelector, RandomSelector>();
            collection.AddSingleton<IConnectionManage, DefaultConnectionManage>();
            collection.AddSingleton<IGatewayProvider, GatewayProvider>();
            collection.AddSingleton<IGatewayPipeline, DefaultGatewayPipline>();
            collection.AddSingleton<DefaultGatewayFilter>();
            collection.AddSingleton<NoneAuthenticateGatewayFilter>();
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
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<DefaultGatewayFilter>());
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<NoneAuthenticateGatewayFilter>());
            this.OnStart?.Invoke(this.serviceProvider);                        
            Task.Factory.StartNew(async () =>
            {
                context.ConnectedServiceName = serviceProvider.GetService<ClientOptions>().ServiceName;                
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
