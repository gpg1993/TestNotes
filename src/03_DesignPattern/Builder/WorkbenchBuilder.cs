using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    /// <summary>
    /// 抽象工作台
    /// </summary>
    public abstract class WorkbenchBuilder
    {
        protected Workbench workbench = new Workbench();

        public abstract void buildDesk();
        public abstract void buildChair();
        public abstract void buildLamp();
        public abstract void buildSocket();
        public abstract void buildtoolBox();

        public Workbench BuildWorkbench()
        {
            return workbench;
        }
    }
    /// <summary>
    /// 电脑工作台（具体工作台）
    /// </summary>
    public class ComputerWorkBench : WorkbenchBuilder
    {
        public override void buildChair()
        {
            workbench.Chair = "电脑椅";
        }

        public override void buildDesk()
        {
            workbench.Desk = "电脑桌";
        }

        public override void buildLamp()
        {
            workbench.Desk = "电脑桌灯";
        }

        public override void buildSocket()
        {
            workbench.Desk = "网线插座";
        }

        public override void buildtoolBox()
        {
            ToolBox toolBox = new ToolBox();
            toolBox.toolBoxName = "电脑组合";
            toolBox.toolCount = 12;
            workbench.toolBox = toolBox;
        }
    }
    /// <summary>
    /// 服装工作台（具体工作台）
    /// </summary>
    public class clothingWorkBench : WorkbenchBuilder
    {
        public override void buildChair()
        {
            workbench.Chair = "服装工作椅子";
        }

        public override void buildDesk()
        {
            workbench.Desk = "服装工作桌";
        }

        public override void buildLamp()
        {
            workbench.Lamp = "服装工作灯";
        }

        public override void buildSocket()
        {
            workbench.Socket = "服装工作插座";
        }

        public override void buildtoolBox()
        {
            ToolBox toolBox = new ToolBox();
            toolBox.toolBoxName = "缝纫机，熨斗等";
            toolBox.toolCount = 12;
            workbench.toolBox = toolBox;
        }
    }
}
