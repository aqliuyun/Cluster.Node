using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ServiceNameToken : DefaultConnectionToken
    {
        public ServiceNameToken(string name, string key) : base(name, key)
        {

        }

        public ServiceNameToken(string name) : base(name, null)
        {
        }
    }
}
