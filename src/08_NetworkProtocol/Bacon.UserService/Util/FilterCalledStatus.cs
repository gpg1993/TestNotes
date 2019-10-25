using System;
using System.Collections.Generic;
using System.Text;

namespace Bacon.Service1.Util
{
    [Flags]
    public enum FilterCalledStatus
    {
        Begin   = 1,
        Catch   = 2,
        Finally = 4
    }
}
