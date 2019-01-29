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
        public int NovelCharacterAge { get; set; }
        /// <summary>
        /// 是否主角（0：不是，1：是）
        /// </summary>
        public int NovelCharacterIsLead { get; set; }
    }
}
