using NUnit.Framework;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Demo.Demo
{
    [TestFixture]
    public class OperationHelper
    {
        /// <summary>
        /// Gets or Sets the Redis Manager. The Built-in used with ServiceStack autowires this property
        /// </summary>
        IRedisClientsManager RedisManager { get; set; }

        [SetUp]
        public void Init()
        {
            RedisManager = new PooledRedisClientManager(new string[] { "onlyangusqinguse@127.0.0.1:6379" }); //设置密码 password@ip:port
        }

        [TestCase(12)]
        public void DeleteQuestion(long questionId)
        {
            using (var redis = RedisManager.GetClient())
            {
                var redisQuestions = redis.As<Question>();

                var question = redisQuestions.GetById(questionId);
                if (question == null) return;
                
                //decrement score in tags list
                question.Tags.ForEach(tag => redis.IncrementItemInSortedSet("urn:tags", tag, -1));
            }
        }
    }
}
