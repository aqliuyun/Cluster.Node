using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class ClientOptions : ClusterOptions
    {       
        /// <summary>
        /// reconnection times
        /// </summary>
        public int MaxRetryTimes { get; set; } = 5;
        /// <summary>
        /// reconnection delay time (seconds)
        /// </summary>
        public int RetryInterval { get; set; } = 5;
        /// <summary>
        /// diconnect restore delay time (seconds)
        /// </summary>
        public int RestoreInterval { get; set; } = 30;
    }
}
