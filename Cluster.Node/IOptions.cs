using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node
{
    public interface IOptions<out TOptions> where TOptions : class, new()
    {
        TOptions Value { get; }
    }
}
