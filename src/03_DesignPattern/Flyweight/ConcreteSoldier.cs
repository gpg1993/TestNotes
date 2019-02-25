using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Flyweight
{
    /// <summary>
    /// (红方士兵) 具体享元类
    /// </summary>
    public class RedSoldier : Soldier
    {
        public override StandType stand()
        {
            return StandType.red;
        }
    }
    /// <summary>
    /// (蓝方士兵) 具体享元类
    /// </summary>
    public class BlueSoldier : Soldier
    {
        public override StandType stand()
        {
            return StandType.blue;
        }
    }

    public enum StandType
    {
        [Description("蓝方士兵")]
        blue=0,
        [Description("红方士兵")]
        red =1
    }

}
