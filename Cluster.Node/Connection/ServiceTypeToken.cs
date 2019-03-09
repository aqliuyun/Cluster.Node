using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ServiceTypeToken<T> : DefaultConnectionToken
    {

        public ServiceTypeToken(string key = null) : base(typeof(T).FullName, key)
        {
        }
    }
}
