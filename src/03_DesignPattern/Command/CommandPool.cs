using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    /// <summary>
    /// 命令池
    /// </summary>
    public class CommandPool
    {
        #region fields
        private Stack<ICommand> toRedoStack;//重做栈
        private int maxCommandCount;//命令池最大命令量
        private Deque<ICommand> toUndoDeque;//撤销双端队列
        #endregion

        #region properties
        /// <summary>
        /// 命令总数
        /// </summary>
        public int TotalCommandCount
        {
            get
            {
                return toUndoDeque.Count;
            }
        }
        /// <summary>
        /// 撤销总数
        /// </summary>
        public int TotalUndoCommandCount
        {
            get
            {
                return toUndoDeque.Count;
            }
        }
        /// <summary>
        /// 重做总数
        /// </summary>
        public int TotalRedoCommandCount
        {
            get
            {
                return toRedoStack.Count;
            }
        }
        #endregion

        #region constructors
        public CommandPool()
        {
            toRedoStack = new Stack<ICommand>();
            toUndoDeque = new Deque<ICommand>();
            this.maxCommandCount = 1;
        }

        public CommandPool(int maxCommandCount)
        {
            toRedoStack = new Stack<ICommand>();
            toUndoDeque = new Deque<ICommand>(maxCommandCount);
            this.maxCommandCount = maxCommandCount;
        }
        #endregion

        #region methods
        public void Register(ICommand command)
        {
            toRedoStack.Clear();
            if (toUndoDeque.Count==maxCommandCount)
            {
                toUndoDeque.RemoveHead();
            }
            toUndoDeque.AddTail(command);
        }

        public void Undo()
        {
            if (toUndoDeque.Count == 0)
            {
                return;
            }
            ICommand command = toUndoDeque.RemoveTail();
            command.Undo();
            toRedoStack.Push(command);
        }

        public void Redo()
        {
            if (toRedoStack.Count == 0)
            {
                return;
            }

            ICommand command = toRedoStack.Pop();
            command.Excute();
            toUndoDeque.AddTail(command);
        }
        public string GetNextUndoCommandInfo()
        {
            ICommand command = toUndoDeque.GetTail();

            if (command == null)
            {
                return "no command";
            }

            return command.ToString();
        }
        public string GetNextRedoCommandInfo()
        {
            if (toRedoStack.Count == 0)
            {
                return "no command";
            }

            ICommand command = toRedoStack.Peek();
            return command.ToString();
        }
        public override string ToString()
        {
            return "this pool has " + toRedoStack.Count + " to redo command left and " + toUndoDeque.Count + " to undo command left";
        }
        #endregion
    }
}
