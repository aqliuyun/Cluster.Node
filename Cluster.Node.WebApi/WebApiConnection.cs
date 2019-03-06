using Cluster.Node.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.WebApi
{
    public class WebApiConnection : ClusterConnection, IWebApiConnection
    {
        private IGatewayProvider _gatewayProvider;
        private HttpClient _instance;
        public WebApiConnection(ClusterContext context) : base(context)
        {
        }

        public WebApiConnection(IGatewayProvider gatewayProvider, ClusterContext context) : base(context)
        {
            this._gatewayProvider = gatewayProvider;
        }

        public override bool Connect(string gateway)
        {
            var node = _gatewayProvider.GetClusterNode(gateway);
            _instance = new HttpClient()
            {
                BaseAddress = new Uri(node.Address)
            };
            _instance.DefaultRequestHeaders.Accept.Clear();
            _instance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.OnConnected?.Invoke(_instance);
            return true;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var res = await _instance.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
            }
            this.OnDisconnected?.Invoke(this.gateway);
            return default(T);
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var res = await _instance.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
            }
            this.OnDisconnected?.Invoke(this.gateway);
            return default(T);
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            var res = await _instance.PutAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
            }
            this.OnDisconnected?.Invoke(this.gateway);
            return default(T);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var res = await _instance.DeleteAsync(url);
            if (res.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
            }
            this.OnDisconnected?.Invoke(this.gateway);
            return default(T);
        }

        public void Dispose()
        {
            try
            {
                _instance.Dispose();
            }
            catch { }
            finally
            {
                _instance = null;
            }
        }
    }
}
