using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class InterfaceNameToken<T> : IConnectionToken
    {
        private string _key;

        public InterfaceNameToken(string key = null)
        {
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
            return typeof(T).FullName;
        }

        public string UniqueKey()
        {
            return this._key;
        }
    }
}
