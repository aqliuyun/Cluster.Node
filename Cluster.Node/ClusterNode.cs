using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterNode:IEquatable<ClusterNode>
    {
        public ClusterNode() { }

        public string NodeId { get; set; } = Guid.NewGuid().ToString("N");

        public string Version { get; set; }

        public string Address { get; set; }

        public DateTime LastActiveTime { get; set; }

        public int NoReply { get; set; }

        public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();

        public bool Equals(ClusterNode other)
        {
            return this.NodeId.Equals(other?.NodeId);
        }

        public override int GetHashCode()
        {
            return this.NodeId.GetHashCode();
        }
    }
}
