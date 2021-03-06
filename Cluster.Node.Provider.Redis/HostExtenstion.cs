﻿using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Provider.Redis
{
    public static class HostExtenstion
    {
        public static ClusterHost UseRedisClusterProvider(this ClusterHost host, Action<RedisOptions> configuration)
        {
            return host.ConfigureServices(services =>
            {
                var options = new RedisOptions();
                configuration?.Invoke(options);
                services.AddSingleton(options).AddRedis();
            });
        }

        private static IServiceCollection AddRedis(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(context =>
            {
                return ConnectionMultiplexer.Connect(context.GetService<RedisOptions>().ConnectionString);
            })
                .AddSingleton<IClusterNodeProvider, ClusterNodeProvider>();
            return services;
        }
    }
}
