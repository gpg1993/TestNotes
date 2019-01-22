using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 定义一个工厂类，它可以根据参数的不同返回不同类的实例，被创建的实例通常都具有共同的父类。
    /// 因为在简单工厂模式中用于创建实例的方法是静态（static）方法，因此简单工厂模式又被称为静态工厂方法模式，它属于创建型模式。
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
