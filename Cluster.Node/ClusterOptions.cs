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

        public string ServerName { get; set; }
        /// <summary>
        /// reconnection times
        /// </summary>
        public int MaxRetryTimes { get; set; } = 5;
        /// <summary>
        /// reconnection delay time
        /// </summary>
        public int RetryInterval { get; set; } = 5;
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
