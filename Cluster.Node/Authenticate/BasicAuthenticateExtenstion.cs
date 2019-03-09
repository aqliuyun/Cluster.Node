using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;
using Microsoft.Extensions.DependencyInjection;

namespace Cluster.Node.Authenticate
{
    public static class BasicAuthenticateExtenstion
    {
        public static ClusterClient UseBasicAuthenticate(this ClusterClient cluster)
        {
            cluster.ConfigureServices(x => {
                x.AddSingleton<BasicAuthenticateGatewayFilter>();
                x.AddSingleton<IAuthenticateService,BasicAuthenticateService>();
            });
            cluster.OnStart += (x) =>
            {
                var gatewayPipeline = x.GetService<IGatewayPipeline>();
                gatewayPipeline.RemoveFilter(x.GetService<NoneAuthenticateGatewayFilter>());
                gatewayPipeline.AddFilters(x.GetService<BasicAuthenticateGatewayFilter>());
            };
            return cluster;
        }

        public static ClusterHost UseBasicAuthenticate(this ClusterHost cluster,string token)
        {
            cluster.CurrentNode.Details.Add("Authorization", token);
            return cluster;
        }
    }
}
