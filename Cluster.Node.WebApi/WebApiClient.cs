using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace Cluster.Node.WebApi
{
    public class WebApiClient : ClusterClient
    {
        public WebApiClient() : base()
        {
            collection.AddSingleton<WebApiGatewayFilter>();
        }

        public Task<T> Get<T>(IConnectionToken token, string url)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token) as IWebApiConnection;
            return connection.GetAsync<T>(url);
        }

        public Task<T> Post<T>(IConnectionToken token, string url, object data)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token) as IWebApiConnection;
            return connection.PostAsync<T>(url, data);
        }

        public Task<T> Put<T>(IConnectionToken token, string url, object data)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token) as IWebApiConnection;
            return connection.PutAsync<T>(url, data);
        }

        public Task<T> Delete<T>(IConnectionToken token, string url)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(token) as IWebApiConnection;
            return connection.DeleteAsync<T>(url);
        }

        public Task<T> Get<T>(string url)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceNameToken(context.ConnectedServiceName)) as IWebApiConnection;
            return connection.GetAsync<T>(url);
        }

        public Task<T> Post<T>(string url,object data)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceNameToken(context.ConnectedServiceName)) as IWebApiConnection;
            return connection.PostAsync<T>(url,data);
        }

        public Task<T> Put<T>(string url, object data)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceNameToken(context.ConnectedServiceName)) as IWebApiConnection;
            return connection.PutAsync<T>(url, data);
        }

        public Task<T> Delete<T>(string url)
        {
            var connectionManage = this.serviceProvider.GetService<IConnectionManage>();
            var connection = connectionManage.GetConnection(new ServiceNameToken(context.ConnectedServiceName)) as IWebApiConnection;
            return connection.DeleteAsync<T>(url);
        }

        public override void Start()
        {
            this.serviceProvider.GetService<IGatewayPipeline>().AddFilters(this.serviceProvider.GetService<WebApiGatewayFilter>());
            base.Start();
        }

    }
}
