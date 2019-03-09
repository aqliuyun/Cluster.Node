using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;
using Cluster.Node.Extension;
using Grpc.Core;

namespace Cluster.Node.gRPC
{
    public class GrpcClient : ClusterClient
    {        
        public GrpcClient():base()
        {
            collection.AddSingleton<GrpcGatewayFilter>();
        }

        public T Service<T>(IConnectionToken token) where T : ClientBase,new()
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token);
            return ((IGrpcConnection)connection).As<T>();
        }

        public T Service<T>() where T : ClientBase, new()
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceNameToken(context.ConnectedServiceName));
            return ((IGrpcConnection)connection).As<T>();
        }

        public override void Start()
        {
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<GrpcGatewayFilter>());
            base.Start();
        }
    }
}
