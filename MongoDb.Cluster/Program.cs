using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDb.Cluster
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoDBHelper<MongodbCustomerModel> helper = new MongoDBHelper<MongodbCustomerModel>("mydb", "test");

            //helper.Save(new MongodbCustomerModel()
            //{
            //    SendLastTime = DateTime.Now,
            //    ShopID = 2
            //});
            var result = helper.SearchAll();

            foreach (var rs in result)
            {
                Console.WriteLine("结果如下" + rs.ShopID + ",其登录时间为" + rs.SendLastTime);
            }

            Console.ReadKey();
        }
    }

    public class MongodbCustomerModel
    {
        public ObjectId _id { get; set; }

        public int ShopID { get; set; }

        public DateTime SendLastTime { get; set; }
    }
}
