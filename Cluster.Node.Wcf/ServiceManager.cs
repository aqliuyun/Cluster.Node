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
    public class ServiceManager : ClusterClient
    {        
        public ServiceManager():base()
        {
            collection.AddSingleton<WcfGatewayFilter>();
        }

        public void Register<T>(string endpointConfigName) where T : class
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection<T>();
            ((IWcfConnection)connection).Bind(endpointConfigName);
        }

        public T Service<T>() where T : class
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection<T>();
            return ((IWcfConnection)connection).As<T>();
        }

        public override void Start()
        {
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<WcfGatewayFilter>());
            base.Start();
        }
    }
}
