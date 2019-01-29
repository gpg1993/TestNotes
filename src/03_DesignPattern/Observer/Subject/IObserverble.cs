using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    /// <summary>
    /// 可观察的对象
    /// </summary>
    public interface IObserverble
    {
        void Register(IObserver observer);

        void UnRegister(IObserver observer);

    }
}
