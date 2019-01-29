using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 抽象工厂模式提供一个创建一系列相关或相互依赖对象的接口，而无须指定它们具体的类。抽象工厂模式又称为Kit模式，它是一种对象创建型模式。
    /// </summary>
    public class AbstractFactoty
    {

    }
    public interface IButton
    {
        void Display();
    }

    public interface ITextField
    {
        void Display();
    }

    public interface IComboBox
    {
        void Display();
    }

    #region spring
    public class SpringButton : IButton
    {
        public void Display()
        {
            Console.WriteLine("显示浅绿色按钮...");
        }
    }
    public class SpringTextField : ITextField
    {
        public void Display()
        {
            Console.WriteLine("显示绿色边框文本框...");
        }
    }
    public class SpringComboBox : IComboBox
    {
        public void Display()
        {
            Console.WriteLine("显示绿色边框下拉框...");
        }
    }
    #endregion

    #region summer
    public class SummerButton : IButton
    {
        public void Display()
        {
            Console.WriteLine("显示浅蓝色按钮...");
        }
    }
    public class SummerTextField : ITextField
    {
        public void Display()
        {
            Console.WriteLine("显示蓝色边框文本框...");
        }
    }
    public class SummerComboBox : IComboBox
    {
        public void Display()
        {
            Console.WriteLine("显示蓝色边框下拉框...");
        }
    }
    #endregion

    public interface ISkinFactory
    {
        IButton CreateButton();
        ITextField CreateTextField();
        IComboBox CreateComboBox();
    }

    // Spring皮肤工厂
    public class SpringSkinFactory : ISkinFactory
    {
        public IButton CreateButton()
        {
            return new SpringButton();
        }

        public IComboBox CreateComboBox()
        {
            return new SpringComboBox();
        }

        public ITextField CreateTextField()
        {
            return new SpringTextField();
        }
    }
    //Summer皮肤工厂
    public class SummerSkinFactory : ISkinFactory
    {
        public IButton CreateButton()
        {
            return new SummerButton();
        }

        public IComboBox CreateComboBox()
        {
            return new SummerComboBox();
        }

        public ITextField CreateTextField()
        {
            return new SummerTextField();
        }
    }


}
