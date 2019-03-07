using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public interface IConnectionToken
    {
        string Name();

        string UniqueKey();
    }
}
