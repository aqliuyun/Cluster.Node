using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public class HostOptions : ClusterOptions
    {
        /// <summary>
        /// listen address
        /// </summary>
        public string Address { get; set; }
    }
}
