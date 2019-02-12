using System;

namespace Bridge
{
    /// <summary>
    /// 桥接模式是一种很实用的结构型模式，如果软件系统中某个类存在两个独立变化的维度，
    /// 通过该模式可以将这两个维度分离出来，使两者可以独立扩展，让系统更加符合单一职责原则。
    /// 桥接模式主要使用抽象关联取代传统的多重继承，将类之间的静态继承关系转换为动态地对象组合关系，
    /// 使得系统更加灵活，并易于扩展，同时有效地控制了系统中类的个数。
    /// （1）Abstraction（抽象类）：用于定义抽象类的接口，其中定义了一个实现了
    ///      Implementor接口的对象并可以维护该对象，它与Implementor之间具有关联关系， 
    ///      它既可以包含抽象业务方法，也可以包含具体业务方法。
    /// （2）RefinedAbstratction（扩充抽象类）：扩充由Abstraction定义的接口，通常情
    ///      况下他不再是抽象类而是具体类，实现了在Abstraction中声明的抽象业务方法，
    ///      在RefinedAbstraction中可以调用在Implementor中定义的业务方法。
    /// （3）Implementor（实现类接口）：定义实现类的接口，一般而言，它不与Abstraction的接口一致。
    ///      它只提供基本操作，而Abstraction定义的接口可能会做更多更复杂的操作。
    /// （4）ConcreteImplementor（具体实现类）：具体实现Implementor接口，
    ///      在不同的ConcreteImplementor中提供基本操作的不同实现，在程序运行时，
    ///      ConcreteImplentor将替换其父类对象，提供给抽象类具体的业务操作方法。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Artillery artillery = new Cannon();
            Ishell gasShell = new GasShell();
            artillery.SetShell(gasShell);
            artillery.Shooting();

            Artillery Mortar = new Mortar();
            Ishell mortarShell = new MortarShell();
            Mortar.SetShell(mortarShell);
            Mortar.Shooting();
        }
    }
}
