using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 发送消息手机（消费者）
    /// </summary>
    public class SendMessagePhone : IObserver
    {
        public void Notify(IObserverble observerble, EventArgs eventArgs)
        {
            // 解耦不够彻底
            // 这里存在一个向下转换(由继承体系中高级别的类向低级别的类转换)。
            door door = (door)observerble;
            Console.WriteLine($"I am SendMessagePhone,the opendoor time {door._TimeNow}");
        }
    }
}
