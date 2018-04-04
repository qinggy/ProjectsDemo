using NUnit.Framework;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Redis.Demo
{
    [TestFixture]
    public class Simple
    {
        private string host = "";
        private string password = "";
        private string passcode = "elementkey";

        [SetUp]
        public void Init()
        {
            host = "localhost";
            password = "onlyangusqinguse";
        }

        [Test]
        public void saveAndGetSimpleData()
        {
            using (RedisClient redisClient = new RedisClient(host, 6379, password))
            {
                if (redisClient.Get<string>(passcode) == null)
                {
                    //add a delay to see the difference
                    Thread.Sleep(5000);

                    //save a value to cache
                    redisClient.Set(passcode, "Redis test by angus");
                }

                Assert.AreEqual(redisClient.Get<string>(passcode), "Redis test by angus");
            }
        }

        [Test]
        public void saveAndGetEntityDemo() //k/v形式
        {
            using (RedisClient redisClient = new RedisClient(host, 6379, password))
            {
                IRedisTypedClient<Phone> phones = redisClient.As<Phone>();
                Phone phoneFive = phones.GetValue("5");
                if (phoneFive == null)
                {
                    //make a small delay
                    Thread.Sleep(5000);
                    //create a new phone entity
                    phoneFive = new Phone
                    {
                        Id = 5,
                        Manufacturer = "Apple",
                        Model = "iPhone 6",
                        Owner = new Person
                        {
                            Id = 1,
                            Age = 29,
                            Name = "angus",
                            Profession = "SoftWare Senior Engineer",
                            Surname = "qing"
                        }
                    };

                    //add the entity to the typed entity set
                    phones.SetEntry(phoneFive.Id.ToString(), phoneFive);
                }

                Assert.AreEqual(phones.GetValue("5").Model, "iPhone 6");
            }
        }

        [Test]
        public void saveAndGetEntityListDemo() //List/Set等形式
        {
            //主要要注意的是，Redis列表实现IList<T>，而Redis集合实现ICollection<T>。
            //当需要区分相同类型的不同分类对象时，使用列表。
            using (RedisClient redisClient = new RedisClient(host, 6379, password))
            {
                //Create a 'strongly-typed' API that makes all Redis Value operations to apply against Phones
                IRedisTypedClient<Phone> redis = redisClient.As<Phone>();
                
                IRedisList<Phone> mostSelling = redis.Lists["urn:phones:mostselling"];
                IRedisList<Phone> oldCollection = redis.Lists["urn:phones:oldcollection"];

                Person phonesOwner = new Person
                {
                    Id = 7,
                    Age = 90,
                    Name = "OldOne",
                    Profession = "sportsmen",
                    Surname = "OldManSurname"
                };

                // adding new items to the list
                mostSelling.Add(new Phone
                {
                    Id = 5,
                    Manufacturer = "Sony",
                    Model = "768564564566",
                    Owner = phonesOwner
                });

                oldCollection.Add(new Phone
                {
                    Id = 8,
                    Manufacturer = "Motorolla",
                    Model = "324557546754",
                    Owner = phonesOwner
                });

                var upgradedPhone = new Phone
                {
                    Id = 3,
                    Manufacturer = "LG",
                    Model = "634563456",
                    Owner = phonesOwner
                };

                mostSelling.Add(upgradedPhone);

                // remove item from the list
                oldCollection.Remove(upgradedPhone);

                // find objects in the cache
                IEnumerable<Phone> LGPhones = mostSelling.Where(ph => ph.Manufacturer == "LG");

                // find specific
                Phone singleElement = mostSelling.FirstOrDefault(ph => ph.Id == 8);

                //reset sequence and delete all lists
                redis.SetSequence(0);
                redisClient.Remove("urn:phones:mostselling");
                redisClient.Remove("urn:phones:oldcollection");
            }
        }
    }
}
