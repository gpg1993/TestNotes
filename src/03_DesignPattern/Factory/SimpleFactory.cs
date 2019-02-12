using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 定义一个工厂类，它可以根据参数的不同返回不同类的实例，被创建的实例通常都具有共同的父类。
    /// 因为在简单工厂模式中用于创建实例的方法是静态（static）方法，因此简单工厂模式又被称为静态工厂方法模式，它属于创建型模式。
    /// Factory - 工厂角色：该模式的核心，负责实现创建所有产品实例的内部逻辑，提供一个静态的工厂方法GetProduct()，返回抽象产品类型Product的实例。
    /// Product - 抽象产品角色：所有产品类的父类，封装了各种产品对象的共有方法，它的引入将提高系统的灵活性，使得在工厂类中只需要定义一个通用的工厂方法，因为所有创建的具体产品对象都是其子类对象。
    /// ConcreteProduct - 具体产品角色：简单工厂模式的创建目标，所有被创建的对象都充当这个角色的某个具体类的实例。
    /// </summary>
    public class SimpleFactory
    {
        public static IKitchen GetCook(CookName CookName)
        {
            IKitchen kitchen = null;
            switch (CookName)
            {
                case CookName.鱼香肉丝:
                    kitchen = new YXRSCook();
                    break;
                case CookName.宫保鸡丁:
                    kitchen = new GBJDCook();
                    break;
                default:
                    break;
            }
            return kitchen;
        }
    }
    public enum CookName
    {
        鱼香肉丝 = 1,
        宫保鸡丁 = 2
    }
    public interface IKitchen
    {
        void cook();
    }
    public interface ISalad
    {
        void Salad();
    }
    public class GBJDCook : IKitchen
    {
        public GBJDCook()
        {
            Console.WriteLine("宫保鸡丁");
        }
        public void cook()
        {
            Console.WriteLine("炒宫保鸡丁");
        }
    }
    public class YXRSCook : IKitchen
    {
        public YXRSCook()
        {
            Console.WriteLine("鱼香肉丝");
        }
        public void cook()
        {
            Console.WriteLine("炒鱼香肉丝");
        }
    }
}
