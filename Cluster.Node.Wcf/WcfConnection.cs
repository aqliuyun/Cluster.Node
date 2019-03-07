using Cluster.Node.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class WcfConnection : ClusterConnection, IWcfConnection
    {
        private string _endpointConfigName = string.Empty;
        private ICommunicationObject _instance = null;
        private Type _instance_type;
        private Type _interface_type;
        private IGatewayProvider _gatewayProvider;

        public WcfConnection(IGatewayProvider gatewayProvider, ClusterContext context) : base(context)
        {
            this._gatewayProvider = gatewayProvider;
        }

        public IWcfConnection Bind(string endpointConfigName)
        {
            if (string.IsNullOrEmpty(endpointConfigName.Trim())) throw new ArgumentException("终结点不能配置为空。");
            this._endpointConfigName = endpointConfigName;
            return this;
        }

        public T As<T>() where T : class
        {
            this._interface_type = typeof(T);
            this._instance_type = typeof(WcfClientProxy<T>);
            this.Open();
            return ((WcfClientProxy<T>)_instance).ClientChannel;
        }

        private void Open()
        {
            try
            {
                if (_instance == null || _instance.State != System.ServiceModel.CommunicationState.Opened)
                {
                    lock (_instance)
                    {
                        if (_instance == null || _instance.State != System.ServiceModel.CommunicationState.Opened)
                        {
                            if (this.Connect())
                            {
                                _instance.Faulted += Faulted;
                            }
                            else
                            {
                                OnDisconnected?.Invoke(this.gateway);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Faulted(object sender, EventArgs e)
        {
            lock (_instance)
            {
                OnRetryConnect?.Invoke(this.gateway);
            }
        }

        public override bool Connect(string gateway)
        {
            try
            {
                var node = _gatewayProvider.GetClusterNode(gateway);
                if (node.Details.TryGetValue("contract", out string list))
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(list);
                    _instance = (ICommunicationObject)Activator.CreateInstance(_instance_type, _endpointConfigName, dict[_interface_type.FullName]);
                    this.OnConnected?.Invoke(_instance);
                    return true;
                }
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
                var channel = _instance;
                if (channel != null)
                {
                    channel.Faulted -= this.Faulted;
                    switch (channel.State)
                    {
                        case CommunicationState.Faulted:
                            channel.Abort();
                            break;
                        case CommunicationState.Closing:
                        case CommunicationState.Closed:
                            break;
                        default:
                            channel.Close();
                            break;
                    }
                }
            }
            catch { }
            finally
            {
                _instance = null;
            }
        }
    }
}
