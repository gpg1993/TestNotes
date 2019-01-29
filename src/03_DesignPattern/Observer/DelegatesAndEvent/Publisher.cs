using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public delegate void NumChangedEventHandler();

    /// <summary>
    /// 发布者
    /// </summary>
    public class Publisher
    {
        public NumChangedEventHandler NumChangedEvent;// 声明委托变量(委托可以任意触发)
        //public event NumChangedEventHandler NumChangedEvent; // 声明一个事件(事件只能被发布者本身触发，不能直接触发)
        /// <summary>
        /// 执行任务
        /// </summary>
        public void DoTask()
        {
            if (NumChangedEvent == null)
                return;
            NumChangedEvent();
        }
    }
}
