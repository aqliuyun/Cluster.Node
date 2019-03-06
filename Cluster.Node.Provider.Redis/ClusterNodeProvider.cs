using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Provider.Redis
{
    public class ClusterNodeProvider : IClusterNodeProvider
    {
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly IDatabase _db;
        private readonly RedisOptions _redisOptions;
        private readonly ClusterOptions _clusterOptions;
        public ClusterNodeProvider(IConnectionMultiplexer multiplexer, RedisOptions redisOptions, ClusterOptions clusterOptions)
        {
            _redisOptions = redisOptions;
            _multiplexer = multiplexer;
            _db = multiplexer.GetDatabase(_redisOptions.Database);
            _clusterOptions = clusterOptions;
        }

        public async Task<List<ClusterNode>> GetClusterNodeList()
        {
            var list = new List<ClusterNode>();
            var nodes = _db.HashGetAll(_clusterOptions.ClusterID).Select(x => JsonConvert.DeserializeObject<ClusterNode>(x.Value));
            foreach (var node in nodes)
            {
                if (node.NoReply >= _clusterOptions.MaxNoReply || (DateTime.Now - node.LastActiveTime) >= _clusterOptions.MaxNoActiveTime)
                {
                    await _db.HashDeleteAsync(_clusterOptions.ClusterID, node.Address);
                }
                else
                {
                    list.Add(node);
                }
            }
            return await Task.FromResult(list);

        }

        public async Task<bool> RegisterClusterNode(ClusterNode node)
        {
            return await _db.HashSetAsync(_clusterOptions.ClusterID, node.Address, JsonConvert.SerializeObject(node));
        }

        public async Task<bool> RemoveClusterNode(ClusterNode node)
        {
            return await _db.HashDeleteAsync(_clusterOptions.ClusterID, node.Address);
        }

        public async Task<bool> UpdateClusterNode(ClusterNode node, Action<ClusterNode> updateAction)
        {
            var lastest = JsonConvert.DeserializeObject<ClusterNode>(await _db.HashGetAsync(_clusterOptions.ClusterID, node.Address));
            updateAction?.Invoke(lastest);
            return await _db.HashSetAsync(_clusterOptions.ClusterID, node.Address, JsonConvert.SerializeObject(lastest));
        }

        public async Task<bool> UpdateClusterNodeAsync(ClusterNode node)
        {
            return await _db.HashSetAsync(_clusterOptions.ClusterID, node.Address, JsonConvert.SerializeObject(node));
        }
    }
}
