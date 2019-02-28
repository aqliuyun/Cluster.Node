using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public interface IWcfConnection
    {
        IWcfConnection Bind(string endpointConfigName);
        T As<T>() where T : class;
    }
}
