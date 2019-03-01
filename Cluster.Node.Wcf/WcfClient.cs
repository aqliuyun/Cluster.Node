using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class WcfClient<T> : ClientBase<T> where T : class
    {
        public T ClientChannel => this.Channel;

        public WcfClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {            
        }
    }
}
