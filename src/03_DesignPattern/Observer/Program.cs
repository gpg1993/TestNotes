using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //MainOberser(args);
            //MainEvent();
            //MainMutiSubcriber();
            MainMutiSubcriber1();
        }
        /// <summary>
        /// 观察者模式中的推拉，类似于winform窗体事件
        /// </summary>
        /// <param name="args"></param>
        private static void MainOberser(string[] args)
        {
            door door = new door();
            Doorbell doorbell = new Doorbell();
            Photograph photograph = new Photograph();
            Whistle whistle = new Whistle();
            SendMessagePhone sendMessagePhone = new SendMessagePhone();
            door.Register(photograph);
            door.Register(whistle);
            door.Register(sendMessagePhone);

            //doorbell.Register(photograph);
            //doorbell.Register(sendMessagePhone);
            //doorbell.Press();

            door.IsAlarm = true;
            door.OpenedDoor();

            door.IsAlarm = false;
            door.OpenedDoor();

            door.UnRegister(whistle);
            door.IsAlarm = true;
            door.OpenedDoor();



            Console.ReadLine();
        }
        /// <summary>
        /// 事件调用(事件和委托的区别之一：事件不能被客户端直接调用，只能被事件本身的发布者调用；委托可以直接调用)
        /// </summary>
        private static void MainEvent()
        {
            Publisher publisher = new Publisher();
            Subcriber subcriber = new Subcriber();
            publisher.NumChangedEvent += new NumChangedEventHandler(subcriber.DoNumChanged);

            publisher.DoTask();
            publisher.NumChangedEvent();
        }
        /// <summary>
        /// 多订阅收到一条返回值
        /// </summary>
        private static void MainMutiSubcriber()
        {
            Publisher1 publisher = new Publisher1();
            Subcriber1 subcriber1 = new Subcriber1();
            Subcriber2 subcriber2 = new Subcriber2();
            Subcriber3 subcriber3 = new Subcriber3();
            publisher.GeneralEvent += new GeneralEventHandler(subcriber1.DoChanged);
            publisher.GeneralEvent += new GeneralEventHandler(subcriber2.DoChanged);
            publisher.GeneralEvent += new GeneralEventHandler(subcriber3.DoChanged);
            publisher.DoTask();

            //Publishser2 publishser2 = new Publishser2();
            //publishser2.Register(new GeneralEventHandler(subcriber1.DoChanged));

            //Publishser3 publishser3 = new Publishser3();
            //publishser3.NumberChanged += subcriber1.DoChanged;
            //publishser3.NumberChanged -= subcriber1.DoChanged;
        }
        /// <summary>
        /// 多订阅收到多条返回值
        /// </summary>
        private static void MainMutiSubcriber1()
        {
            Publisher4 publisher = new Publisher4();
            Subcriber1 subcriber1 = new Subcriber1();
            Subcriber2 subcriber2 = new Subcriber2();
            Subcriber3 subcriber3 = new Subcriber3();
            publisher.GeneralEvent += new GeneralEventHandler(subcriber1.DoChanged);
            publisher.GeneralEvent += new GeneralEventHandler(subcriber2.DoChanged);
            publisher.GeneralEvent += new GeneralEventHandler(subcriber3.DoChanged);
            List<string> list = publisher.DoTask();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }
}
