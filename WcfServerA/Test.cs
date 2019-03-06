using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WcfServerA
{
    public class Test : ITest
    {
        public string Get()
        {
            return "I am server A this is test, in process:"+Process.GetCurrentProcess().Id.ToString();
        }
    }
}
