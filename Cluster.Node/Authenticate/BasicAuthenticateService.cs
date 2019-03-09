using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cluster.Node.Connection;

namespace Cluster.Node.Authenticate
{
    public class BasicAuthenticateService : IAuthenticateService
    {
        public bool Authenticate(ClusterNode node, IAuthenticatedConnectionToken token)
        {
            if(node.Details.TryGetValue("Authorization", out string value))
            {
                return token.Token().Equals(value);
            }
            return false;
        }

        public void Init()
        {
            
        }
    }
}
