using System;

namespace Adapter
{
    class Program
    {
        /// <summary>
        /// 将一个接口转换成客户希望的另一个接口，使接口不兼容的那些类可以一起工作。
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
