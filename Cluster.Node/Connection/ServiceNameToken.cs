using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class ServiceNameToken : IConnectionToken
    {
        private string _name;
        private string _key;

        public ServiceNameToken(string name, string key)
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

        public ServiceNameToken(string name)
        {
            this._name = name;
            this._key = Guid.NewGuid().ToString("N");
        }

        public string Name()
        {
            return this._name;
        }

        public string UniqueKey()
        {
            return this._key;
        }
    }
}
