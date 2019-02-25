using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    // 定义宝石基类（抽象装饰者）
    public abstract class Diamond : Weapon
    {
        protected Weapon weapon;        // 保存对武器的引用
    }

    // 定义蓝宝石（具体装饰者）
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

    // 定义红宝石（具体装饰者）
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
