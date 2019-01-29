using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    public class FuncBtn
    {
        public string Name { get; set; }
        public ICommand command;
        public FuncBtn(string name)
        {
            this.Name = name;
        }
        public void SetCmd(ICommand command)
        {
            this.command = command;  
        }
        public void onClick(CommandPool pool)
        {
            if (command!=null)
            {
                pool.Register(command);
                command.Excute();
            }
        }
    }
}
