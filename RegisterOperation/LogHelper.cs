using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportRegistor
{
    public class LogHelper
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(object info, object parameter)
        {
            string infoMsg = string.Format("【日志时间】：{0} \r\n【操作人员】：{1} \r\n【执行方法】：{2} \r\n【调用参数】：{3}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                "XX",
                info,
                parameter);

            AppendLog(infoMsg);
        }

        /// <summary>
        /// 错误记录
        /// </summary>
        /// <param name="info">附加信息</param>
        /// <param name="ex">错误</param>
        public static void ErrorLog(string info, Exception ex)
        {
            string errorMsg = BeautyErrorMsg(ex);

            AppendLog(errorMsg);
        }

        /// <summary>
        /// 美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            string errorMsg = string.Format("【异常时间】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { DateTime.Now.ToString("yyy-MM-dd HH:mm:ss"), ex.GetType().Name, ex.Message, ex.StackTrace });

            return errorMsg;
        }

        private static void AppendLog(string msg)
        {
            string fileName = @"LogFile\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            StreamWriter fileStream = null;
            object @lock = new object();

            lock (@lock)
            {
                try
                {
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile"))
                    {
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile");
                    }
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                    }
                    else
                    {
                        fileStream = new StreamWriter(filePath, true);
                        fileStream.WriteLine("-------------------------------------------------------------");
                        fileStream.WriteLine(msg);
                        fileStream.WriteLine("-------------------------------------------------------------\r\n");
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(string.Empty, ex);
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Flush();
                        fileStream.Close();
                    }
                }
            }
        }
    }
}
