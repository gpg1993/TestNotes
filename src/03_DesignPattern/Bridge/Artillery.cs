using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge
{
    /// <summary>
    /// 抽象大炮
    /// </summary>
    public abstract class Artillery
    {
        public Ishell shell;
        public void SetShell(Ishell shell)
        {
            this.shell = shell;
        }
        public abstract void Shooting();
    }
    public class Cannon : Artillery
    {
        public override void Shooting()
        {
            shell.Blast();
            Console.WriteLine("加农炮");
        }
    }
    public class Mortar : Artillery
    {
        public override void Shooting()
        {
            shell.Blast();
            Console.WriteLine("迫击炮");
        }
    }
    /// <summary>
    /// 炮弹接口
    /// </summary>
    public interface Ishell
    {
        void Blast();
    }
    public class GasShell : Ishell
    {
        public void Blast()
        {
            Console.WriteLine("毒气弹");
        }
    }
    public class MortarShell : Ishell
    {
        public void Blast()
        {
            Console.WriteLine("迫击炮弹");
        }
    }
}
