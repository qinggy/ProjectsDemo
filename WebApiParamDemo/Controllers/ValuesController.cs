using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiParamDemo.Models;

namespace WebApiParamDemo.Controllers
{
    [RoutePrefix("")]
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        /// <summary>
        /// 使用Get传递基础数据类型（int, string etc.）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetAllChargingData(int id, string name, string bir)
        {
            return "ChargingData：编号 " + id + "姓名 " + name + "生日" + bir;
        }

        /// <summary>
        /// 使用Get传递实体类参数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetByModel([FromUri]TB_CHARGING oData)
        {
            return "ChargingData：姓名" + oData.NAME;
        }

        /// <summary>
        /// 使用Post新增基础数据类型（int，string etc.）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveData([FromBody]string name)
        {
            return name;
        }

        /// <summary>
        /// 使用Post提交多个基础数据类型（int，string etc.）
        /// </summary>
        /// <param name="age"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveDatas(int age, string name)
        {
            return "上传数据：" + age + "名称：" + name;
        }

        /// <summary>
        /// 使用Post提交自定义实体类参数
        /// </summary>
        /// <param name="oData"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveEntity(TB_CHARGING oData)
        {
            return oData.NAME;
        }


        /// <summary>
        /// 使用Post提交自定义实体类集合
        /// </summary>
        /// <param name="oDataList"></param>
        /// <returns></returns>
        [HttpPost]
        public string SaveEntities(List<TB_CHARGING> oDataList)
        {
            return oDataList[0].NAME;
        }

        /// <summary>
        /// 使用Post同时提交自定义实体类参数已经基础数据类型（int，string etc.）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public string SaveDataAndEntity(dynamic param)
        {
            return param.Name;
        }

        /// <summary>
        /// 使用Post传递数组参数，一般不建议使用Get传递数据参数，因为Get对传递的参数大小有限制，一般为1024kb
        /// </summary>
        /// <param name="oData"></param>
        /// <returns></returns>
        [HttpPost]
        public int SaveArrayData(string[] oData)
        {
            return oData[0].Length;
        }

        /// <summary>
        /// 使用Put更新基础数据（int，string etc.），和Post方法一样
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPut]
        public string UpdateOData(dynamic param)
        {
            return param.Name;
        }


    }
}
