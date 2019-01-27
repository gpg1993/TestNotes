using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    /// <summary>
    /// 工作台（复杂产品）
    /// </summary>
    public class Workbench
    {
        /// <summary>
        /// 桌子
        /// </summary>
        public string Desk { get; set; }
        /// <summary>
        /// 椅子
        /// </summary>
        public string Chair { get; set; }
        /// <summary>
        /// 灯
        /// </summary>
        public string Lamp { get; set; }
        /// <summary>
        /// 插座
        /// </summary>
        public string Socket { get; set; }
        public ToolBox toolBox { get; set; }

    }
    /// <summary>
    /// 工具箱
    /// </summary>
    public class ToolBox
    {
        public string toolBoxName { get; set; }
        public int toolCount { get; set; }
    }

    
}
