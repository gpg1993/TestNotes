using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SocketBase
{
    /// <summary>
    /// 数据转换
    /// </summary>
    public class ObjectInversion
    {
        public ObjectInversion()
        { }

        public byte[] SerializeTo(object source)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, source);
                return memoryStream.ToArray();
            }
        }

        public object DeSerializeTo(byte[] bytes)
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            object obj = formatter.Deserialize(stream);
            return obj;
        }
    }
}
