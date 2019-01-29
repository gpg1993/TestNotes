using System;

namespace Command
{
    class Program
    {
        /// <summary>
        /// 将一个请求封装为一个对象，从而可以用不同的请求对客户进行参数化；
        /// 对请求排队或者记录请求日志，以及支持可撤销的操作。
        /// 命令模式是一种对象行为型模式，其别名为动作（Action）模式或事物（Transaction）模式。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CommandPool commandPool = new CommandPool(6);
            ICommand cmdPoint = new DrawPointCommand();
            ICommand cmdLine = new DrawLineCommand();
            ICommand cmdPolygon = new DrawPolygonCommand();
            VirtualWindow window = new VirtualWindow("功能键设置窗口");
            FuncBtn funcBtn1 = new FuncBtn("点");
            FuncBtn funcBtn2 = new FuncBtn("线");
            FuncBtn funcBtn3 = new FuncBtn("面");
            //FuncBtn funcBtn4 = new FuncBtn("撤销");
            //FuncBtn funcBtn5 = new FuncBtn("重做");

            funcBtn1.SetCmd(cmdPoint);
            funcBtn2.SetCmd(cmdLine);
            funcBtn3.SetCmd(cmdPolygon);
            
            window.AddFuncBtn(funcBtn1);
            window.AddFuncBtn(funcBtn2);
            window.AddFuncBtn(funcBtn3);
            //window.AddFuncBtn(funcBtn4);
            //window.AddFuncBtn(funcBtn5);
            window.Display();

            funcBtn1.onClick(commandPool);
            funcBtn1.onClick(commandPool);
            funcBtn1.onClick(commandPool);
            funcBtn2.onClick(commandPool);
            funcBtn2.onClick(commandPool);
            funcBtn3.onClick(commandPool);
            funcBtn3.onClick(commandPool);
            commandPool.Undo();
            commandPool.Undo();
            commandPool.Undo();
            commandPool.Undo();
            commandPool.Undo();
            commandPool.Redo();
            funcBtn3.onClick(commandPool);
            Console.ReadKey();
        }
    }
}
