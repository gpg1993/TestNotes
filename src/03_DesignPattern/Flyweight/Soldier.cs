using System;
using System.Collections.Generic;
using System.Text;

namespace Flyweight
{
    /// <summary>
    /// 士兵 抽象享元类
    /// </summary>
    public abstract class Soldier
    {
        /// <summary>
        /// 士兵立场
        /// </summary>
        /// <returns></returns>
        public abstract StandType stand();
        /// <summary>
        /// 进攻
        /// </summary>
        /// <param name="target"></param>
        public void attack(Target target)
        {
            Console.WriteLine($"进攻目标：{target.TargetName}，坐标为x:{target.x}   y:{target.y}");
        }
        /// <summary>
        /// 防守
        /// </summary>
        /// <param name="target"></param>
        public void Defense(Target target)
        {
            Console.WriteLine($"防御目标：{target.TargetName}，坐标为x:{target.x}   y:{target.y}");
        }
    }
    /// <summary>
    /// 目标 非共享具体享元类
    /// </summary>
    public class Target
    {
        public double x { get; set; }
        public double y { get; set; }
        public string TargetName { get; set; }
    }
}
