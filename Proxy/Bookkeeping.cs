using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    /// <summary>
    /// 记账
    /// </summary>
    public class Bookkeeping
    {
        public void Books(string context)
        {
            Console.WriteLine($"{DateTime.Now}记账{context}");
        }
    }
    public class log
    {
        public void logWrite(string context)
        {
            Console.WriteLine($"{DateTime.Now}记录日志{context}");
        }
    }
    public class validation
    {
        public bool Check(string context)
        {
            Console.WriteLine($"{DateTime.Now}验证{context}");
            if (string.IsNullOrEmpty(context))
            {
                return false;
            }
            return true;
        }
    }
}
