using System;

namespace Adapter
{
    class Program
    {
        /// <summary>
        /// 将一个接口转换成客户希望的另一个接口，使接口不兼容的那些类可以一起工作。
        /// （1）Target（目标抽象类）：目标抽象类定义了客户所需要的接口，可以是一个抽象类或接口，也可以是一个具体的类。
        /// （2）Adapter（适配器类）：适配器可以调用另一个接口，作为一个转换器，对Adaptee和Target进行适配。适配器类是适配者模式的核心，在适配器模式中，它通过继承Target并关联一个Adaptee对象使二者产生联系。
        /// （3）Adaptee（适配者类）：适配者即被适配的角色，它定义了一个已经存在的接口，这个接口需要适配，一般是一个具体类，包含了客户希望使用的业务方法，在某些情况下可能没有适配者类的源代码。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //植物大战僵尸豌豆人光合作用产生能量，产生果实，攻击
            IPeaser peaser = new PeaserOperation();
            peaser.Photosynthesis();
            peaser.CreateFruit();
            peaser.attacking();
        }
    }
}
