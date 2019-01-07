using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    public class VirtualWindow
    {
        public String Title { get; set; }
        private IList<FuncBtn> funcBtns = new List<FuncBtn>();
        public VirtualWindow(string title)
        {
            this.Title = title;
        }
        public void AddFuncBtn(FuncBtn funcBtn)
        {
            funcBtns.Add(funcBtn);
        }
        public void RemoveFuncBtn(FuncBtn funcBtn)
        {
            funcBtns.Remove(funcBtn);
        }
        public void Display()
        {
            Console.WriteLine("显示窗口：{0}", this.Title);
            Console.WriteLine("显示功能键：");
            foreach (var fb in funcBtns)
            {
                Console.WriteLine(fb.Name);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}
