using CSRedis;
using System;

namespace CSRedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneralTest generalTest = new GeneralTest();
            CSRedisClient client = generalTest.CommonRedis();
            RedisHelper.Initialization(client);
            RedisHelper.Set()
            Console.WriteLine("Hello World!");
        }
    }
}
