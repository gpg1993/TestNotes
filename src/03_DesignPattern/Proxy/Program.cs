using System;

namespace Proxy
{
    /// <summary>
    /// 代理模式(给某一个对象提供一个代理，并由代理对象控制对原对象的引用。代理模式是一种对象结构型模式。)
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BookkeepingProxy bookkeepingProxy = new BookkeepingProxy();
            bookkeepingProxy.Books("用了15元");
            Console.ReadKey();
        }
    }
}
