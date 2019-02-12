using System;

namespace Composite
{
    /// <summary>
    /// 组合（Composite）模式：组合多个对象形成树形结构以表示具有“整体-部分”关系的层次结构。
    /// 组合模式对单个对象（即叶子对象）和组合对象（即容器对象）的使用具有一致性，
    /// 组合模式又可以称为“部分-整体”（Part-Whole）模式，它是一种对象结构型模式。　
    /// </summary>
    class Program
    {
        /// <summary>
        /// 优点：
        /// （1）可以清楚地定义分层次的复杂对象，表示对象的全部或部分层次，使客户忽略了层次的差异，
        ///      方便对整个层次结构进行控制。
        /// （2）增加新的容器构件和叶子构件都十分方便，无需对现有类库代码进行任何修改，符合开闭原则。
        /// （3）为树形结构的面向对象实现提供了灵活地解决方案，可以形成复杂的树形结构，但对树形结构的控制却很简单。
        /// 缺点：
        /// 增加新构件时很难对容器中的构建类型进行限制。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            AbstractFile folder1 = new Folder("EDC的资料");
            AbstractFile folder2 = new Folder("图像文件");
            AbstractFile folder3 = new Folder("文本文件");
            AbstractFile folder4 = new Folder("视频文件");

            AbstractFile image1 = new ImageFile("小龙女.jpg");
            AbstractFile image2 = new ImageFile("张无忌.gif");

            AbstractFile text1 = new TextFile("九阴真经.txt");
            AbstractFile text2 = new TextFile("葵花宝典.doc");

            AbstractFile video1 = new VideoFile("笑傲江湖.rmvb");
            AbstractFile video2 = new VideoFile("天龙八部.mp4");

            folder2.Add(image1);
            folder2.Add(image2);

            folder3.Add(text1);
            folder3.Add(text2);

            folder4.Add(video1);
            folder4.Add(video2);

            folder1.Add(folder2);
            folder1.Add(folder3);
            folder1.Add(folder4);

            folder1.KillVirus();
        }
    }
}
