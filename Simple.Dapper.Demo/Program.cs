using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Dapper.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string DbConnectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            //Insert 
            using (var connection = new SqlConnection(DbConnectionString))
            {
                connection.Open();
                Book book = new Book();
                book.Name = "C#本质论";
                string strExecute = "insert into Book(Name) values(@name)";
                connection.Execute(strExecute, new { name = book.Name });
                Console.ReadLine();
            }

            //Delete





            //Update




            //Read



            //StorageProce

        }

        public class Book
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<BookReview> Reviews { get; set; }
            public override string ToString()
            {
                return string.Format("图书：[{0}]------《{1}》", Id, Name);
            }
        }

        public class BookReview
        {
            public int Id { get; set; }
            public int BookId { get; set; }
            public string Content { get; set; }
            public Book AssoicationWithBook { get; set; }
            public override string ToString()
            {
                return string.Format("评论：{0})--[{1}]\t\"{3}\"", Id, BookId, Content);
            }
        }
    }
}
