using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 订阅者
    /// </summary>
    public class Subcriber
    {
        public void DoNumChanged()
        {
            Console.WriteLine("执行订阅");
        }
    }
}
