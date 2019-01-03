using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    interface ICommand
    {
        void Excute();
        void Undo();
        
    }
}
