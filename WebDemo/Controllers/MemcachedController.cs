using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memcached.ClientLibrary;

namespace WebDemo.Controllers
{
    public class MemcachedController : BaseController
    {
        //
        // GET: /Memcached/

        public ActionResult Index()
        {
            //初始化memcached 服务器端集群列表。
            String[] serverlist = { "127.0.0.1:11211"};


            // initialize the pool for memcache servers
            SockIOPool pool = SockIOPool.GetInstance("test");
            //设置怎么mem池连接点服务器端。
            pool.SetServers(serverlist);
            pool.Initialize();

            //创建了一个mem客户端的代理类。
            var mc = new MemcachedClient();
            mc.PoolName = "test";
            mc.EnableCompression = false;

            //mc.Add("itcast", "csdn 黑马");

            mc.Set("itast", "ssss", DateTime.Now.AddSeconds(15));

            pool.Shutdown();//关闭连接池

            //return View();

            return Content("ok");
        }

    }
}
