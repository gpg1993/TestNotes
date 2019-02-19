using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;

namespace RXnetDemo
{
    /// <summary>
    /// 创建可观察对象
    /// rx的主要接口为IObersable和IOberser两个通过Subject衔接
    /// </summary>
    public class test1
    {
        /**
         * 多播传输
         * BehaviorSubject- 推送默认值或最新值给观察者
         * ReplaySubject- 缓存指定通知以对后续订阅的观察者进行重放
         * AsyncSubject- 当可观察序列完成后有且仅发送一个通知
         * Subject- 向所有观察者广播每个通知
         * **/


        /// <summary>
        /// ToObservable创建
        /// </summary>
        public void create1()
        {
            var observable = Enumerable.Range(1, 100).ToObservable(NewThreadScheduler.Default);
            Subject<int> subject = new Subject<int>();
            subject.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            subject.Subscribe(waterTemperature, exMethon);
            observable.Subscribe(subject);
        }
        /// <summary>
        /// Observable.Create创建
        /// </summary>
        public void create2()
        {
            var observable = Observable.Create<int>(observer =>
            {
                for (int i = 0; i < 5; i++)
                {
                    observer.OnNext(i);
                }
                return Disposable.Empty;
            });
            Subject<int> subject = new Subject<int>();
            subject.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            subject.Subscribe(waterTemperature, exMethon);
            observable.Subscribe(subject);
        }
        /// <summary>
        /// 使用 Observable.Deffer进行延迟创建（当有观察者订阅时才创建）比如要连接数据库进行查询，
        /// 如果没有观察者，那么数据库连接会一直被占用，这样会造成资源浪费。使用Deffer可以解决这个问题。
        /// </summary>
        public void creatDefer()
        {
            //Observable.Defer(() =>
            //{
            //    var connection = Connect(user, password);
            //    return connection.ToObservable();
            //});
        }
        public void creatGenerate()
        {
            /*
             * NewThreadScheduler：即在新线程上执行
             * ThreadPoolScheduler：即在线程池中执行
             * TaskPoolScheduler：同ThreadPoolScheduler
             * CurrentThreadScheduler：在当前线程执行
             * ImmediateScheduler：在当前线程立即执行
             * EventLoopScheduler：创建一个后台线程按序执行所有操作
             * **/
            Console.WriteLine($"Current ThreadId：{Thread.CurrentThread.ManagedThreadId}");
            IObservable<int> observable =
                Observable.Generate(
                    0,//起始状态
                    i => i < 10,//条件当状态满足一定条件时才执行
                    i => { Console.WriteLine($"iterate {i} on ThreadId：{Thread.CurrentThread.ManagedThreadId}"); return i + 1; },//迭代 步骤
                    i => { Console.WriteLine($"selectResult {i} on ThreadId：{Thread.CurrentThread.ManagedThreadId}"); return i * 2; }, //返回迭代结果
                    new EventLoopScheduler()//NewThreadScheduler.Default
                    );
            Subject<int> subject = new Subject<int>();
            subject.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            subject.Subscribe(waterTemperature, exMethon);
            observable.Subscribe(subject);
            Console.WriteLine($"Current ThreadId：{Thread.CurrentThread.ManagedThreadId}");
        }

        public void CreateSpecial()
        {
            Observable.Return("Hello World");//创建单个元素的可观察序列
            Observable.Never<string>();//创建一个空的永远不会结束的可观察序列
            Observable.Throw<int>(new ArgumentException("Error in observable"));////创建一个抛出指定异常的可观察序列
            Observable.Empty<string>();//创建一个空的立即结束的可观察序列
        }

        public void CreateTask()
        {
            var observable = ObservableCreate.GetTaskObservable();
            Subject<int> subject = new Subject<int>();
            subject.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            observable.Subscribe(subject);
        }

        

        #region 冷热
        /// <summary>
        /// 不管有无观察者订阅都会发送通知，且所有观察者共享同一份观察者序列。
        /// </summary>
        public void hotObservable()
        {
            IObservable<int> observable =
                Observable.Generate(
                    0,//起始状态
                    i => i < 10,//条件当状态满足一定条件时才执行
                    i => i + 1,//迭代 步骤
                    i => i * 2 //返回迭代结果

                    );
            observable.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            observable.Subscribe((temperature) => Console.WriteLine($"当前温度1：{temperature}"), () => Console.WriteLine($"--------"));
        }
        /// <summary>
        /// 有且仅当有观察者订阅时才发送通知，且每个观察者独享一份完整的观察者序列。
        /// Observable可以为每个Subscriber创建新的数据生产者
        /// </summary>
        public void ColdObservable()
        {
            IObservable<int> observable =
                Observable.Generate(
                    0,//起始状态
                    i => i < 10,//条件当状态满足一定条件时才执行
                    i => i + 1,//迭代 步骤
                    i => i * 2 //返回迭代结果

                    );
            Subject<int> subject = new Subject<int>();
            subject.Subscribe((temperature) => Console.WriteLine($"当前温度：{temperature}"), () => Console.WriteLine($"--------"));
            observable.Subscribe((temperature) => Console.WriteLine($"当前温度1：{temperature}"), () => Console.WriteLine($"--------"));
            observable.Subscribe(subject);
        }
        #endregion

        public void SchedulerTest()
        {
            Console.WriteLine($"Current ThreadId：{Thread.CurrentThread.ManagedThreadId}");
            Observable.Return("hello", NewThreadScheduler.Default)
                .Subscribe(str => Console.WriteLine($"{str} on ThreadId：{Thread.CurrentThread.ManagedThreadId}"));
            Console.WriteLine($"Current ThreadId：{Thread.CurrentThread.ManagedThreadId}");
        }



        private void waterTemperature(int temp)
        {
            int a = temp % 2;
            if (a == 1)
            {
                Console.WriteLine($"奇数播报温度：{temp}");
            }
            else
            {
                //throw new Exception("偶数异常");
            }
        }
        private void exMethon(Exception exception)
        {
            Console.WriteLine($"出问题了：{exception.Message}");
        }



    }
}
