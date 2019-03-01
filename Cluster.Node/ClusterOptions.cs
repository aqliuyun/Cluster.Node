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

        public int MaxRetryTimes { get; set; } = DEFAULT_MAX_REPLYTIME;

        public int RetryInterval { get; set; } = DEFAULT_MAX_REPLYINTERVAL;

        public int MaxNoReply { get; set; } = DEFAULT_MAX_NO_REPLY;

        public const int DEFAULT_MAX_NO_REPLY = 5;

        public const int DEFAULT_MAX_REPLYTIME = 5;

        public const int DEFAULT_MAX_REPLYINTERVAL = 10000;
    }
}
