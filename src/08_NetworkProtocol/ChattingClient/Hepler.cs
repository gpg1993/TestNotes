using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingClient
{
    public static class Hepler
    {
        public static void Append(this TextBox txtBox, string msg)
        {
            try
            {
                if (!txtBox.IsHandleCreated)
                {
                    txtBox.CreateControl();
                }
                txtBox.Invoke(new Action(() => { txtBox.AppendText(msg); }));
                txtBox.Invoke(new Action(() => { txtBox.AppendText("\n"); }));
            }
            catch { }
        }
    }
}
