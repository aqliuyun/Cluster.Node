using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.Connection
{
    public class AuthenticatedConnectionToken : DefaultConnectionToken, IAuthenticatedConnectionToken
    {
        protected string _token;

        public AuthenticatedConnectionToken(string name, string token) : base(name, null)
        {
            this._token = token;
        }

        public AuthenticatedConnectionToken(string name, string key, string token) : base(name, key)
        {
            this._token = token;
        }

        public string Token()
        {
            return _token;
        }
    }
}
