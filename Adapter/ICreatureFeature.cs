using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    /// <summary>
    /// 动物特征
    /// </summary>
    public interface ICreatureFeature
    {
        /// <summary>
        /// 奔跑
        /// </summary>
        void Runing();
        /// <summary>
        /// 攻击
        /// </summary>
        void attacking();
        /// <summary>
        /// 呼吸
        /// </summary>
        void Breathing();

    }

    public class CreatureFeature : ICreatureFeature
    {
        public void attacking()
        {
            Console.WriteLine("攻击");
        }

        public void Breathing()
        {
            Console.WriteLine("呼吸");
        }

        public void Runing()
        {
            Console.WriteLine("奔跑");
        }
    }
}
