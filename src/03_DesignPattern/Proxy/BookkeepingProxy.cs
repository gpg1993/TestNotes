using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    /// <summary>
    /// 记账代理类
    /// </summary>
    public class BookkeepingProxy
    {
        private Bookkeeping Bookkeeping = new Bookkeeping();
        private log log;
        private validation validation;

        public void Books(string context)
        {
            if (check(context))
            {
                Bookkeeping.Books(context);
            }
            logWrite(context);
        }
        private void logWrite(string context)
        {
            log = new log();
            log.logWrite(context);
        }
        private bool check(string context)
        {
            validation = new validation();
            return validation.Check(context);
        }
    }
}
