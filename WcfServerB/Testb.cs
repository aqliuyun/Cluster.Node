using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.IBLL;

namespace WcfServerB
{
    public class TestB : ITestB
    {
        public string Get()
        {
            return "I am server b,this is test b!";
        }
    }
}
