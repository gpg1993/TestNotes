using System;

namespace Builder
{
    /// <summary>
    /// 将一个复杂对象的构建与它的表示相分离，使得同样的构建过程可以创建不同的表示。建造者模式是一种对象创建型模式。
    /// （1）Builder（抽象建造者）：为创建一个产品对象的各个部件指定抽象接口，在其接口中一般包含两类方法：一类是BuildPartX()，用于创建复杂对象的各个部件；另一类是GetResult()，用于返回生成好的复杂对象。它就可以是抽象类，也可以是接口。
    /// （2）ConcreteBuilder（具体建造者）：实现了Builder接口，即实现了各个部件的具体构造和装配方法，定义并明确其所创建的复杂对象。
    /// （3）Product（产品角色）：被构建的复杂对象，包含多个组成部件。
    /// （4）Director（指挥者）：负责安排复杂对象的建造次序，指挥者与抽象建造者之间存在关联关系，可以在其Construct()方法中调用建造者对象的部件构造和装配方法，完成复杂对象的建造。因此，客户端只需要和指挥者进行交互，这也确保了单一职责。
    /// </summary>
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
