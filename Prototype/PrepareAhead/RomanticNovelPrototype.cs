using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.PrepareAhead
{
    public class RomanticNovelPrototype : NovelPrototype
    {
        public override NovelPrototype Clone()
        {
            NovelPrototype novelPrototype = new RomanticNovelPrototype();
            novelPrototype.NovelCharacterList = this.NovelCharacterList;
            novelPrototype.NovelEvent = this.NovelEvent;
            novelPrototype.NovelScene = this.NovelScene;
            return novelPrototype;
        }
    }
}
