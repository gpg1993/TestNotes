using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SocketBase
{
    /// <summary>
    /// 聊天状态枚举
    /// </summary>
    public enum ChatStateEnum
    {
        [Description("上线")]
        OnLine = 0,
        [Description("说话")]
        Message = 1,
        [Description("传输文件")]
        File = 2,
        [Description("下线")]
        OffLine = 3,
        [Description("心跳包")]
        Heartbeat = 4,
        [Description("检查下线")]
        CheckOffLine = 5
    }
}
