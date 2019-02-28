using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.IBLL
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        string Get();
    }
}
