using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Facade
{
    /// <summary>
    /// 这个地方用抽象类和接口类差不多。。。
    /// </summary>
    public abstract class AbstractEncryptFacade
    {
        public abstract void FileEncrypt(string fileName,string fileNameDes);
    }

    public interface IEncryptFacade
    {
        void FileEncrypt(string fileName, string fileNameDes);
    }
}
