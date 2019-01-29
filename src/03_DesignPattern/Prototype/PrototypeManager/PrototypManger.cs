using Prototype.PrepareAhead;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.PrototypeManager
{
    public class PrototypManager
    {
        private List<MartialArtsNovelPrototypeEntiy> lists = new List<MartialArtsNovelPrototypeEntiy>();
        class Nested
        {
            static Nested() { }
            internal static readonly PrototypManager intance = new PrototypManager();
        }

        public PrototypManager GetIntance()
        {
            return Nested.intance;
        }

        public void AddMartialArtsNovel(MartialArtsNovelPrototypeEntiy entiy)
        {
            lists.Add(entiy);
        }
        public MartialArtsNovelPrototypeEntiy GetMartialArtsNovel(string MartialArtsNovelName)
        {
            MartialArtsNovelPrototypeEntiy martialArtsNovelPrototypeEntiy = null;
            foreach (var item in lists)
            {
                if (item.MartialArtsNovelName== MartialArtsNovelName)
                {
                    martialArtsNovelPrototypeEntiy = item;
                    break;
                }
            }
            return martialArtsNovelPrototypeEntiy;
        }
    }

    public class MartialArtsNovelPrototypeEntiy
    {
        public string MartialArtsNovelName { get; set; }
        public MartialArtsNovelPrototype martialArtsNovelPrototype { get; set; }
    }
}
