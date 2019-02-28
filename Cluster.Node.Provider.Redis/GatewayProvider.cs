using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Provider.Redis
{
    public class GatewayProvider : IGatewayProvider
    {
        private IClusterNodeProvider provider;
      
        public GatewayProvider(IClusterNodeProvider provider)
        {
            this.provider = provider;
        }

        public async Task<List<string>> GetGateways()
        {
            var nodes = await provider.GetClusterNodeList();
            return await Task.FromResult(nodes.Select(x => x.Address).ToList());
        }

        public async Task Init()
        {
            await Task.CompletedTask;
        }
    }
}
