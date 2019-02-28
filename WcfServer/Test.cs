using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WcfServer
{
    public class Test : ITest
    {
        public string Get()
        {
            return Process.GetCurrentProcess().Id.ToString();
        }
    }
}
