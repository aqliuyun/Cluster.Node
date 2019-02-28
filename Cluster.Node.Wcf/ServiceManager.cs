using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class ServiceManager
    {
        private ClusterClient client;

        public ConcurrentDictionary<Type, IWcfConnection> Connections = new ConcurrentDictionary<Type, IWcfConnection>();

        public ServiceManager(ClusterClient client)
        {
            this.client = client;
        }

        public T Register<T>(string endpointConfigName) where T : class
        {
            var connection = ((IWcfConnection)client.GetConnection()).Bind(endpointConfigName);
            Connections.TryAdd(typeof(T), connection);
            return connection.As<T>();
        }

        public T Service<T>() where T : class
        {
            if(Connections.TryGetValue(typeof(T), out IWcfConnection connection))
            {
                return connection.As<T>();
            }
            return default(T);
        }
    }
}
