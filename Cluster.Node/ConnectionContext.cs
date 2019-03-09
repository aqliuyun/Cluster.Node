using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections;

namespace Cluster.Node
{
    public class ConnectionContext
    {
        public List<ClusterNode> ClusterNodes { get; set; }

        public ArrayList BlackList { get; private set; } = ArrayList.Synchronized(new ArrayList());

        public string ConnectedServiceName { get; set; }        
    }
}
