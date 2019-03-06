using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.WebApi
{
    public static class HostExtension
    {
        public static void EnableCluster(this ClusterHost cluster)
        {
            cluster.CurrentNode.Details.Add("name", cluster.GetConfig<ClusterOptions>().ServerName);
        }
    }
}
