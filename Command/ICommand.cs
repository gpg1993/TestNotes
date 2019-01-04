using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    /// <summary>
    /// 抽象命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 兼具 （重做）Redo
        /// </summary>
        void Excute();
        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();
    }
}
