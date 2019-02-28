using Cluster.Node.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Wcf
{
    public class KeepAliveConnection : ClusterConnection, IWcfConnection
    {
        private string _endpointConfigName = string.Empty;
        private ICommunicationObject _instance = null;
        private Type _instance_type;
        public bool IsBusy { get; set; }
        
        public KeepAliveConnection(ClusterContext context,string gateway):base(context)
        {
            this.gateway = gateway;
        }

        public IWcfConnection Bind(string endpointConfigName)
        {
            if (string.IsNullOrEmpty(endpointConfigName.Trim())) throw new ArgumentException("终结点不能配置为空。");
            this._endpointConfigName = endpointConfigName;
            return this;
        }

        public T As<T>() where T : class
        {
            this._instance_type = typeof(WcfClient<T>);
            this.Open();
            return ((WcfClient<T>)_instance).ClientChannel;
        }

        private void Open()
        {
            if (_instance == null || _instance.State != System.ServiceModel.CommunicationState.Opened)
            {
                try
                {
                    this.Connect();
                    _instance.Faulted += Faulted;
                }
                catch
                {
                    IsBusy = true;
                    ReConnect();
                }
            }
        }

        private void Faulted(object sender, EventArgs e)
        {
            lock (_instance)
            {
                IsBusy = true;
                ReConnect();
            }
        }

        public override bool Connect(string gateway)
        {
            _instance = (ICommunicationObject)Activator.CreateInstance(_instance_type, _endpointConfigName, this.gateway);
            IsBusy = false;
            return true;
        }

        private void ReConnect()
        {
            int retryTimes = 0;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (retryTimes > context.Gateways.Count)
                        {
                            break;
                        }
                        retryTimes++;
                        this.Connect();
                        IsBusy = false;
                        break;
                    }
                    catch
                    {
                        int sleepMillionSeconds = retryTimes * 5 * 1000;
                        sleepMillionSeconds = sleepMillionSeconds > 60 * 1000 ? 60 * 1000 : sleepMillionSeconds;
                        System.Threading.Thread.Sleep(sleepMillionSeconds);
                    }
                }
            });
        }

        public void Dispose()
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
            _instance = null;
        }
    }
}
