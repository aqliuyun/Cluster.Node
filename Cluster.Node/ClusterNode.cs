using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterNode
    {
        public string Version { get; set; }

        public string Address { get; set; }

        public DateTime LastActiveTime { get; set; }

        public int NoReply { get; set; }

        public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();
    }
}
