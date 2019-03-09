using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
namespace Cluster.Node.gRPC
{
    public class GrpcConnection : ClusterConnection, IGrpcConnection
    {
        private Channel _channel = null;
        private ClientBase _instance = null;
        private Type _clienttype;
        private IGatewayProvider _gatewayProvider;

        public GrpcConnection(IGatewayProvider gatewayProvider, ConnectionContext context) : base(context)
        {
            this._gatewayProvider = gatewayProvider;
        }
        
        public T As<T>() where T : ClientBase, new()
        {
            this._clienttype = typeof(T);
            this.Open();
            return (T)_instance;
        }

        private void Open()
        {
            if (_channel == null || (_channel.State != ChannelState.Ready && _channel.State != ChannelState.Idle))
            {
                if (!this.Connect()) {                 
                    OnDisconnected?.Invoke(this.gateway);
                }
            }
            if (_instance == null)
            {
                lock (_instance)
                {
                    if (_instance == null)
                    {
                        _instance = Activator.CreateInstance(_clienttype, _channel) as ClientBase;
                    }
                }
            }
        }

        public override bool Connect(string gateway)
        {
            try
            {
                _channel = new Channel(gateway, ChannelCredentials.Insecure);
                this.OnConnected?.Invoke(_instance);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public void Dispose()
        {
            try
            {
                _channel.ShutdownAsync();
            }
            catch { }
            finally
            {
                _channel = null;
                _instance = null;
            }
        }
    }
}
