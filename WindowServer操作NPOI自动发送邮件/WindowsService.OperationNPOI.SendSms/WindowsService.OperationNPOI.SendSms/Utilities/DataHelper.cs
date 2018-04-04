using Esd.EnergyPec.Entities;
using Esd.EnergyPec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace WindowsService.OperationNPOI.SendSms.Utilities
{
    public class DataHelper
    {
        static DataHelper()
        {
            var ip = ConfigFileHelper.GetXmlNodeByXpath(@"C:\ReportGenerationXmlFile\ReportSystem.xml", "Sys/ServerIP").InnerText;
            if (string.IsNullOrEmpty(ip))
            {
                ip = "127.0.0.1";
            }
            ServiceURL = "net.tcp://" + ip + ":28888/EnergyPEC/{0}/";
        }

        public static void PreviewFillData(List<Mapping> mappings)
        {
            try
            {
                var factory = ServiceFactory.CreateService<IPriceTypeConfigService>(ServiceURL);
                var priceConfigs = factory.GetPriceTypeConfigs();
                factory.CloseService();
                var MeterFieldTypeService = ServiceFactory.CreateService<IMeterFieldTypeService>(ServiceURL);
                var DataService = ServiceFactory.CreateService<IDataService>(ServiceURL);
                var HourRecordService = ServiceFactory.CreateService<IHourRecordService>(ServiceURL);
                var DayRecordService = ServiceFactory.CreateService<IDayRecordService>(ServiceURL);
                var MonthRecordService = ServiceFactory.CreateService<IMonthRecordService>(ServiceURL);
                var YearRecordService = ServiceFactory.CreateService<IYearRecordService>(ServiceURL);

                foreach (var map in mappings)
                {
                    //非峰谷平处理
                    if (string.IsNullOrEmpty(map.PriceType))
                    {
                        switch (map.PeriodType)
                        {
                            //瞬时值
                            case PeriodType.Minute:

                                var type = MeterFieldTypeService.GetDataType(map.FieldTypeId);

                                switch (map.ValEnum)
                                {
                                    case ValEnum.Maximum:
                                        if (type == 0)
                                        {
                                            double valMax = DataService.GetAddUpdateMaximumVal(map.Period.Start, map.Period.End, map.FieldTypeId);
                                            map.Value = valMax.ToString();
                                        }
                                        else if (type == 1)
                                        {
                                            var valMax = DataService.GetMaximumVal(map.Period.Start, map.Period.End, map.FieldTypeId);
                                            map.Value = valMax.ToString();
                                        }
                                        break;
                                    case ValEnum.Minimum:
                                        if (type == 0)
                                        {
                                            double valMin = DataService.GetAddUpdateMinimumVal(map.Period.Start, map.Period.End, map.FieldTypeId);
                                            map.Value = valMin.ToString();
                                        }
                                        else if (type == 1)
                                        {
                                            double valInsMin = DataService.GetMinimum(map.Period.Start, map.Period.End, map.FieldTypeId);
                                            map.Value = valInsMin.ToString();
                                        }
                                        break;
                                    case ValEnum.Noraml:

                                        //瞬时值和累计值采集值
                                        if (type == 0)
                                        {
                                            AddUpData addupdata = DataService.GetLastAddUpDataByTimeAndId(map.FieldTypeId, map.Period.Start);
                                            map.Value = addupdata != null ? addupdata.MeterValue.ToString() : "0.00";
                                        }
                                        else if (type == 1)
                                        {
                                            InstantaneousDatas instantaneousData = DataService.GetLastInstantaneousDatasByTimeAndId(map.FieldTypeId, map.Period.Start);
                                            map.Value = instantaneousData != null ? instantaneousData.MeterValue.ToString() : "0.00";
                                        }
                                        break;
                                    default:
                                        break;
                                }

                                break;
                            case PeriodType.Hour:
                                var start = map.Period.Start.ToString("yyyy-MM-dd HH");
                                var endTime = map.Period.End.ToString("yyyy-MM-dd HH");
                                DateTime end = map.Period.End;
                                if (!start.Equals(endTime))
                                {
                                    end = map.Period.End.AddHours(-1);
                                }

                                var dataType = MeterFieldTypeService.GetDataType(map.FieldTypeId);
                                if (dataType == 0)
                                {
                                    var hourList = HourRecordService.GetAllHourRecord(DateTime.Parse(map.Period.Start.ToString("yyyy-MM-dd HH") + ":00:00"), DateTime.Parse(end.ToString("yyyy-MM-dd HH") + ":00:00"), map.FieldTypeId);
                                    if (hourList != null && hourList.Any())
                                    {
                                        map.Value = hourList.Sum(a => a.TotalData).ToString();
                                    }
                                    else
                                    {
                                        map.Value = "0.00";
                                    }
                                }
                                else if (dataType == 1)
                                {
                                    DateTime sTime = DateTime.Parse(map.Period.Start.ToString("yyyy-MM-dd HH") + ":00:00");
                                    DateTime eTime = DateTime.Parse(map.Period.End.ToString("yyyy-MM-dd HH") + ":59:59");
                                    var datas = DataService.GetInstantaneousDatasByType(sTime, eTime, map.FieldTypeId);
                                    if (datas != null)
                                    {
                                        var lastData = datas.Where(a => a.MeterValue > 0.0).OrderBy(a => a.CollectionTime).FirstOrDefault();
                                        map.Value = lastData == null ? "0.0" : lastData.MeterValue.ToString();
                                    }
                                    else
                                    {
                                        map.Value = "0.00";
                                    }
                                }
                                break;
                            case PeriodType.Day:
                                var dayList = DayRecordService.GetAllDayRecord(DateTime.Parse(map.Period.Start.ToString("yyyy-MM-dd") + " 00:00:00"), DateTime.Parse(map.Period.End.ToString("yyyy-MM-dd") + " 00:00:00"), map.FieldTypeId);
                                if (dayList != null && dayList.Any())
                                {
                                    map.Value = dayList.Sum(a => a.TotalData).ToString();
                                }
                                else
                                {
                                    map.Value = "0.00";
                                }
                                break;
                            case PeriodType.Month:
                                var monthdata = MonthRecordService.GetMonthRecord(DateTime.Parse(map.Period.Start.ToString("yyyy-MM") + "-01 00:00:00"), DateTime.Parse(map.Period.End.ToString("yyyy-MM") + "-01 00:00:00"), map.FieldTypeId);
                                if (monthdata != null && monthdata.Any())
                                {
                                    map.Value = monthdata.Sum(a => a.TotalData).ToString();
                                }
                                else
                                {
                                    map.Value = "0.00";
                                }
                                break;
                            case PeriodType.Year:
                                var yeardata = YearRecordService.GetYearRecord(DateTime.Parse(map.Period.Start.ToString("yyyy") + "-01-01 00:00:00"), DateTime.Parse(map.Period.End.ToString("yyyy") + "-01-01 00:00:00"), map.FieldTypeId);
                                if (yeardata != null && yeardata.Any())
                                {
                                    map.Value = yeardata.Sum(a => a.TotalData).ToString();
                                }
                                else
                                {
                                    map.Value = "0.00";
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    //峰谷平处理
                    else
                    {
                        var prices = priceConfigs.Where(a => a.PartTimeTypeName.Contains(map.PriceType)).ToList();
                        map.Value = GetPriceData(prices, map).ToString();
                    }

                }

                MeterFieldTypeService.CloseService();
                DataService.CloseService();
                HourRecordService.CloseService();
                DayRecordService.CloseService();
                MonthRecordService.CloseService();
                YearRecordService.CloseService();
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException ?? ex);
            }
        }

        private static double GetPriceData(List<PriceTypeConfig> pt, Mapping map)
        {
            double val = 0.0;
            IMinutesRecordService MinuteRecordService = null;
            try
            {
                MinuteRecordService = ServiceFactory.CreateService<IMinutesRecordService>(ServiceURL);
                var etime = MyEndTime(map.Period.End, map.PeriodType).AddDays(1);
                var stime = MyAddTime(etime, map.PeriodType);
                etime = etime.AddMinutes(-1);
                //用量
                var minutesData = MinuteRecordService.GetAllMinutesRecord(stime, etime, map.FieldTypeId);

                if (pt != null)
                {
                    foreach (var minutesRecord in minutesData)
                    {

                        var pct = pt.FirstOrDefault(p => Convert.ToDateTime(p.StartDateTime.ToString("HH:mm:ss")) <= Convert.ToDateTime(minutesRecord.HTime.ToString("HH:mm:ss")) &&
                            Convert.ToDateTime(p.EndDateTime.ToString("HH:mm:ss")) > Convert.ToDateTime(minutesRecord.HTime.ToString("HH:mm:ss")));
                        if (pct != null)
                        {
                            val += minutesRecord.TotalMoney;
                        }
                    }
                }
            }
            catch { }
            finally
            {
                MinuteRecordService.CloseService();
            }
            return val;
        }

        private static DateTime MyAddTime(DateTime time, PeriodType periodType)
        {
            switch (periodType)
            {
                case PeriodType.Year:
                    time = time.AddYears(-1);
                    break;
                case PeriodType.Month:
                    time = time.AddMonths(-1);
                    break;
                case PeriodType.Week:
                    time = time.AddDays(-6);
                    break;
                case PeriodType.Day:
                    time = time.AddDays(-1);
                    break;
                case PeriodType.Hour:
                    time = time.AddHours(-1);
                    break;
            }
            return time;
        }

        private static DateTime MyEndTime(DateTime end, PeriodType periodType)
        {
            switch (periodType)
            {
                case PeriodType.Year:
                    return DateTime.Parse(end.ToString("yyyy-12-31"));
                case PeriodType.Month:
                    var days = DateTime.DaysInMonth(end.Year, end.Month);
                    return DateTime.Parse(end.ToString("yyyy-MM-" + days));
                case PeriodType.Week:
                case PeriodType.Day:
                    return DateTime.Parse(end.ToString("yyyy-MM-dd"));
                case PeriodType.Hour:
                    return DateTime.Parse(end.ToString("yyyy-MM-dd HH:00:00"));
                case PeriodType.Minute:
                    return DateTime.Parse(end.ToString("yyyy-MM-dd HH:mm:00"));
            }
            return end;
        }

        private static string ServiceURL { get; set; }

        /// <summary>
        /// Get All Areas
        /// </summary>
        /// <returns></returns>
        public static ICollection<Area> GetAreas()
        {
            try
            {
                IAreaService areaservice = ServiceFactory.CreateService<IAreaService>(ServiceURL);
                var areas = areaservice.GetAreas();
                areaservice.CloseService();
                return areas;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
                return null;
            }
        }

        public static ICollection<BaseMeter> GetMeters()
        {
            IMeterService meterService = null;
            try
            {
                meterService = ServiceFactory.CreateService<IMeterService>(ServiceURL);
                var meters = meterService.GetBaseMeters();
                meterService.CloseService();
                return meters;
            }
            catch (Exception ex)
            {
                meterService.CloseService();
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
                return null;
            }
        }

        /// <summary>
        /// Get Parameters for meter
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static ICollection<BaseMeterFieldType> GetParameters(int meterId)
        {
            try
            {
                IMeterFieldTypeService meterFieldTypeService = ServiceFactory.CreateService<IMeterFieldTypeService>(ServiceURL);
                var meterFieldType = meterFieldTypeService.GetBaseMeterFieldTypeByMeterId(meterId);
                meterFieldTypeService.CloseService();
                return meterFieldType;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
                return null;
            }
        }

    }
}
