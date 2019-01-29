using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 观察者
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 通知
        /// </summary>
        void Notify(IObserverble observerble, EventArgs eventArgs);
    }
}
