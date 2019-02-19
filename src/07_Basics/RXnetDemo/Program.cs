using System;
using System.Reactive.Subjects;

namespace RXnetDemo
{
    /// <summary>
    /// Rx = Observables + LINQ + Schedulers。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            test1 test1 = new test1();
            //test1.Heater();
            //test1.create2();
            //test1.creatGenerate();

            test1.creatGenerate();
            Console.ReadKey();
            
        }
    }
}
