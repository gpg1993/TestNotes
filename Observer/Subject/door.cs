using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 门（被观察的具体对象）
    /// </summary>
    public class door:BaseSubject
    {
        public DateTime _TimeNow;//闯入时间
        public string _Intruder;//闯入者
        //是否开启警报
        public Boolean IsAlarm = true;
        public door() : base()
        {

        }

        /// <summary>
        /// 广播
        /// </summary>
        protected virtual void broadcast(IObserverble observerble, EventArgs eventArgs)
        {
            base.Noticing(observerble, eventArgs);
        }

        public void OpenedDoor()
        {
            if (IsAlarm)
            {
                _TimeNow = DateTime.Now;
                Random ran = new Random();
                int RandKey = ran.Next(100, 999);
                _Intruder = $"闯入者{RandKey}";
                EventArgs eventArgs = new EventArgs(_TimeNow, _Intruder);
                broadcast(this, eventArgs);
            }
        }
    }
}
