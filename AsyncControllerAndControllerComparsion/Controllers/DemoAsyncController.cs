using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AsyncControllerAndControllerComparsion.Controllers
{
    public class DemoAsyncController : AsyncController
    {
        //
        // GET: /DemoAsync/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 同步方法获取网页内容
        /// </summary>
        /// <returns></returns>
        public ContentResult getWebContent()
        {
            //Thread.Sleep(10000);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.cnblogs.com/kissdodog/archive/2013/04/06/3002779.html");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = sReader.ReadToEnd();

            return Content(html);
        }

        /// <summary>
        /// Task可以在不是继承自AsyncController的控制器中使用
        /// </summary>
        /// <returns></returns>
        public Task<ActionResult> TaskHtml()
        {
            return Task.Factory.StartNew(() =>
             {
                 HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.cnblogs.com/kissdodog/archive/2013/04/06/3002779.html");
                 HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                 Stream responseStream = response.GetResponseStream();
                 StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8);

                 return sReader.ReadToEnd();
             }).ContinueWith<ActionResult>(r =>
             {
                 return Content(r.Result);
             });
        }

        /// <summary>
        /// Asp.net Mvc 3.0使用方式，XXXAsync和XXXCompleted
        /// 由于默认是XXXAsync和XXXCompleted的方式，所以在调用该Action时，HtmlAsync方法写作Html
        /// etc. $.post("/DemoAsync/Html", function(data){});
        /// 对于以XxxAsync/XxxCompleted形式定义的异步Action方法来说，ASP.NET MVC并不会以异步的方式来调用XxxAsync方法，所以我们需要在该方法中自定义实现异步操作的执行
        /// 在下面定义的HtmlAsync方法中，我们是通过基于Task的并行编程方式来实现对文章内容的异步读取的。
        /// </summary>
        public void HtmlAsync()
        {
            //AsyncManager.OutstandingOperations.Increment();
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.cnblogs.com/kissdodog/archive/2013/04/06/3002779.html");
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream responseStream = response.GetResponseStream();
            //StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8);

            //AsyncManager.Parameters["content"] = sReader.ReadToEnd();
            //AsyncManager.OutstandingOperations.Decrement();

            //或者

            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() =>
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.cnblogs.com/kissdodog/archive/2013/04/06/3002779.html");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader sReader = new StreamReader(responseStream, Encoding.UTF8);
                AsyncManager.Parameters["content"] = sReader.ReadToEnd();

                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult HtmlCompleted(string content)
        {
            return Content(content);
        }
    }
}
