using Prototype.PrepareAhead;
using System;
using System.Collections.Generic;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            RomanticNovelPrototype romanticNovelPrototype = new RomanticNovelPrototype();
            List<NovelCharacter> NovelCharacterlist = new List<NovelCharacter>();
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 21, NovelCharacterIsLead = 1, NovelCharacterName = "女1" });
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 40, NovelCharacterIsLead = 1, NovelCharacterName = "男1" });
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 21, NovelCharacterIsLead = 0, NovelCharacterName = "女2" });
            romanticNovelPrototype.NovelCharacterList = NovelCharacterlist;
            romanticNovelPrototype.NovelEvent = "西湖断桥偶遇";
            romanticNovelPrototype.NovelScene = "三角恋关系";

            RomanticNovelPrototype romanticNovelPrototype1 = romanticNovelPrototype.Clone() as RomanticNovelPrototype;
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 21, NovelCharacterIsLead = 1, NovelCharacterName = "女1" });
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 40, NovelCharacterIsLead = 1, NovelCharacterName = "男1" });
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 21, NovelCharacterIsLead = 0, NovelCharacterName = "路人甲" });
            NovelCharacterlist.Add(new NovelCharacter { NovelCharacterAge = 21, NovelCharacterIsLead = 0, NovelCharacterName = "路人乙" });
            romanticNovelPrototype.NovelCharacterList = NovelCharacterlist;
            romanticNovelPrototype.NovelEvent = "咖啡厅相遇";
            romanticNovelPrototype.NovelScene = "破解重圆";


        }
    }
}
