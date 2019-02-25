using System;
using System.Collections.Generic;
using System.Text;

namespace Mediator
{
    /// <summary>
    /// 抽象中介者
    /// </summary>
    public abstract class Mediator
    {
        public abstract void ComponenetChanged(Component c);
    }
    /// <summary>
    /// 具体中介者
    /// </summary>
    public class ConcreteMediator : Mediator
    {
        // 维持对各个同事对象的引用
        public Button addButton;
        public List list;
        public TextBox userNameTextBox;
        public ComboBox cb;

        // 封装同事对象之间的交互
        public override void ComponenetChanged(Component c)
        {
            // 单击按钮
            if (c == addButton)
            {
                Console.WriteLine("-- 单击增加按钮 --");
                list.Update();
                cb.Update();
                userNameTextBox.Update();
            }
            // 从列表框选择客户
            else if (c == list)
            {
                Console.WriteLine("-- 从列表框选择客户 --");
                cb.Select();
                userNameTextBox.SetText();
            }
            // 从组合框选择客户
            else if (c == cb)
            {
                Console.WriteLine("-- 从组合框选择客户 --");
                cb.Select();
                userNameTextBox.SetText();
            }
        }
    }
    public class SubConcreteMediator : ConcreteMediator
    {
        // 增加对Label对象的引用
        public Label label;

        public override void ComponenetChanged(Component c)
        {
            // 单击按钮
            if (c == addButton)
            {
                Console.WriteLine("-- 单击增加按钮 --");
                list.Update();
                cb.Update();
                userNameTextBox.Update();
                // 文本标签更新
                label.Update();
            }
            // 从列表框选择客户
            else if (c == list)
            {
                Console.WriteLine("-- 从列表框选择客户 --");
                cb.Select();
                userNameTextBox.SetText();
            }
            // 从组合框选择客户
            else if (c == cb)
            {
                Console.WriteLine("-- 从组合框选择客户 --");
                cb.Select();
                userNameTextBox.SetText();
            }
        }
    }
}
