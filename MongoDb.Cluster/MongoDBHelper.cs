using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MongoDb.Cluster
{
    public class MongoDBHelper<T>
    {
        private static readonly string connectionString = ConfigurationManager.AppSettings["mongodbServerList"];

        static MongoClientSettings settings = null;
        MongoServer server = null;

        public string tableName = "person";

        public string dataBaseName = "test";

        static MongoDBHelper()
        {
            var ips = connectionString.Split(';');
            var serviceList = new List<MongoServerAddress>();
            foreach (var ip in ips)
            {
                var host = ip.Split(':')[0];
                var port = int.Parse(ip.Split(':')[1]);

                MongoServerAddress address = new MongoServerAddress(host, port);
                serviceList.Add(address);
            }

            settings = new MongoClientSettings();
            settings.ReplicaSetName = "datamip";
            settings.Servers = serviceList; //集群中的服务器列表
        }

        public MongoDBHelper(string dataBaseName, string tableName)
        {
            this.dataBaseName = dataBaseName;
            this.tableName = tableName;

            server = new MongoClient(settings).GetServer();
        }

        public bool Remove(Expression<Func<T, bool>> func)
        {
            var dataBase = server.GetDatabase(dataBaseName);
            var collection = dataBase.GetCollection<T>(tableName);
            var query = Query<T>.Where(func);
            var result = collection.Remove(query);
            return result.Response["ok"].AsInt32 > 0 ? true : false;
        }

        public bool RemoveAll()
        {
            try
            {
                var database = server.GetDatabase(dataBaseName);   //mongodb中的数据库
                var collection = database.GetCollection<T>(tableName);
                var result = collection.RemoveAll();
                return result.Response["ok"].AsInt32 > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Insert(T t)
        {
            var database = server.GetDatabase(dataBaseName);
            var collection = database.GetCollection<T>(tableName);
            var result = collection.Insert(t);

            return result.DocumentsAffected > 0;
        }

        public bool Save(T t)
        {
            var database = server.GetDatabase(dataBaseName);
            var collection = database.GetCollection<T>(tableName);
            var result = collection.Save(t);

            return result.DocumentsAffected > 0;
        }

        public bool Insert(IEnumerable<T> t)
        {
            try
            {
                var database = server.GetDatabase(dataBaseName);
                var collection = database.GetCollection<T>(tableName);
                collection.InsertBatch(t);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> Search(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            var list = new List<T>();

            try
            {
                //是否强制使用“主服务器”
                if (forcemaster)
                {
                    var database = server.GetDatabase(dataBaseName);
                    var collection = database.GetCollection<T>(tableName);
                    list = collection.Find(Query<T>.Where(func)).ToList();
                }
                else
                {
                    var database = server.GetDatabase(dataBaseName);
                    var collection = database.GetCollection<T>(tableName);
                    list = collection.Find(Query<T>.Where(func)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public T SearchOne(Expression<Func<T, bool>> func, bool forcemaster = false)
        {
            T t = default(T);

            try
            {
                //是否强制使用“主服务器”
                if (forcemaster)
                {
                    var database = server.GetDatabase(dataBaseName);   //mongodb中的数据库
                    var collection = database.GetCollection<T>(tableName);
                    t = collection.FindOne(Query<T>.Where(func));
                }
                else
                {
                    var database = server.GetDatabase(dataBaseName);   //mongodb中的数据库
                    var collection = database.GetCollection<T>(tableName);
                    t = collection.FindOne(Query<T>.Where(func));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return t;
        }

        public List<T> SearchAll()
        {
            var list = new List<T>();

            try
            {
                var database = server.GetDatabase(dataBaseName);    //mongodb中的数据库
                var collection = database.GetCollection<T>(tableName);
                list = collection.FindAll().ToList();

                return list;
            }
            catch (Exception ex)
            {
                return list;
            }
        }
    }
}
