using System;

namespace Flyweight
{
    /// <summary>
    /// （1）Flyweight（抽象享元类）：一个接口或抽象类，声明了具体享元类的公共方法。
    /// （2）ConcreteFlyweight（具体享元类）：实现了抽象享元类，其实例称为享元对象。
    /// （3）UnsharedConcreteFlyweight（非共享具体享元类）：并不是所有的抽象享元类的子类都需要被共享，不能被共享的子类可设计为费共享具体享元类。
    /// （4）FlyweightFactory（享元工厂类）：用于创建并管理享元对象，一般设计为一个存储“Key-Value”键值对的集合（可以结合工厂模式设计）。
    ///     其作用就在于：提供一个用于存储享元对象的享元池，当用户需要对象时，首先从享元池中获取，如果享元池中不存在，
    ///     那么则创建一个新的享元对象返回给用户，并在享元池中保存该新增对象。=> 想想.NET中的各种资源池的设计？
    ///  缺点：为了使对象可以共享，享元模式需要将享元对象的部分状态外部化，而读取外部状态将使得运行时间变长！
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 获取享元工厂
            SoldierFactory soldierFactory = SoldierFactory.GetInstance();
            // 通过享元工厂获取
            Soldier soldier1 = soldierFactory.GetSoldier(StandType.blue);
            Soldier soldier2 = soldierFactory.GetSoldier(StandType.blue);
            Soldier soldier3 = soldierFactory.GetSoldier(StandType.blue);
            Soldier soldier4 = soldierFactory.GetSoldier(StandType.blue);
            Soldier soldier5 = soldierFactory.GetSoldier(StandType.blue);

            Console.WriteLine("判断两个士兵是否相同：{0}", object.ReferenceEquals(soldier1, soldier2));

            Soldier soldier6 = soldierFactory.GetSoldier(StandType.red);
            Soldier soldier7 = soldierFactory.GetSoldier(StandType.red);
            Soldier soldier8 = soldierFactory.GetSoldier(StandType.red);

            Console.WriteLine("判断两个士兵是否相同：{0}", object.ReferenceEquals(soldier6, soldier7));

            soldier1.attack(new Target { TargetName = "A高地", x = 0, y = 1 });
            soldier2.attack(new Target { TargetName = "A高地", x = 0, y = 1 });
            soldier3.attack(new Target { TargetName = "A高地", x = 0, y = 1 });
            soldier4.attack(new Target { TargetName = "A高地", x = 0, y = 1 });

            Console.ReadKey();
        }
    }
}
