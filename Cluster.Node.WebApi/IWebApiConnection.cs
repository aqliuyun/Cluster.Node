using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Node.WebApi
{
    public interface IWebApiConnection
    {
        Task<T> GetAsync<T>(string url);

        Task<T> PostAsync<T>(string url, object data);

        Task<T> PutAsync<T>(string url, object data);

        Task<T> DeleteAsync<T>(string url);
    }
}
