using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input username and password, such as admin|admin");
            var input = Console.ReadLine();
            var username = input.Split('|')[0];
            var psd = input.Split('|')[1];

            ServiceTokenValidation.DinoTechDataSyncServiceClient client = new ServiceTokenValidation.DinoTechDataSyncServiceClient();
            ServiceTokenValidation.ResponseUserTokenInfo tokenInfo = client.ValidateUser(username, psd, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            ServiceTokenValidation.DinoTechDataSyncServiceClient clientService = CreateService(tokenInfo.Token);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 100; i++)
            {
                Stopwatch watcher = new Stopwatch();
                watcher.Start();
                List<ServiceTokenValidation.YearRecord> yearRecordList = new List<ServiceTokenValidation.YearRecord>();
                if (i == 0)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ServiceTokenValidation.YearRecord month = new ServiceTokenValidation.YearRecord();
                        month.Id = "1232";
                        month.TotalData = 2650;
                        month.SameCompareTotalData = 0;
                        month.SameCompareTotalMoney = 0;
                        month.TotalMoney = 0;
                        month.LinkCompareTotalData = 0;
                        month.LinkCompareTotalMoney = 0;
                        month.BaseMeterAcquisitionParameterId = "26273";//"23779";
                        month.HTime = DateTime.Now;

                        yearRecordList.Add(month);
                    }
                }
                else
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ServiceTokenValidation.YearRecord month = new ServiceTokenValidation.YearRecord();
                        month.Id = "1232";
                        month.TotalData = 2650;
                        month.SameCompareTotalData = 0;
                        month.SameCompareTotalMoney = 0;
                        month.TotalMoney = 0;
                        month.LinkCompareTotalData = 0;
                        month.LinkCompareTotalMoney = 0;
                        month.BaseMeterAcquisitionParameterId = "23779";//"23779";
                        month.HTime = DateTime.Now;

                        yearRecordList.Add(month);
                    }
                }
                clientService.SubmitYearRecords(yearRecordList.ToArray(), "2016-02-25 12:00:10");
                watcher.Stop();
                Console.WriteLine(string.Format("完成第{0}次插入循环，耗时" + watcher.Elapsed, i + 1));
            }

            timer.Stop();
            Console.WriteLine(string.Format("更新或插入{0}条数据完成，总共耗时" + timer.Elapsed, 100000));
            Console.ReadKey();
        }

        static ServiceTokenValidation.DinoTechDataSyncServiceClient CreateService(string userToken)
        {
            MessageHeader header;
            OperationContextScope scope;

            ServiceTokenValidation.DinoTechDataSyncServiceClient objService = new ServiceTokenValidation.DinoTechDataSyncServiceClient();

            //Defining the scope
            scope = new OperationContextScope(objService.InnerChannel);

            //Creating the Message header
            header = MessageHeader.CreateHeader("TokenHeader", "TokenNameSpace", userToken);

            //Adding the created Message header with client request
            OperationContext.Current.OutgoingMessageHeaders.Add(header);

            return objService;
        }
    }
}
