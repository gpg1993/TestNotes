using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class HttpServer
    {
        /// <summary>
        /// 服务器名称唯一
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 服务器处理容量
        /// </summary>
        public string ServerCapacity { get; set; }
    }
}
