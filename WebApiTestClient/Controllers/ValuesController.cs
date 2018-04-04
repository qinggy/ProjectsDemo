using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiTestClient.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 根据Id获取指定数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Put更改数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
        }
    }
}