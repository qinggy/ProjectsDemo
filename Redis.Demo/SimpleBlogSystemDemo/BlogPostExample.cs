using Newtonsoft.Json;
using NUnit.Framework;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Demo.SimpleBlogSystemDemo
{
    [TestFixture]
    public class BlogPostExample
    {
        readonly IRedisClient redisClient = new RedisClient("localhost", 6379, "onlyangusqinguse");

        [SetUp]
        public void OnBeforeEachTest()
        {
            //将所有记录标记删除，但没有真正删除
            redisClient.FlushAll();
            InsertTestData();
        }

        public void InsertTestData()
        {
            IRedisTypedClient<User> redisUsers = redisClient.As<User>();
            IRedisTypedClient<Blog> redisBlogs = redisClient.As<Blog>();
            IRedisTypedClient<BlogPost> redisBlogPosts = redisClient.As<BlogPost>();

            var angusqing = new User { Id = redisUsers.GetNextSequence(), Name = "angus qing" };
            var angelli = new User { Id = redisUsers.GetNextSequence(), Name = "angel li" };

            var qingblog = new Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = angusqing.Id,
                UserName = angusqing.Name,
                Tags = new List<string> { "Architecture", ".NET", "Databases" }
            };

            var liblog = new Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = angelli.Id,
                UserName = angelli.Name,
                Tags = new List<string> { "Shopping", "Food", "Entertainment" }
            };

            var blogPosts = new List<BlogPost> { 
                new BlogPost{
                     Id = redisBlogPosts.GetNextSequence(),
                     Title="Memcache",
                     Content ="Tell Us, How To Use Memcached",
                     BlogId = qingblog.Id,
                     Categories = new List<string>{"NoSQL", "DocumentDB"},
                     Tags = new List<string>{"Memcache", "NoSQL", "JSON", ".NET"},
                     Comments = new List<BlogPostComment>{
                        new BlogPostComment{
                                Content ="First Comment",
                                CreateDate = DateTime.Now
                        },
                        new BlogPostComment{
                            Content ="Second Comment",
                            CreateDate = DateTime.Now
                        }
                    }
                },
                new BlogPost{
                    Id = redisBlogPosts.GetNextSequence(),
                    Title ="Redis",
                    Content = "Redis Work Book",
                    BlogId = qingblog.Id,
                    Categories = new List<string>{"NoSql","MemoryDB"},
                    Tags = new List<string>{"Redis","NoSql","Scalability","Performance"},
                    Comments = new List<BlogPostComment>{
                        new BlogPostComment {
                            Content ="First Comment",
                            CreateDate = DateTime.Now
                        },
                        new BlogPostComment {
                            Content = "Second Comment",
                            CreateDate = DateTime.Now
                        }
                    }
                },
                new BlogPost{
                    Id = redisBlogPosts.GetNextSequence(),
                    Title ="Shopping",
                    Content = "How To Shop",
                    BlogId = liblog.Id,
                    Categories = new List<string>{"Shop","Food"},
                    Tags = new List<string>{"Shop","Fashion","Delicious"},
                    Comments = new List<BlogPostComment>{
                        new BlogPostComment {
                            Content ="First Comment",
                            CreateDate = DateTime.Now
                        },
                        new BlogPostComment {
                            Content = "Second Comment",
                            CreateDate = DateTime.Now
                        }
                    }
                },
                new BlogPost{
                    Id = redisBlogPosts.GetNextSequence(),
                    Title ="Fashion",
                    Content = "How To Fashion",
                    BlogId = liblog.Id,
                    Categories = new List<string>{"Fashion","Clothing"},
                    Tags = new List<string>{"Fashion","Clothing"},
                    Comments = new List<BlogPostComment>{
                        new BlogPostComment {
                            Content ="First Comment",
                            CreateDate = DateTime.Now
                        },
                        new BlogPostComment {
                            Content = "Second Comment",
                            CreateDate = DateTime.Now
                        }
                    }
                }
            };

            angusqing.BlogIds.Add(qingblog.Id);
            qingblog.BlogPostIds.AddRange(blogPosts.Where(a => a.BlogId == qingblog.Id).Select(a => a.Id));

            angelli.BlogIds.Add(liblog.Id);
            liblog.BlogPostIds.AddRange(blogPosts.Where(a => a.BlogId == liblog.Id).Select(a => a.Id));

            //保存用户信息
            redisUsers.Store(angusqing);
            redisUsers.Store(angelli);

            //保存博客信息
            redisBlogs.StoreAll(new[] { qingblog, liblog });

            //保存文章信息
            redisBlogPosts.StoreAll(blogPosts);
        }

        /// <summary>
        /// 显示所有博客
        /// </summary>
        [Test]
        public void Show_a_list_of_blogs()
        {
            IRedisTypedClient<Blog> redisbBlogs = redisClient.As<Blog>();
            var blogs = redisbBlogs.GetAll();
            Debug.WriteLine(JsonConvert.SerializeObject(blogs));
        }

        /// <summary>
        /// 显示最近的文章和评论
        /// </summary>
        [Test]
        public void Show_a_list_of_recent_posts_and_comments()
        {
            IRedisTypedClient<BlogPost> redisPosts = redisClient.As<BlogPost>();
            IRedisTypedClient<BlogPostComment> redisComments = redisClient.As<BlogPostComment>();

            //pretend this is the recently post
            var newIncomingBlogPosts = redisPosts.GetAll();

            IRedisList<BlogPost> recentPosts = redisPosts.Lists["urn:BlogPost:RecentPosts"];
            IRedisList<BlogPostComment> recentComments = redisComments.Lists["urn:BlogPostComment:RecentComments"];

            foreach (var newPost in newIncomingBlogPosts)
            {
                recentPosts.Prepend(newPost);
                //Prepend all the new blog post comments to the start of the 'RecentComments' list
                newPost.Comments.ForEach(recentComments.Prepend);
            }

            //fetch latest 3 items
            recentPosts.Trim(0, 2);
            recentComments.Trim(0, 2);

            Debug.WriteLine("最近3篇文章及其评论");
            Debug.WriteLine(JsonConvert.SerializeObject(recentPosts));
            Debug.WriteLine(JsonConvert.SerializeObject(recentComments));
        }

        /// <summary>
        /// 显示博客的标签
        /// </summary>
        [Test]
        public void Show_a_TagCloud()
        {
            //Get strongly-typed clients
            var redisBlogPosts = redisClient.As<BlogPost>();
            var newIncomingBlogPosts = redisBlogPosts.GetAll();

            foreach (var newBlogPost in newIncomingBlogPosts)
            {
                //For every tag in each new blog post, increment the number of times each Tag has occurred 
                newBlogPost.Tags.ForEach(x => redisClient.IncrementItemInSortedSet("urn:TagCloud", x, 1));
            }

            //Show top 5 most popular tags with their scores
            var tagCloud = redisClient.GetRangeWithScoresFromSortedSetDesc("urn:TagCloud", 0, 4);
            Debug.WriteLine(JsonConvert.SerializeObject(tagCloud));
        }

        /// <summary>
        ///  获取文章分类
        /// </summary>
        [Test]
        public void Show_all_Categories()
        {
            var redisBlogPosts = redisClient.As<BlogPost>();
            var blogPosts = redisBlogPosts.GetAll();

            foreach (var blogPost in blogPosts)
            {
                blogPost.Categories.ForEach(x => redisClient.AddItemToSet("urn:Categories", x));
            }

            var uniqueCategories = redisClient.GetAllItemsFromSet("urn:Categories");
            Debug.WriteLine("获取分类");
            Debug.WriteLine(JsonConvert.SerializeObject(uniqueCategories));
        }

        /// <summary>
        /// 显示文章机器评论
        /// </summary>
        [Test]
        public void Show_post_and_all_comments()
        {
            //There is nothing special required here as since comments are Key Value Objects 
            //they are stored and retrieved with the post
            var postId = 1;
            var redisBlogPosts = redisClient.As<BlogPost>();
            var selectedBlogPost = redisBlogPosts.GetById(postId.ToString());
            Debug.WriteLine("显示文章及其评论");
            Debug.WriteLine(JsonConvert.SerializeObject(selectedBlogPost));
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        [Test]
        public void Add_comment_to_existing_post()
        {
            var postId = 1;
            var redisBlogPosts = redisClient.As<BlogPost>();
            var blogPost = redisBlogPosts.GetById(postId.ToString());
            blogPost.Comments.Add(
                new BlogPostComment { Content = "Third Post!", CreateDate = DateTime.UtcNow });
            redisBlogPosts.Store(blogPost);

            var refreshBlogPost = redisBlogPosts.GetById(postId.ToString());
            Debug.WriteLine(string.Format("为编号为{0}的文章，添加了一个评论", postId));
            Debug.WriteLine(JsonConvert.SerializeObject(refreshBlogPost));
        }

        /// <summary>
        /// 显示分类以及分类对应的文章 【本文为获取所有分类为DocumentDB的文章】
        /// </summary>
        [Test]
        public void Show_all_Posts_for_the_DocumentDB_Category()
        {
            var redisBlogPosts = redisClient.As<BlogPost>();
            var newIncomingBlogPosts = redisBlogPosts.GetAll();

            foreach (var newBlogPost in newIncomingBlogPosts)
            {
                //For each post add it's Id into each of it's 'Cateogry > Posts' index
                newBlogPost.Categories.ForEach(x =>
                        redisClient.AddItemToSet("urn:Category:" + x, newBlogPost.Id.ToString()));
            }

            //Retrieve all the post ids for the category you want to view
            var documentDbPostIds = redisClient.GetAllItemsFromSet("urn:Category:DocumentDB");

            //Make a batch call to retrieve all the posts containing the matching ids 
            //(i.e. the DocumentDB Category posts)
            var documentDbPosts = redisBlogPosts.GetByIds(documentDbPostIds);

            Debug.WriteLine("显示分类以及分类对应的文章");
            Debug.WriteLine(JsonConvert.SerializeObject(documentDbPosts));
        }
    }
}
