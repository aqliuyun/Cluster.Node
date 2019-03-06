using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class WcfClientProxy<T> : ClientBase<T> where T : class
    {
        public T ClientChannel => this.Channel;

        public WcfClientProxy(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {            
        }
    }
}
