using CSRedis;
using Microsoft.Extensions.Caching.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSRedisTest
{
    public class GeneralTest
    {
        #region 普通模式
        public CSRedisClient CommonRedis()
        {
           return new CSRedisClient("127.0.0.1:6379,password=svse,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240,prefix=key前辍");
        }

        public void setString()
        {
            var csredis = CommonRedis();
            RedisHelper.Initialization(csredis);
            //RedisHelper.
        }
        #endregion

        #region 集群模式
        public CSRedisClient ClusterRedis()
        {
            var csredis = new CSRedis.CSRedisClient(null,
              "127.0.0.1:6371,password=123,defaultDatabase=11,poolsize=10,ssl=false,writeBuffer=10240,prefix=key前辍",
              "127.0.0.1:6372,password=123,defaultDatabase=12,poolsize=11,ssl=false,writeBuffer=10240,prefix=key前辍",
              "127.0.0.1:6373,password=123,defaultDatabase=13,poolsize=12,ssl=false,writeBuffer=10240,prefix=key前辍",
              "127.0.0.1:6374,password=123,defaultDatabase=14,poolsize=13,ssl=false,writeBuffer=10240,prefix=key前辍");
            //实现思路：根据key.GetHashCode() % 节点总数量，确定连向的节点
            //也可以自定义规则(第一个参数设置)
            return csredis;
        }
        #endregion

        #region 缓存
        public void CacheRedis()
        {
            var csredis = new CSRedisClient("127.0.0.1:6379,password=svse,defaultDatabase=0,poolsize=50,ssl=false,writeBuffer=10240,prefix=key前辍");
            CSRedisCache redisCache = new CSRedisCache(csredis);
        }
        #endregion
    }
}
