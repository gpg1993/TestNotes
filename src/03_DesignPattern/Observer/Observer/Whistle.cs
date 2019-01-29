using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 鸣笛（消费者）
    /// </summary>
    public class Whistle : IObserver
    {
        public void Notify(IObserverble observerble, EventArgs eventArgs)
        {
            Console.WriteLine($"I am whistling");
        }
    }
}
