using System;

namespace Mediator
{
    /// <summary>
    /// （1）Mediator（抽象中介者）：它定义了一个接口，该接口用于与各同事对象之间进行通信。
    /// （2）ConcreteMediator（具体中介者）：它实现了接口，通过协调各个同事对象来实现协作行为，维持了各个同事对象的引用。
    /// （3）Colleague（抽象同事类）：它定义了各个同事类公有的方法，并声明了一些抽象方法来供子类实现，同时维持了一个对抽象中介者类的引用，其子类可以通过该引用来与中介者通信。
    /// （4）ConcreteColleague（具体同事类）：抽象同事类的子类，每一个同事对象需要和其他对象通信时，都需要先与中介者对象通信，通过中介者来间接完成与其他同事类的通信。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Step1.定义中介者对象
            ConcreteMediator mediator = new ConcreteMediator();

            // Step2.定义同事对象
            Button addButton = new Button();
            List list = new List();
            ComboBox cb = new ComboBox();
            TextBox userNameTextBox = new TextBox();

            addButton.SetMediator(mediator);
            list.SetMediator(mediator);
            cb.SetMediator(mediator);
            userNameTextBox.SetMediator(mediator);

            mediator.addButton = addButton;
            mediator.list = list;
            mediator.cb = cb;
            mediator.userNameTextBox = userNameTextBox;

            // Step3.点击增加按钮
            addButton.Changed();

            Console.WriteLine("---------------------------------------------");

            // Step4.从列表框选择客户
            list.Changed();
        }
    }
}
