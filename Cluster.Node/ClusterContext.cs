using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterContext
    {
        public List<ClusterNode> ClusterNodes { get; set; }

        public string ServerName { get; set; }
    }
}
