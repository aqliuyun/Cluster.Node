using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public static class HostExtension
    {
        internal static List<ServiceHost> hosts;

        public static void StartWcfService(this ClusterHost cluster)
        {
            hosts = new List<ServiceHost>();
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            var groups = (ServiceModelSectionGroup)config.GetSectionGroup("system.serviceModel");
            var routes = new Dictionary<string, string>();
            foreach (ServiceElement service in groups.Services.Services)
            {
                ServiceHost host = new ServiceHost(Assembly.GetEntryAssembly().GetType(service.Name));
                host.Open();
                hosts.Add(host);              
                foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
                {
                    routes.Add(endpoint.Contract.ContractType.FullName, endpoint.ListenUri.ToString());
                }
            }
            cluster.UseConfig<HostOptions>(new HostOptions()
            {
                Address = hosts.FirstOrDefault().BaseAddresses.FirstOrDefault()?.ToString()
            });
            cluster.CurrentNode.Details.Add("contract", JsonConvert.SerializeObject(routes));
        }

        public static void StartWcfService<T>(this ClusterHost cluster)
        {
            hosts = new List<ServiceHost>();
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            var groups = (ServiceModelSectionGroup)config.GetSectionGroup("system.serviceModel");
            var routes = new Dictionary<string, string>();
            foreach (ServiceElement service in groups.Services.Services)
            {
                ServiceHost host = new ServiceHost(typeof(T).Assembly.GetType(service.Name));
                host.Open();
                hosts.Add(host);
                foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
                {
                    routes.Add(endpoint.Contract.ContractType.FullName, endpoint.ListenUri.ToString());
                }
            }
            cluster.CurrentNode.Details.Add("contract", JsonConvert.SerializeObject(routes));
        }

        public static void StopWcfService(this ClusterHost cluster)
        {
            if (hosts == null) return;
            foreach (var host in hosts)
            {
                host.Close();
            }
        }
    }
}
