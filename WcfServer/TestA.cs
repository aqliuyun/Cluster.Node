using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WcfServerA
{
    public class TestA : ITestA
    {
        public string Get()
        {
            return "I am server A this is test a, in process:"+Process.GetCurrentProcess().Id.ToString();
        }
    }
}
