using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 专题
    /// </summary>
    public abstract class BaseSubject : IObserverble
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Register(IObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }

        public void UnRegister(IObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public virtual void Noticing(IObserverble observerble, EventArgs eventArgs)
        {
            observers.ForEach(c => { c.Notify(observerble, eventArgs); });
        }
    }

    public class EventArgs
    {
        private readonly DateTime _TimeNow;
        private readonly string _Intruder;

        public EventArgs(DateTime TimeNow,string Intruder)
        {
            this._TimeNow = TimeNow;
            this._Intruder = Intruder;
        }

        public DateTime TimeNow { get { return _TimeNow; }}

        public string Intruder { get { return _Intruder; } }
    }
}
