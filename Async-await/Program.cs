using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Async_await
{
    class Program
    {
        private static Stopwatch watch = new Stopwatch();
        static void Main(string[] args)
        {
            watch.Start();
            const string url1 = "http://www.cnblogs.com";
            const string url2 = "http://www.cnblogs.com/liqingwen";

            var result1 = CountCharacters(1, url1);
            var result2 = CountCharacters(2, url2);

            Console.WriteLine($"{url1} 的字符个数：{result1}");
            Console.WriteLine($"{url2} 的字符个数：{result2}");

            Task<int> result3 = CountCharactersAsync(1, url1);
            Task<int> result4 = CountCharactersAsync(2, url2);

            Console.WriteLine($"{url1} 的字符个数：{result3.Result}");
            Console.WriteLine($"{url2} 的字符个数：{result4.Result}");

            Console.Read();
        }

        private static int CountCharacters(int id, string address)
        {
            var webClient = new WebClient();
            Console.WriteLine($"Begin Invoke id={id}: {watch.ElapsedMilliseconds} ms");

            var result = webClient.DownloadString(address);
            Console.WriteLine($"Finish Invoke id={id}: {watch.ElapsedMilliseconds} ms");

            return result.Length;
        }

        private static async Task<int> CountCharactersAsync(int id, string address)
        {
            var webClient = new WebClient();
            Console.WriteLine($"Begin Async Invoke id = {id}：{watch.ElapsedMilliseconds} ms");

            var result = await webClient.DownloadStringTaskAsync(address);
            Console.WriteLine($"End Async Invoke id = {id}：{watch.ElapsedMilliseconds} ms");

            return result.Length;
        }
    }
}
