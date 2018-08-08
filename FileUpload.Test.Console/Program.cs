using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FileUpload.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                // Make sure to change API address
                client.BaseAddress = new Uri("http://localhost:33328/");

                //Add first file content
                var fileContent = new ByteArrayContent(File.ReadAllBytes("Knockout应用开发指南.pdf"));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Sample.pdf"
                };

                // Add Second file content
                var fileContent2 = new ByteArrayContent(File.ReadAllBytes("health-detail.css"));
                fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Sample.css"
                };

                content.Add(fileContent);
                content.Add(fileContent2);

                // Make a call to Web API
                var result = client.PostAsync("/api/upload", content).Result;

                System.Console.WriteLine(result.StatusCode);
                System.Console.ReadLine();
            }
        }
    }
}
