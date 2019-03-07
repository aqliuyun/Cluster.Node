using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace Cluster.Node.gRPC
{
    public interface IGrpcConnection
    {
        T As<T>() where T : ClientBase,new();
    }
}
