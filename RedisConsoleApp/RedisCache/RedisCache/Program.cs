using System;
using StackExchange.Redis;

namespace RedisCacheDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabase cache = lazyConnection.Value.GetDatabase();
            Console.WriteLine("Cache response : " + 
                cache.StringSet("Message", "Hello! The cache is working from a .NET Core console app!").ToString());

            Console.WriteLine("Cache response : " + cache.StringGet("Message").ToString());
            cache.ListLeftPush(new RedisKey("vclist"), new RedisValue("firstObject"));
            cache.ListLeftPush(new RedisKey("vclist"), new RedisValue("secondObject"));
            Console.WriteLine(cache.ListLeftPop(new RedisKey("vclist")).ToString()); 
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = "vc2021.redis.cache.windows.net:6380,password=5kcI2NiovJ3wzNmJMnQ3QEEgQ9URJz+ffWd3yYt50pM=,ssl=True,abortConnect=False";
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }

}
