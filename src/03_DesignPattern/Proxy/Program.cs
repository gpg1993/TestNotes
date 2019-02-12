using System;

namespace Proxy
{
    /// <summary>
    /// 代理模式(给某一个对象提供一个代理，并由代理对象控制对原对象的引用。代理模式是一种对象结构型模式。)
    /// （1）Subject（抽象主题角色）：声明真实主题和代理主题的共同接口，使得在任何使用真实主题的地方都可以使用代理主题。
    /// （2）Proxy（代理主题角色）：代理主题角色内部包含了对真实主题的引用，从而可以在任何时候操作真实主题对象；
    /// （3）RealSubject（真实主题角色）：定义了代理角色所代表的真实对象，在真实主题角色中实现了真实的业务操作。
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
