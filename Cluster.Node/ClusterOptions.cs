using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterOptions
    {
        public string ClusterID { get; set; }

        public int MaxNoReply { get; set; } = DEFAULT_MAX_NO_REPLY;

        public const int DEFAULT_MAX_NO_REPLY = 5;
    }
}
