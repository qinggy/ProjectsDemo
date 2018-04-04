using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Configuration;
using System.Reflection;
using DinoTechDataSyncService.Domain;

namespace DinoTechDataSyncService.Repository.Utilities
{
    public static class Common
    {

        /// <summary>
        /// This method will convert list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, typeof(string));
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable ToBaseRecordDataTable<T>(List<T> items) where T : DinoTechDataSyncService.Domain.BaseRecord
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            dataTable.Columns.Add("Id", typeof(string));
            dataTable.Columns.Add("TotalData", typeof(float));
            dataTable.Columns.Add("SameCompareTotalData", typeof(float));
            dataTable.Columns.Add("LinkCompareTotalData", typeof(float));
            dataTable.Columns.Add("TotalMoney", typeof(float));
            dataTable.Columns.Add("SameCompareTotalMoney", typeof(float));
            dataTable.Columns.Add("LinkCompareTotalMoney", typeof(float));
            dataTable.Columns.Add("HTime", typeof(DateTime));
            //dataTable.Columns.Add("RecordTypeId", typeof(int));
            dataTable.Columns.Add("BaseMeterFieldTypeId", typeof(int));

            foreach (var item in items)
            {
                DataRow row = dataTable.NewRow();
                row["Id"] = item.Id;
                row["TotalData"] = item.TotalData;
                row["SameCompareTotalData"] = item.SameCompareTotalData;
                row["LinkCompareTotalData"] = item.LinkCompareTotalData;
                row["TotalMoney"] = item.TotalMoney;
                row["SameCompareTotalMoney"] = item.SameCompareTotalMoney;
                row["LinkCompareTotalMoney"] = item.LinkCompareTotalMoney;
                row["HTime"] = item.HTime;
                //row["RecordTypeId"] = item.RecordTypeId;
                row["BaseMeterFieldTypeId"] = int.Parse(item.BaseMeterAcquisitionParameterId);

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
