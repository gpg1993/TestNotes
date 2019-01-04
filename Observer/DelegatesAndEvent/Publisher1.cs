using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public delegate string GeneralEventHandler();
    /// <summary>
    /// 多订阅但只返回单个消息
    /// </summary>
    public class Publisher1
    {
        public event GeneralEventHandler GeneralEvent;
        public void DoTask()
        {
            if (GeneralEvent == null)
                return;
            string strMsg = GeneralEvent();
            Console.WriteLine(strMsg);
        }
    }

    public class Publisher4
    {
        public event GeneralEventHandler GeneralEvent;
        public List<string> DoTask()
        {
            List<string> list = new List<string>();
            if (GeneralEvent == null)
                return null;
            Delegate[] @delegate = GeneralEvent.GetInvocationList();
            //方法1
            //for (int i = 0; i < @delegate.Length; i++)
            //{
            //    GeneralEventHandler a =(GeneralEventHandler)@delegate[i];
            //    list.Add(a());
            //}

            //方法2
            //foreach (Delegate item in @delegate)
            //{
            //    try
            //    {
            //        item.DynamicInvoke(this);
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("Exception: {0}", e.Message);
            //    }
            //}

            //方法3
            FireEvent(GeneralEvent, null);
            return list;
        }

        // 触发某个事件，以列表形式返回所有方法的返回值
        public static object[] FireEvent(Delegate del, params object[] args)
        {
            List<object> objList = new List<object>();

            if (del != null)
            {
                Delegate[] delArray = del.GetInvocationList();
                foreach (Delegate method in delArray)
                {
                    try
                    {
                        // 使用DynamicInvoke方法触发事件
                        object obj = method.DynamicInvoke(args);
                        if (obj != null)
                            objList.Add(obj);
                    }
                    catch { }
                }
            }
            return objList.ToArray();
        }
    }



    public class Subcriber1
    {
        public string DoChanged()
        {
            string myName = "Subcriber1";
            return $"我是：{myName}";
        }
    }
    public class Subcriber2
    {
        public string DoChanged()
        {
            string myName = "Subcriber2";
            return $"我是：{myName}";
        }
    }
    public class Subcriber3
    {
        public string DoChanged()
        {
            string myName = "Subcriber3";
            return $"我是：{myName}";
        }
    }

    // 定义事件发布者(只允许单订阅)
    public class Publishser2
    {
        private event GeneralEventHandler NumberChanged;    // 声明一个私有事件
                                                            // 注册事件
        public void Register(GeneralEventHandler method)
        {
            NumberChanged = method;
        }
        // 取消注册
        public void UnRegister(GeneralEventHandler method)
        {
            NumberChanged -= method;
        }

        public void DoSomething()
        {
            // 做某些其余的事情
            if (NumberChanged != null)
            {    // 触发事件
                string rtn = NumberChanged();
                Console.WriteLine("Return: {0}", rtn);      // 打印返回的字符串，输出为Subscriber3
            }
        }
    }
    // 定义事件发布者
    public class Publishser3
    {
        // 声明一个委托变量
        private GeneralEventHandler numberChanged;
        // 事件访问器的定义
        public event GeneralEventHandler NumberChanged
        {
            add
            {
                numberChanged = value;
            }
            remove
            {
                numberChanged -= value;
            }
        }

        public void DoSomething()
        {
            // 做某些其他的事情
            if (numberChanged != null)
            {    // 通过委托变量触发事件
                string rtn = numberChanged();
                Console.WriteLine("Return: {0}", rtn);      // 打印返回的字符串
            }
        }
    }
}
