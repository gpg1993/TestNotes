using System;
using System.Collections.Generic;
using System.Text;

namespace Factory
{
    /// <summary>
    /// 定义一个用于创建对象的接口，让子类决定将哪一个类实例化。
    /// 工厂方法模式让一个类的实例化延迟到其子类。
    /// 工厂方法模式又简称为工厂模式，也可称为多态工厂模式，它是一种创建型模式。　
    /// 隐藏具体实现类
    /// </summary>
    public abstract class GeneralFacorty
    {
        // 在工厂类中直接调用烹饪方法
        public void Cooking()
        {
            IKitchen kitchen = this.CreateCook();
            kitchen.cook();
        }

        public abstract IKitchen CreateCook();
    }

    public class GBJDFactory : GeneralFacorty
    {
        public override IKitchen CreateCook()
        {
            IKitchen kitchen = new GBJDCook();
            return kitchen;
        }
    }

    public class YXRSFactory : GeneralFacorty
    {
        public override IKitchen CreateCook()
        {
            IKitchen kitchen = new YXRSCook();
            return kitchen;
        }
    }
    
}
