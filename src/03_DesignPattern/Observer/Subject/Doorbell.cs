using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 门铃（被观察的具体对象）
    /// </summary>
    public class Doorbell : BaseSubject
    {
        protected virtual void broadcast(IObserverble observerble, EventArgs eventArgs)
        {
            base.Noticing(observerble, eventArgs);
        }

        public void Press()
        {
            Random ran = new Random();
            int RandKey = ran.Next(100, 999);
            EventArgs eventArgs = new EventArgs(DateTime.Now, $"闯入者{RandKey}");
            broadcast(this, eventArgs);
        }
    }
}
