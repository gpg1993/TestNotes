using Prototype.PrepareAhead;
using System;
using System.Collections.Generic;

namespace Prototype
{
    /// <summary>
    /// 使用原型实例指定创建对象的种类，并且通过拷贝这些原 型创建新的对象。原型模式是一种对象创建型模式。
    /// ● Prototype（抽象原型类）：它是声明克隆方法的接口，是所有具体原型类的公共父类，可以是抽象类也可以是接口，甚至还可以是具体实现类。 
    /// ● ConcretePrototype（具体原型类）：它实现在抽象原型类中声明的克隆方法，在克隆方法中返回自己的一个克隆对象 
    /// ● Client（客户类）：让一个原型对象克隆自身从而创建一个新的对象，在客户类中只需要直接实例化或通过工厂方法等方式创建一个原型对象，再通过调用该对象的克隆方法即可得到多个相同的对象。由于客户类针对抽象原型类Prototype编程，因此用户可以根据需要选择具体原型类，系统具有较好的可扩展性，增加或更换具体原型类都很方便。
    /// </summary>
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
