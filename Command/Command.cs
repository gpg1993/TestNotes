using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    public enum ExcuteState
    {
        Undo=0,
        Redo=1
    }

    /// <summary>
    /// 画线命令 
    /// </summary>
    public class DrawLineCommand : ICommand
    {
        public DrawLineCommand()
        {

        }

        public void Excute()
        {
            Console.WriteLine("画线");
        }
        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            Console.WriteLine("撤销线");
        }

        public override string ToString()
        {
            return "DrawLine";
        }
    }
    /// <summary>
    /// 画点命令 
    /// </summary>
    public class DrawPointCommand : ICommand
    {
        public DrawPointCommand()
        {

        }

        public void Excute()
        {
            Console.WriteLine("画点");
        }
        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            Console.WriteLine("撤销点");
        }

        public override string ToString()
        {
            return "DrawPoint";
        }
    }
    /// <summary>
    /// 画面命令 
    /// </summary>
    public class DrawPolygonCommand : ICommand
    {
        public DrawPolygonCommand()
        {

        }

        public void Excute()
        {
            Console.WriteLine("画面");
        }
        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            Console.WriteLine("撤销面");
        }

        public override string ToString()
        {
            return "DrawPolygon";
        }
    }
}
