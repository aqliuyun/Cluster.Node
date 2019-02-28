using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.LoadBalance
{
    public interface IConnectionManage
    {
        IClusterConnection GetConnection();
    }
}
