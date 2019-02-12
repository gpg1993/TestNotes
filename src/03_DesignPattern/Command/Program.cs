using System;

namespace Command
{
    class Program
    {
        /// <summary>
        /// 将一个请求封装为一个对象，从而可以用不同的请求对客户进行参数化；
        /// 对请求排队或者记录请求日志，以及支持可撤销的操作。
        /// 命令模式是一种对象行为型模式，其别名为动作（Action）模式或事物（Transaction）模式。
        /// （1）Command（抽象命令类）：一个抽象类或接口，声明了执行请求的Execute()方法，通过这些方法可以调用请求接收者的相关操作。
        /// （2）ConcreteCommand（具体命令类）：具体命令类是抽象命令类的子类，实现了抽象命令类中声明的方法。在实现Execute()方法时，将调用接收者对象的相关操作（Action）。
        /// （3）Invoker（调用者）：请求发送者，通过命令对象来执行请求。
        /// （4）Receiver（接收者）：接收者执行与请求相关的操作，它具体实现对请求的业务处理。
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
