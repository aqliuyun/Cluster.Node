using Cluster.Node.LoadBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ClusterConnection : IClusterConnection
    {
        protected ConnectionContext context;
        protected string gateway;
        private int _retryTime = 0;
        public int RetryTimes { get { return _retryTime; } private set { _retryTime = value; } }
        public Action<object> OnConnected;
        public Action<string> OnRetryConnect;
        public Action<string> OnDisconnected;
        private SemaphoreSlim semaphore;
        public ClusterConnection(ConnectionContext context)
        {
            this.context = context;
            this.semaphore = new SemaphoreSlim(1);
        }

        public void UseGateway(string gateway)
        {
            if(string.IsNullOrWhiteSpace(gateway))
            {
                this.OnDisconnected?.Invoke(this.gateway);
                return;
            }
            this.gateway = gateway;
        }

        public bool Connect()
        {
            if (string.IsNullOrWhiteSpace(gateway)) return false;
            if(this.Connect(gateway))
            {
                Interlocked.Exchange(ref _retryTime,0);
                return true;
            }
            this.OnRetryConnect?.Invoke(gateway);
            return false;
        }

        public virtual bool Connect(string gateway)
        {
            return true;
        }       

        public void Dispose()
        {            
        }

        public void AcquireLock()
        {
            this.semaphore.Wait();
        }

        public void ReleaseLock()
        {
            this.semaphore.Release();
        }

        public bool ReConnect()
        {
            Interlocked.Increment(ref _retryTime);
            return Connect();
        }
    }
}
