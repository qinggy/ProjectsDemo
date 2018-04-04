using Esd.EnergyPec.Entities;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsService.OperationNPOI.SendSms.Utilities
{
    public class NpoiExcelHelper
    {
        private static readonly string REPORT_SAVE_DIRECTORY;
        //报表保存的临时文件夹（防止文件被占用的冲突）
        private static readonly string REPORT_TEMPORATY_PATH = @"C:\ReportGenerationServerXmlFile\TemporaryFiles\";

        static NpoiExcelHelper()
        {
            var saveDirectory = ConfigurationManager.AppSettings["SaveDirectory"];
            if (!string.IsNullOrEmpty(saveDirectory))
            {
                REPORT_SAVE_DIRECTORY = @"C:\ReportFile";
            }
        }

        public static SmsContent RefreshExcelData(string id, string filePath, string address, DateTime currentTime, string autoSend, string autoSave, string name, PeriodType periodType)
        {
            //获取当前待生成/发送报表的配置信息
            SmsContent content = ConfigFileHelper.GetSendContentFromXmlFile(address, "");
            try
            {
                //发送系统报表
                if (string.IsNullOrEmpty(filePath))
                {
                    //CreateReport cr = new CreateReport(int.Parse(id));
                    //content.Attachment = cr.StartCreateReport();
                }
                else //发送智能报表
                {
                    UpdateReportData(currentTime, content);

                    //更新报表文件数据
                    if (!string.IsNullOrEmpty(content.Attachment) && File.Exists(content.Attachment))
                    {
                        string fileType = Path.GetExtension(content.Attachment);
                        string tempfilePath = REPORT_TEMPORATY_PATH + Guid.NewGuid().ToString().Replace("-", "") + fileType;
                        //File.Copy(content.Attachment, tempfilePath, true);

                        if (autoSend.Equals("是") || autoSave.Equals("是"))
                        {
                            ExcelDataFill(content, tempfilePath, currentTime, periodType, fileType);
                        }

                        if (autoSend.Equals("是"))
                        {
                            string smsAttachment = REPORT_TEMPORATY_PATH + name + fileType;
                            File.Copy(tempfilePath, smsAttachment, true);
                            content.Attachment = smsAttachment;
                        }
                        if (autoSave.Equals("是"))
                        {
                            if (!Directory.Exists(REPORT_SAVE_DIRECTORY))
                            {
                                Directory.CreateDirectory(REPORT_SAVE_DIRECTORY);
                            }

                            var fileSavePath = Path.Combine(REPORT_SAVE_DIRECTORY, name + "[" + currentTime.ToString("yyyy-MM-dd HHmm") + "]" + fileType);
                            File.Copy(tempfilePath, fileSavePath, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
            }

            return content;
        }

        private static void UpdateReportData(DateTime currentTime, SmsContent content)
        {
            TimeSpan timespan = new TimeSpan(0);
            TimeSpan maxTimeSpan = new TimeSpan(0);

            var maxObj = content.Mappings.MappingsList.OrderByDescending(a => a.Period.End).Take(1).FirstOrDefault();
            DateTime maxEnd = maxObj.Period.End;
            DateTime maxStart = maxObj.Period.Start;

            foreach (var mapping in content.Mappings.MappingsList)
            {
                if (mapping.ChangingYear || mapping.ChangingYeadAndMonth)
                {
                    try
                    {
                        var stime = GetConvTime(mapping.Period.Start, mapping.PeriodType);
                        var etime = GetConvTime(mapping.Period.End, mapping.PeriodType);
                        if (mapping.ChangingYeadAndMonth)
                        {
                            //变年和变月
                            DateTime[] yearMonth = PorductChangeYearAndMonth(mapping.Period.End, mapping.Period.Start, maxEnd, currentTime);

                            mapping.Period.Start = new DateTime(yearMonth[0].Year, yearMonth[0].Month, stime.Day, stime.Hour, stime.Minute, stime.Second);
                            mapping.Period.End = new DateTime(yearMonth[1].Year, yearMonth[1].Month, etime.Day, etime.Hour, etime.Minute, etime.Second);
                        }
                        else if (mapping.ChangingYear)
                        {
                            //只变年 需要有年数差
                            int[] year = ProductChangeYear(mapping.Period.End.Year, mapping.Period.Start.Year, maxEnd.Year, currentTime.Year);

                            mapping.Period.Start = new DateTime(year[0], stime.Month, stime.Day, stime.Hour, stime.Minute, stime.Second);
                            mapping.Period.End = new DateTime(year[1], etime.Month, etime.Day, etime.Hour, etime.Minute, etime.Second);
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemLogHelper.Logger.Error("时间溢出，不影响", ex);
                    }
                }
                else
                {
                    if (DateTimeEquals(mapping.Period.End, maxEnd))
                    {
                        timespan = mapping.Period.End - mapping.Period.Start;
                        mapping.Period.End = DateTime.Parse(currentTime.ToString("yyyy-MM-dd") + " " + mapping.Period.End.Hour + ":" + mapping.Period.End.Minute + ":" + mapping.Period.End.Second);

                        if (mapping.PeriodType == PeriodType.Month)
                        {
                            mapping.Period.Start = DateTime.Parse(mapping.Period.End.AddDays(0 - timespan.Days + 1).ToString("yyyy-MM-dd") + " " + mapping.Period.Start.Hour + ":" + mapping.Period.Start.Minute + ":" + mapping.Period.Start.Second);
                        }
                        //if (mapping.PeriodType == PeriodType.Hour)
                        //{
                        //    //这里如何在小时类型数据中，又出现了跨天的情况 。。。。。(解决方案：小时数据不能跨天)
                        //    mapping.Period.Start = mapping.Period.End.AddHours(0 - timespan.Hours + 1);
                        //}
                        else
                        {
                            mapping.Period.Start = DateTime.Parse(mapping.Period.End.AddDays(0 - timespan.Days).ToString("yyyy-MM-dd") + " " + mapping.Period.Start.Hour + ":" + mapping.Period.Start.Minute + ":" + mapping.Period.Start.Second);
                        }
                    }
                    else
                    {
                        maxTimeSpan = maxEnd - mapping.Period.End;
                        timespan = mapping.Period.End - mapping.Period.Start;
                        mapping.Period.End = DateTime.Parse(currentTime.AddDays(0 - maxTimeSpan.Days).ToString("yyyy-MM-dd") + " " + mapping.Period.End.Hour + ":" + mapping.Period.End.Minute + ":" + mapping.Period.End.Second);
                        if (mapping.PeriodType == PeriodType.Month)
                        {
                            mapping.Period.Start = DateTime.Parse(mapping.Period.End.AddDays(0 - timespan.Days + 1).ToString("yyyy-MM-dd") + " " + mapping.Period.Start.Hour + ":" + mapping.Period.Start.Minute + ":" + mapping.Period.Start.Second);
                        }
                        //if (mapping.PeriodType == PeriodType.Hour)
                        //{
                        //    mapping.Period.Start = mapping.Period.End.AddHours(0 - timespan.Hours + 1);
                        //}
                        else
                        {
                            mapping.Period.Start = DateTime.Parse(mapping.Period.End.AddDays(0 - timespan.Days).ToString("yyyy-MM-dd") + " " + mapping.Period.Start.Hour + ":" + mapping.Period.Start.Minute + ":" + mapping.Period.Start.Second);
                        }
                    }
                }
            }

            FillExcelUsingNpoi(content.Mappings);
        }

        private static void FillExcelUsingNpoi(Mappings mappings)
        {
            DataHelper.PreviewFillData(mappings.MappingsList);
        }

        private static DateTime GetConvTime(DateTime time, PeriodType pt)
        {
            DateTime result;
            switch (pt)
            {
                case PeriodType.Year:
                    result = Convert.ToDateTime(time.ToString("yyyy-MM-dd"));
                    break;
                case PeriodType.Month:
                    result = Convert.ToDateTime(time.ToString("yyyy-MM-dd"));
                    break;
                case PeriodType.Day:
                    result = Convert.ToDateTime(time.ToString("yyyy-MM-dd"));
                    break;
                case PeriodType.Hour:
                    result = Convert.ToDateTime(time.ToString("yyyy-MM-dd HH:00:00"));
                    break;
                case PeriodType.Minute:
                    result = Convert.ToDateTime(time.ToString("yyyy-MM-dd HH:mm:00"));
                    break;
                default:
                    result = time;
                    break;
            }
            return result;
        }

        private static bool DateTimeEquals(DateTime time, DateTime compareTime)
        {
            if (time.Year == compareTime.Year && time.Month == compareTime.Month && time.Day == compareTime.Day)
            {
                return true;
            }

            return false;
        }

        private static void ExcelDataFill(SmsContent content, string filePath, DateTime currentTime, PeriodType periodType, string fileType)
        {
            //ISheet sheet;
            IWorkbook workBook = null;
            string sheetNameKey = "";
            string cellKey = "";
            Regex letterRegex = new Regex("[a-zA-Z]+", RegexOptions.IgnoreCase);
            Regex numRegex = new Regex(@"\d+", RegexOptions.IgnoreCase);
            //Dictionary<string, ISheet> sheets = new Dictionary<string, ISheet>();

            try
            {
                using (FileStream fs = new FileStream(content.Attachment, FileMode.Open, FileAccess.Read))
                {
                    switch (fileType)
                    {
                        case ".xlsx":
                            workBook = new XSSFWorkbook(fs);
                            //foreach (ISheet item in workBook)
                            //{
                            //    sheets.Add(item.SheetName, item);
                            //}

                            break;
                        case ".xls":
                            workBook = new HSSFWorkbook(fs);
                            //foreach (ISheet item in workBook)
                            //{
                            //    sheets.Add(item.SheetName, item);
                            //}

                            break;
                        default:
                            break;
                    }
                    fs.Close();
                }


                if (!string.IsNullOrEmpty(content.WriteTime.Trim())) //填充报表制表时间
                {
                    sheetNameKey = content.WriteTime.Split('!')[0];
                    cellKey = content.WriteTime.Split('!')[1];
                    //sheet = workBook.CreateSheet(sheetNameKey);
                    //sheets.Add(sheetNameKey, sheet);
                    DateTime tempTime = currentTime;
                    //if (DateTime.TryParse(content.WriteTime, out tempTime))
                    //{
                    FillCell(tempTime.ToString("yyyy-MM-dd"), letterRegex, numRegex, sheetNameKey, cellKey, workBook.GetSheet(sheetNameKey), "String");
                    //}

                }
                if (!string.IsNullOrEmpty(content.GetDataTime.Trim()))
                {
                    sheetNameKey = content.GetDataTime.Split('!')[0];
                    cellKey = content.GetDataTime.Split('!')[1];
                    //if (sheets.ContainsKey(sheetNameKey))
                    //{
                    //    sheets.TryGetValue(sheetNameKey, out sheet);
                    //}
                    //else
                    //{
                    //    sheet = workBook.CreateSheet(sheetNameKey);
                    //    sheets.Add(sheetNameKey, sheet);
                    //}
                    switch (periodType)
                    {
                        case PeriodType.Day:
                            content.GetDataTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                            break;
                        case PeriodType.Month:
                            content.GetDataTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                            break;
                        case PeriodType.Week:
                            content.GetDataTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                            break;
                        case PeriodType.Year:
                            content.GetDataTime = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                            break;
                        default:
                            break;
                    }

                    FillCell(content.GetDataTime, letterRegex, numRegex, sheetNameKey, cellKey, workBook.GetSheet(sheetNameKey), "String");
                }
                foreach (var item in content.Mappings.MappingsList)
                {
                    if (!string.IsNullOrEmpty(item.CellAddress))
                    {
                        sheetNameKey = item.CellAddress.Split('!')[0];
                        cellKey = item.CellAddress.Split('!')[1];
                        //if (sheets.ContainsKey(sheetNameKey))
                        //{
                        //    sheets.TryGetValue(sheetNameKey, out sheet);
                        //}
                        //else
                        //{
                        //    sheet = workBook.CreateSheet(sheetNameKey);
                        //    sheets.Add(sheetNameKey, sheet);
                        //}

                        FillCell(item.Value, letterRegex, numRegex, sheetNameKey, cellKey, workBook.GetSheet(sheetNameKey), "Numeric");
                    }
                }

                using (FileStream saveStream = new FileStream(filePath, FileMode.Create))
                {
                    workBook.Write(saveStream);
                    saveStream.Close();
                }

            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException ?? ex);
            }
        }

        private static void FillCell(string cellVal, Regex letterRegex, Regex numRegex, string sheetNameKey, string cellKey, ISheet sheet, string cellType)
        {
            var strMath = letterRegex.Match(cellKey);
            string cellNo = strMath.Value;
            var match = numRegex.Match(cellKey);
            int rowNo = 0;
            if (int.TryParse(match.Value, out rowNo))
            {
                IRow row = sheet.GetRow(rowNo - 1);
                if (cellType.Equals("String"))
                {
                    row.CreateCell(GetCellNo(cellNo), CellType.String).SetCellValue(cellVal);
                }
                else if (cellType.Equals("Numeric"))
                {
                    double cellValue = 0.0;
                    if (string.IsNullOrEmpty(cellVal) || cellVal.Equals("null"))
                    {
                        cellVal = "0.0";
                    }
                    if (!double.TryParse(cellVal, out cellValue))
                    {
                        cellValue = 0.0d;
                    }
                    row.CreateCell(GetCellNo(cellNo), CellType.Numeric).SetCellValue(cellValue);
                }
            }
        }

        private static int GetCellNo(string strCellNo)
        {
            int cellNo = 0;
            Dictionary<string, int> cellMap = new Dictionary<string, int>();
            string letterStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < 26; i++)
            {
                cellMap.Add(letterStr.Substring(i, 1), i);
            }
            //目前最多只处理到ZZ的情况，即处理26^26次幂个列数据
            int length = strCellNo.Length;
            if (length == 1)
            {
                string firstLetter = strCellNo.Substring(0);
                cellMap.TryGetValue(firstLetter, out cellNo);

                return cellNo;
            }
            else if (length == 2)
            {
                string firstLetter = strCellNo.Substring(0);
                string lastLetter = strCellNo.Substring(1);
                int firstInt = 0;
                int secondInt = 0;
                cellMap.TryGetValue(firstLetter, out firstInt);
                cellMap.TryGetValue(lastLetter, out secondInt);

                return (firstInt + 1) * 26 + secondInt;
            }
            else if (length > 2)
            {
                return 0;
            }

            return 0;
        }

        private static bool YearEquals(int time, int compareTime)
        {
            if (time.Equals(compareTime))
            {
                return true;
            }

            return false;
        }

        private bool YearAndMonthEquals(DateTime time, DateTime compareTime)
        {
            if (time.Year.Equals(compareTime.Year) && time.Month.Equals(compareTime.Month))
            {
                return true;
            }

            return false;
        }

        private static int[] ProductChangeYear(int endYear, int startYear, int maxYear, int currentYear)
        {
            int[] result = new int[2];
            if (YearEquals(endYear, maxYear))
            {
                int timespan = endYear - startYear;
                //将当前预览时间置为查询结束时间
                endYear = currentYear;
                startYear = endYear + (0 - timespan);
            }
            else
            {
                int maxTimeSpan = maxYear - endYear;
                int timespan = endYear - startYear;
                endYear = currentYear + (0 - maxTimeSpan);
                startYear = endYear + (0 - timespan);
            }
            result[0] = startYear;
            result[1] = endYear;

            return result;
        }

        private static DateTime[] PorductChangeYearAndMonth(DateTime endDate, DateTime startDate, DateTime maxDate, DateTime currentDate)
        {
            DateTime[] result = new DateTime[2];
            int endYear = endDate.Year;
            int startYear = startDate.Year;
            int[] yearResult = ProductChangeYear(endYear, startYear, maxDate.Year, currentDate.Year);//取得变化后的年数据

            int endMonth = endDate.Month;
            int startMonth = startDate.Month;
            int[] monthResult = ProductChangeYear(endMonth, startMonth, 12, currentDate.Month);

            result[0] = new DateTime(yearResult[0], monthResult[0], 1);
            result[1] = new DateTime(yearResult[1], monthResult[1], 1);

            return result;
        }

    }
}
