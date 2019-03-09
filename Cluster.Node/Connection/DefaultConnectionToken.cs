using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public abstract class DefaultConnectionToken : IConnectionToken
    {
        protected string _name;
        protected string _key;
        public DefaultConnectionToken(string name, string key)
        {
            this._name = name;
            if (!string.IsNullOrWhiteSpace(key))
            {
                this._key = key;
            }
            else
            {
                this._key = Guid.NewGuid().ToString("N");
            }
        }

        public string Name()
        {
            return _name;
        }

        public string UniqueKey()
        {
            return _key;
        }
    }
}
