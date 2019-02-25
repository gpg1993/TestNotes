using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    // 定义Weapon基类（抽象被装饰者）
    public abstract class Weapon
    {
        protected string description;           // 武器的描述(攻击效果)
        public virtual string GetDescription()
        {
            return description;
        }
        public abstract int Damage();           // 武器的伤害
    }


    // 定义剑（具体被装饰者）
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
}
