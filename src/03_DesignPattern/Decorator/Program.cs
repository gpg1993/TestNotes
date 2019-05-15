using System;
using System.Collections.Generic;

namespace Decorator
{
    /// <summary>
    /// 装饰（Decorator）模式：动态地给一个对象增加一些额外的职责，就增加对象功能来说，
    /// 装饰模式远比生成子类实现更加灵活。装饰模式是一种对象结构型模式
    /// （1）Component （抽象构件）：具体构件和抽象装饰类的基类，声明了在具体构建中实现的业务方法。
    /// （2）ConcreteComponent（具体构件）：抽象构件的子类，用于定义具体的构件对象，实现了在抽象构件中声明的方法，装饰器可以给它增加额外的职责（方法）。
    /// （3）Decorator（抽象装饰类）：它也是抽象构件类的子类，用于给具体构件增加职责，但是具体职责在其子类中实现。
    /// （4）ConcreteDecorator（具体装饰类）：抽象装饰类的子类，负责向构件添加新的职责。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Weapon sword = new Sword();     // 创建一把新剑
                                            // 打印其描述(攻击效果) 和 伤害
            Console.WriteLine(sword.GetDescription() + "\nDamage:" + sword.Damage());
            Console.WriteLine();

            sword = new BlueDiamond(sword); // 给剑添加一颗蓝宝石
            Console.WriteLine(sword.GetDescription() + "\nDamage:" + sword.Damage());
            Console.WriteLine();

            sword = new RedDiamond(sword);  // 给剑添加一颗红宝石
            Console.WriteLine(sword.GetDescription() + "\nDamage:" + sword.Damage());
            Console.WriteLine();


            BaseProduct latiao = new LaTiaoProduct("1", "辣条", 25);
            BaseProduct laoganma = new LaoGanMaProduct("2", "老干妈", 38);

            latiao = new DisCountActivity(0.8, latiao);
            List<(double, double)> rductions = new List<(double, double)>();
            rductions.Add((30, 2));
            rductions.Add((50, 4));
            rductions.Add((70, 7));
            latiao = new FullRductionActivity(rductions, latiao);
            //被装饰后原属性不会被继承，只是动态增加装饰的一项。
            Console.WriteLine($"打折商品{latiao.Name},编号{latiao.Id},原价{latiao.OriginalPrice},现价{latiao.ProductPrice()}");

            laoganma = new DisCountActivity(0.8, laoganma);
            laoganma = new FullRductionActivity(rductions, laoganma);
            Console.WriteLine($"打折商品{laoganma.Name},编号{laoganma.Id},原价{laoganma.OriginalPrice},现价{laoganma.ProductPrice()}");

            ProductMuti products = new ProductMuti();
            products.Add(new LaoGanMaProduct("3", "老干妈", 38));
            products.Add(new LaoGanMaProduct("4", "老干妈", 68));
            products.Add(new LaoGanMaProduct("5", "老干妈", 58));
            products.Add(new LaoGanMaProduct("6", "老干妈", 48));
            products.Add(new LaTiaoProduct("7", "辣条", 45));
            products.Add(new LaTiaoProduct("8", "辣条", 35));
            products.Add(new LaTiaoProduct("9", "辣条", 25));

            ProductMuti price1 = new FullRductionActivityMuti(rductions, products);
            ProductMuti price2 = new FullRductionActivityMuti(rductions, price1);
            Console.WriteLine($"价格{price2.ProductsPrice()}");
            Console.ReadKey();
        }
    }
}
