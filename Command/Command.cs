using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
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
            
        }

        public override string ToString()
        {
            return "DrawLine";
        }
    }
}
