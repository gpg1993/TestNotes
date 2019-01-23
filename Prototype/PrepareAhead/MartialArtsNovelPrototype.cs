using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.PrepareAhead
{
    public class MartialArtsNovelPrototype : ICloneable
    {
        public List<NovelCharacter> NovelCharacterList { get; set; }
        public string NovelEvent { get; set; }
        public string NovelScene { get; set; }
        public object Clone()
        {
            //深度复制(通过实例化新的对象)
            MartialArtsNovelPrototype obj = new MartialArtsNovelPrototype();
            obj.NovelCharacterList = this.NovelCharacterList;
            obj.NovelEvent = this.NovelEvent;
            obj.NovelScene = this.NovelScene;
            return obj;

            //实现浅复制
            //return base.MemberwiseClone();
        }
    }
}
