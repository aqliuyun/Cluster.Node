using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;
using Cluster.Node.Extension;

namespace Cluster.Node.Wcf
{
    public class WcfClient : ClusterClient
    {        
        public WcfClient():base()
        {
            collection.AddSingleton<WcfGatewayFilter>();
        }

        public IConnectionToken Register<T>(string endpointConfigName) where T : class
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var token = new ServiceTypeToken<T>();
            var connection = connectionManage.GetConnection(token);
            ((IWcfConnection)connection).Bind(endpointConfigName);
            return token;
        }

        public T Service<T>() where T : class
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceTypeToken<T>());
            return ((IWcfConnection)connection).As<T>();
        }

        public T Service<T>(IConnectionToken token) where T : class
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token);
            return ((IWcfConnection)connection).As<T>();
        }

        public override void Start()
        {
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<WcfGatewayFilter>());
            base.Start();
        }
    }
}
