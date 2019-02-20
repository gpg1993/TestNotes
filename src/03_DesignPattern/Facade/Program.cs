using Facade.Facade;
using System;

namespace Facade
{
    /// <summary>
    /// （1）对客户端屏蔽了子系统组件，减少了客户端需要处理的对象数量并且使得子系统使用起来更加容易。
    /// （2）实现了子系统与客户端之间松耦合。
    /// （3）提供了一个访问子系统的统一入口，并不影响客户端直接使用子系统。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AbstractEncryptFacade newFacade = new NewEncryptFacade();
            newFacade.FileEncrypt("Facade/src.txt", "Facade/des.txt");

            IEncryptFacade encryptFacade = new EncryptFacade();
            encryptFacade.FileEncrypt("Facade/src.txt", "Facade/des.txt");
        }
    }
}
