using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MSSQLToMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             *            转换步骤
             * 1，转移Energy    80于84端口结构一样
             * 2，转移MeterModels    80于84端口结构一样
             * 3，转移AquisitionParameters     80于84端口结构一样
             * 4，转移RegionalAreas     80于84端口结构一样
             * 5，转移BaseMeter     80于84端口结构一样
             * 6，转移MeterModelFieldTypes     80于84端口结构一样
             * 7，转移BaseMeterAcquisitionParameters     80于84端口结构一样
             * 8，转移VirtualMeterAcquisitionParameters
             * 9，转移Users表的某个一个用户信息，作为84端口的测试账号
             * 既然结构完全一样，那前9步全部使用SQL语句来实现
             * 
             * 9，转移MinutesRecord,HourRecord,DayRecord,MonthRecord,YearRecord 
             * */
            string dbName = ConfigurationManager.AppSettings["DbName"].ToString();
            IMongoClient _Client = new MongoClient();
            IMongoDatabase _Database = _Client.GetDatabase(dbName);
            string tableStr = ConfigurationManager.AppSettings["Tables"].ToString();
            string num = ConfigurationManager.AppSettings["NumOneTime"].ToString();
            string[] tables = tableStr.Split(',');
            int index = 0;
            double douNum = double.Parse(num);

            //查找当前系统所有的值
            string connectionStr = ConfigurationManager.ConnectionStrings["ConnectDB"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                try
                {
                    connection.Open();
                    for (int i = 0; i < tables.Length; i++)
                    {
                        index = i;
                        var start = DateTime.Now;
                        Console.WriteLine("Starting Transfer " + tables[i] + " Table Data");
                        CreateRecordBson(_Database, connection, tables[i], douNum);
                        var end = DateTime.Now;
                        int total = (end - start).Milliseconds;
                        Console.WriteLine("Finished " + tables[i] + " Data Transfer，Cost Time " + total + " MS");
                    }

                    Console.WriteLine("All data Transfer successfully！");
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(tables[index] + " Table Exception！");
                }
            }

        }

        private static void CreateRecordBson(IMongoDatabase _Database, SqlConnection connection, string tableName, double num)
        {
            //查询分数据插入Mongo
            string sqlQuery = "select count(0) from " + tableName;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlQuery;

            object result = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            if (result != null)
            {
                int count = 0;
                if (int.TryParse(result.ToString(), out count))
                {
                    double val = count / num;
                    int topcount = (int)num;
                    int group = int.Parse(Math.Ceiling(val).ToString());
                    Console.WriteLine(tableName + " Table, Total " + count + " Datas，\n\rWe Will Divide " + group + " Group And Upload into mongoDb");
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    for (int i = 1; i <= group; i++)
                    {
                        int top = topcount * i; //当前页*显示页条数
                        //sqlQuery = "select top " + topcount + " * from (select top " + top + " * from " + tableName + " order by id) a order by a.id desc";
                        sqlQuery = @"select top " + topcount + "* from (select top " + topcount + " * from (SELECT TOP " + top + " bmft.FieldTypeId,ft.Name as Parameter, bmft.MeterId, bm.Name as Meter, bmft.Unit,  mr.* FROM MinutesRecord mr LEFT JOIN BaseMeterFieldType bmft ON mr.BaseMeterFieldTypeId = bmft.Id LEFT JOIN FieldTypes ft ON ft.Id = bmft.FieldTypeId LEFT JOIN BaseMeter bm ON bm.Id = bmft.MeterId order by mr.id) a order by a.id desc)b order by b.id";
                        cmd.CommandText = sqlQuery;
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dt);

                        List<BsonDocument> datas = new List<BsonDocument>();
                        //创建MongoDBmodel对象为插入做准备
                        foreach (DataRow item in dt.Rows)
                        {
                            #region 注释
                            //MongoDBmodel model = new MongoDBmodel();
                            //model.BaseMeterFieldTypeId = int.Parse(item["BaseMeterFieldTypeId"].ToString());
                            //model.HTime = DateTime.Parse(item["HTime"].ToString());
                            //model.Id = int.Parse(item["Id"].ToString());
                            //model.LinkCompareTotalData = 0;
                            //model.LinkCompareTotalMoney = 0;
                            //model.SameCompareTotalData = 0;
                            //model.SameCompareTotalMoney = 0;
                            //model.TotalData = double.Parse(item["TotalData"].ToString());
                            //model.TotalMoney = double.Parse(item["TotalMoney"].ToString());

                            #endregion

                            var Document = new BsonDocument
                            {

                                    { "BaseMeterFieldTypeId", int.Parse(item["BaseMeterFieldTypeId"].ToString()) },
                                    { "TotalData", double.Parse(item["TotalData"].ToString()) },
                                    { "HTime", DateTime.Parse(item["HTime"].ToString()) },
                                    { "SameCompareTotalData", 0 },
                                    { "LinkCompareTotalData", 0 },
                                    { "TotalMoney", double.Parse(item["TotalMoney"].ToString()) },
                                    { "SameCompareTotalMoney", 0 },
                                    { "LinkCompareTotalMoney", 0 },
                                    { "FieldTypeId", int.Parse(item["FieldTypeId"].ToString()) },
                                    { "Parameter", item["Parameter"].ToString() },
                                    { "MeterId", int.Parse(item["MeterId"].ToString())},
                                    { "Meter", item["Meter"].ToString() },
                                    { "Unit", item["Unit"].ToString() },
                            };

                            //dayRecordModel.Add(model);
                            datas.Add(Document);
                        }
                        Console.WriteLine("Current Group is " + i + " Total Group " + group);
                        //每1000条记录读取一次，并插入mongodb中
                        Console.WriteLine("Start insert into " + tableName + " " + topcount + " Datas");
                        CreateRecordsInMongoDB(datas, tableName, _Database);
                        Console.WriteLine("Finished " + tableName + " " + topcount + " Datas");
                    }

                    cmd.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public static bool PrepareRecordsInMongoDB(int BaseMeterFieldTypeId, string HTime, string Tablename, IMongoDatabase _Database)
        {
            bool respone = true;
            var collection = _Database.GetCollection<BsonDocument>(Tablename);
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("BaseMeterFieldTypeId", BaseMeterFieldTypeId) & Builders<BsonDocument>.Filter.Eq("HTime", HTime);
                collection.DeleteOne(filter);
            }
            catch (Exception ex)
            {
                respone = false;
                Console.WriteLine(ex);
            }

            return respone;
        }

        public static void CreateRecordsInMongoDB(List<BsonDocument> DataModel, string TableName, IMongoDatabase _Database)
        {

            //foreach (var item in DataModel)
            //{
            //    //Remove Document from mongodb if already exist
            //    bool response;
            //    response = PrepareRecordsInMongoDB(item.BaseMeterFieldTypeId, item.HTime.ToString(), TableName, _Database);
            //    if (response)
            //    {
            //        var Document = new BsonDocument
            //    {

            //            { "BaseMeterFieldTypeId", item.BaseMeterFieldTypeId },
            //            { "TotalData", item.TotalData },
            //            { "HTime", item.HTime.ToString() },
            //            { "SameCompareTotalData", item.SameCompareTotalData },
            //            { "LinkCompareTotalData", item.LinkCompareTotalData },
            //            { "TotalMoney", item.TotalMoney },
            //            { "SameCompareTotalMoney", item.SameCompareTotalMoney },
            //            { "LinkCompareTotalMoney", item.LinkCompareTotalMoney },
            //            //{ "FieldTypeId", item.FieldTypeId },
            //            //{ "Parameter", item.Parameter },
            //            //{ "MeterId", item.MeterId },
            //            //{ "Meter", item.Meter },
            //            //{ "Unit", item.Unit },
            //    };

            try
            {
                var collection = _Database.GetCollection<BsonDocument>(TableName);
                //collection.InsertOne(Document);
                collection.InsertMany(DataModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //}
        }

    }

}
