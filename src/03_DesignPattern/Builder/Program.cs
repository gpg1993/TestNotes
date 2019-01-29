using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkbenchController workbenchController = new WorkbenchController();
            WorkbenchBuilder workbenchBuilder = new ComputerWorkBench();
            Workbench workbench = workbenchController.GetWorkbench(workbenchBuilder);
            Console.WriteLine($"工作台{workbench.Desk}");
            Console.WriteLine($"工作台{workbench.Chair}");
            //.......
        }
    }
    /// <summary>
    /// 控制建造者
    /// </summary>
    public class WorkbenchController
    {
        public Workbench GetWorkbench(WorkbenchBuilder workbenchBuilder)
        {
            workbenchBuilder.buildChair();
            workbenchBuilder.buildDesk();
            workbenchBuilder.buildLamp();
            workbenchBuilder.buildSocket();
            workbenchBuilder.buildtoolBox();
            return workbenchBuilder.BuildWorkbench();
        }
    }
}
