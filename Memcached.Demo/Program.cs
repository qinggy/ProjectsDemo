using Enyim.Caching;
using MemcachedProviders.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Memcached.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务端缓存通行证数据10秒
            DistCache.DefaultExpireTime = 2;

            //假如用户登录成功  
            User user = new User();
            user.Name = "小怜香";
            user.Age = 20;
            user.Birthday = Convert.ToDateTime("1991-1-1");
            user.Sex = false;
            user.IsLoginSucess = true;

            //生成通过证  
            string passCode = Guid.NewGuid().ToString();
            Console.WriteLine("首次缓存数据-------------------8秒后读取");
            //缓存用户信息(10秒) ，并将PassCode保存到Cookie中  
            //DistCache.Add(passCode, user, true);
            MemcachedClient client = new MemcachedClient("enyim.com/memcached");
            //设置过期时间
            //client.Store(Enyim.Caching.Memcached.StoreMode.Add, passCode, user, new DateTime(2016, 10, 17, 9, 3, 0));

            
            //如何让用户在操作期间 缓存不过期呢？

            Thread.Sleep(8000);
            //Console.WriteLine(DistCache.Get<User>("ds").Name);
            Console.WriteLine(client.Get<User>(passCode).Name);

            Console.WriteLine("重新缓存--------------9秒后再读取");
            //从Cookie中获取PassCode ，通过PassCode从缓存中读取用户数据后重新缓存   
            //User newUser = DistCache.Get<User>(passCode);
            //DistCache.Remove(passCode);
            //方式一 Set才会重新刷新缓存
            //DistCache.Add(passCode, user, DistCache.DefaultExpireTime);  //Set方式重新缓存 （MemcacheProvider封装不是很好）用第一方式设置还是Add  
            User newUser = client.Get<User>(passCode);
            //DistCache.Remove(passCode);
            client.Store(Enyim.Caching.Memcached.StoreMode.Replace, passCode, newUser);


            Thread.Sleep(8000);  //如果上面的代码没有刷新缓存至10秒 那下面的代码肯定会超时  
            //if (DistCache.Get<User>(passCode) != null)
            if (client.Get<User>(passCode) != null)
                Console.WriteLine(client.Get<User>(passCode).Name);
            else
                Console.WriteLine("因为没有操作，缓存未刷新，导致数据过期");

            Console.WriteLine("3秒后再次读取数据----------");
            Thread.Sleep(3000);
            if (client.Get<User>(passCode) != null)
                Console.WriteLine(client.Get<User>(passCode).Name);
            else
                Console.WriteLine("因为没有操作，缓存未刷新，导致数据过期");

            //以上服务端的过程 即服务端用户登录数据先过期,PassCode取不到用户登录数据，可以判定为登录过期  
            //还有一种情况客户端Cookie先过期 导致无法取到PassCode，因此服务端就无法获取用户数据，可以判定为登录过期。  
            Console.ReadLine();
        }

        [Serializable]
        public class User
        {
            public string Name { get; set; }
            public bool Sex { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
            public bool IsLoginSucess { get; set; }
        }
    }
}
