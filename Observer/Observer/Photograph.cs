using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 拍照（消费者）
    /// </summary>
    public class Photograph : IObserver
    {
        public void Notify(IObserverble observerble, EventArgs eventArgs)
        {
            Console.WriteLine($"I'm taking picture:the men is {eventArgs.Intruder}");
        }
    }
}
