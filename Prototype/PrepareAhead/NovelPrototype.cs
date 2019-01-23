using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.PrepareAhead
{
    /// <summary>
    /// 建立小说原型（小说三要素：人物，事件，情景）
    /// </summary>
    public abstract class NovelPrototype
    {
        public List<NovelCharacter> NovelCharacterList { get; set; }
        public string NovelEvent { get; set; }
        public string NovelScene { get; set; }

        public abstract NovelPrototype Clone();
    }
    public class NovelCharacter
    {
        public string NovelCharacterName { get; set; }
        public string NovelCharacterAge { get; set; }
        public int NovelCharacterIsLead { get; set; }
    }
}
