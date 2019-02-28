using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public interface IClusterNodeProvider
    {
        Task<List<ClusterNode>> GetClusterNodeList();

        Task<bool> RegisterClusterNode(ClusterNode node);

        Task<bool> RemoveClusterNode(ClusterNode node);

        Task<bool> UpdateClusterNode(ClusterNode node);
    }
}
