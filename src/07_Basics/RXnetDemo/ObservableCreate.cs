using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;

namespace RXnetDemo
{
    public static class ObservableCreate
    {
        public static IObservable<int> GetSimpleObservable()
        {
            return Observable.Return(42);
        }

        public static IObservable<int> GetThrowObservable()
        {
            return Observable.Throw<int>(new ArgumentException("Error in observable"));
        }

        public static IObservable<int> GetEmptyObservable()
        {
            return Observable.Empty<int>();
        }

        //使用 ToObservable转换 IEnumerate和Task类型
        public static IObservable<int> GetTaskObservable()
        {
            return GetTask().ToObservable();
        }

        public static async Task<int> GetTask()
        {
            return await Task.Run(()=> { Random rd = new Random(); return rd.Next(); });
        }

        public static IObservable<int> GetRangeObservable()
        {
            return Observable.Range(2, 10);
        }

        public static IObservable<long> GetIntervalObservable()
        {
            return Observable.Interval(TimeSpan.FromMilliseconds(200));
        }

        public static IObservable<int> GetCreateObservable()
        {
            return Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnNext(2);
                observer.OnNext(3);
                observer.OnNext(4);
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        public static IObservable<int> GetGenerateObservable()
        {
            return Observable.Generate(
                1,
                x => x < 5,
                x => x + 1,
                x => x
            );
        }
    }
}
