using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Provider.Redis
{
    public class RedisOptions
    {
        public int Database { get; set; } = 0;
        public string ConnectionString { get; set; } = "localhost:6379";
    }
}
