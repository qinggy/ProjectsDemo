using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SqlBulkCopyInsertOrUpdate
{
    class Program
    {
        private static readonly string ConnectionStr = "Data Source=.;Initial Catalog=LocalDemoDb;user id=sa;password=esd.2015;";

        static void Main(string[] args)
        {
            int kk = Environment.TickCount;
            //循环10000000条数据插入 实验证明耗时236s
            //for (int i = 0; i < 1000; i++)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            //    dt.Columns.Add(new DataColumn("UserName", typeof(string)));
            //    for (int k = 0; k < 10000; k++) //10000
            //    {
            //        int userId = i * 10000 + k;
            //        DataRow dr = dt.NewRow();
            //        dr["UserID"] = userId;
            //        dr["UserName"] = "8888 - " + userId;
            //        dt.Rows.Add(dr);
            //    }
            //    //SqlBulkCopyColumnMapping[] mapp = new SqlBulkCopyColumnMapping[2];
            //    //mapp[0] = new SqlBulkCopyColumnMapping("UserID", "UserID");
            //    //mapp[1] = new SqlBulkCopyColumnMapping("UserName", "UserName");
            //    //提交任务表
            //    //BulkToDB(dt, "Test", mapp);
            //    BulkToDB(dt, "Test");
            //}

            //更新并且插入数据数据 更新5000条，插入5000条，混合在一起操作，再看时间
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("UserName", typeof(string)));
            for (int i = 0; i < 10000; i++)
            {
                if (i % 2 == 0) //更新操作
                {
                    DataRow dr = dt.NewRow();
                    dr["UserID"] = i;
                    dr["UserName"] = "19999 - " + i;
                    dt.Rows.Add(dr);
                }
                else //新增操作
                {
                    DataRow dr = dt.NewRow();
                    dr["UserID"] = 10019998 + i;
                    dr["UserName"] = "20000 - " + i;
                    dt.Rows.Add(dr);
                }
            }
            BulkToDB(dt, "Test");
            //实验证明耗时不到1s

            int exeg = Environment.TickCount - kk;
            Console.WriteLine("耗时" + exeg / 1000 + "秒");
            Console.ReadKey();
        }

        /// <summary>
        /// SqlBulkCopy往数据库中批量插入数据
        /// </summary>
        /// <param name="sourceDataTable">数据源表</param>
        /// <param name="targetTableName">服务器上目标表</param>
        /// <param name="mapping">创建新的列映射，并使用列序号引用源列和目标列的列名称。</param>
        public static void BulkToDB(DataTable sourceDataTable, string targetTableName)//, SqlBulkCopyColumnMapping[] mapping)
        {
            /*  调用方法 - 2013年05月10日编写
            //DataTable dt = Get_All_RoomState_ByHID();
            //SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[4];
            //mapping[0] = new SqlBulkCopyColumnMapping("Xing_H_ID", "Xing_H_ID");
            //mapping[1] = new SqlBulkCopyColumnMapping("H_Name", "H_Name");
            //mapping[2] = new SqlBulkCopyColumnMapping("H_sName", "H_sName");
            //mapping[3] = new SqlBulkCopyColumnMapping("H_eName", "H_eName");
            //BulkToDB(dt, "Bak_Tts_Hotel_Name", mapping);
            */
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                //SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);   //用其它源的数据有效批量加载sql server表中
                //指定大容量插入是否对表激发触发器。此属性的默认值为 False。
                SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionStr, SqlBulkCopyOptions.FireTriggers);
                bulkCopy.DestinationTableName = targetTableName;    //服务器上目标表的名称
                bulkCopy.BatchSize = sourceDataTable.Rows.Count;   //每一批次中的行数
                bulkCopy.BulkCopyTimeout = 300; //超时之前操作完成所允许的秒数,大批量数量需要的时长5分钟,2013-11-6备注 报错：“超时时间已到。在操作完成之前超时时间已过或服务器未响应”  解决办法：   
                bulkCopy.ColumnMappings.Add("UserID", "UserID");
                bulkCopy.ColumnMappings.Add("UserName", "UserName");

                if (sourceDataTable != null && sourceDataTable.Rows.Count != 0)
                {
                    //太费时，不需要这样操作
                    //for (int i = 0; i < mapping.Length; i++)
                    //    bulkCopy.ColumnMappings.Add(mapping[i]);
                    //将提供的数据源中的所有行复制到目标表中
                    bulkCopy.WriteToServer(sourceDataTable);
                }
            }
        }
    }
}
