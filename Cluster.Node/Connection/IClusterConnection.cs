using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public interface IClusterConnection : IDisposable
    {
        void UseGateway(string gateway);

        bool Connect();

        bool ReConnect();

        void AcquireLock();

        void ReleaseLock();
    }
}
