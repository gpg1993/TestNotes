using System;

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
        }
    }

    // 定义Weapon基类
    public abstract class Weapon
    {
        protected string description;           // 武器的描述(攻击效果)
        public virtual string GetDescription()
        {
            return description;
        }
        public abstract int Damage();           // 武器的伤害
    }
    // 定义剑
    public class Sword : Weapon
    {
        public Sword()
        {
            description = "One-Hand light Sword";
        }
        public override int Damage()
        {
            return 15;
        }
    }
    // 定义宝石基类
    public abstract class Diamond : Weapon
    {
        protected Weapon weapon;        // 保存对武器的引用
    }

    // 定义蓝宝石
    public class BlueDiamond : Diamond
    {
        private int iceDamage = 2;          // 蓝宝石的额外伤害
        public BlueDiamond(Weapon weapon)
        {
            this.weapon = weapon;        // 保存引用
        }
        public string IceEffect()
        {
            return "\nAddtional Effect: Frozen !";
        }
        public override int Damage()
        {
            return weapon.Damage() + iceDamage; // 攻击加成
        }
        public override string GetDescription()
        {
            return weapon.GetDescription() + IceEffect();  // 加入特殊攻击效果
        }
    }

    // 定义红宝石
    public class RedDiamond : Diamond
    {
        private int fireDamage = 3;         // 蓝宝石的额外伤害
        public RedDiamond(Weapon weapon)
        {
            this.weapon = weapon;        // 保存引用
        }
        public string FireEffect()
        {
            return "\nAddtional Effect: Fire !";
        }
        public override int Damage()
        {
            return weapon.Damage() + fireDamage;    // 攻击加成
        }
        public override string GetDescription()
        {
            return weapon.GetDescription() + FireEffect(); // 加入特殊攻击效果
        }
    }
}
