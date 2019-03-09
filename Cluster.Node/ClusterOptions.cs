using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClusterOptions
    {
        /// <summary>
        /// cluster uniqueid
        /// </summary>
        public string ClusterID { get; set; }
        /// <summary>
        /// service's name in cluster
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// max no reply times,will mark it dead
        /// </summary>
        public int MaxNoReply { get; set; } = 10;
        /// <summary>
        /// max no active time,will mark it dead
        /// </summary>
        public TimeSpan MaxNoActiveTime { get; set; } = TimeSpan.FromSeconds(30);
    }
}
