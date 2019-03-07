using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Authenticate
{
    public interface IAuthenticateService
    {
        void Init();
        bool Authenticate(ClusterNode node,IAuthenticatedConnectionToken token);
    }
}
