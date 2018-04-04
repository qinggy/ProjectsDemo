using Esd.EnergyPec.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;


namespace WindowsService.OperationNPOI.SendSms.Utilities
{
    public class ConfigFileHelper
    {
        private static readonly string CONFIG_PATH = "";
        private static readonly string CONFIG_REPORT_LIST = "";

        static ConfigFileHelper()
        {
            CONFIG_PATH = ConfigurationManager.AppSettings["ConfigurationPath"];
            if (string.IsNullOrEmpty(CONFIG_PATH))
            {
                CONFIG_PATH = @"C:\ReportGenerationServerXmlFile\";
            }
            CONFIG_REPORT_LIST = Path.Combine(CONFIG_PATH, "ReportGeneration.xml");
            if (!Directory.Exists(CONFIG_PATH))
            {
                Directory.CreateDirectory(CONFIG_PATH);
                Directory.CreateDirectory(CONFIG_PATH + "ReportCollection");
                Directory.CreateDirectory(CONFIG_PATH + "TemporaryFiles"); //报表保存的临时文件夹
                Directory.CreateDirectory(CONFIG_PATH + "TemplateSourceFiles"); //报表原始xlsx|xls文件

                File.Create(CONFIG_REPORT_LIST).Close();
                CreateXmlDocument(CONFIG_PATH + "ReportGeneration.xml", "ReportCollection");
            }

            if (!File.Exists(CONFIG_REPORT_LIST))
            {
                File.Create(CONFIG_REPORT_LIST).Close();
                CreateXmlDocument(CONFIG_PATH + "ReportGeneration.xml", "ReportCollection");
            }
        }

        public static bool VerfyConfigExists()
        {
            if (!File.Exists(CONFIG_REPORT_LIST))
            {
                FileNotFoundException exception = new FileNotFoundException("系统报表配置文件C:\\ReportGenerationServerXmlFile\\ReportGeneration.xml不存在！");
                SystemLogHelper.Logger.Error(exception.Message, exception.InnerException);
                return false;
            }

            return true;
        }

        public static void UpdateReportState(double interval, DateTime dtNow)
        {
            int currentMonth = dtNow.Month;
            int currentDay = dtNow.Day;
            int currentHour = dtNow.Hour;
            int currentMinute = dtNow.Minute;
            string dayOfWeek = dtNow.DayOfWeek.ToString();

            //每日都要发送/保存的报表每天凌晨修改当前报表状态
            if (currentHour == 0 && currentMinute >= 0 && currentMinute < interval)
            {
                XmlNodeList dayNodeList = GetXmlNodeListByXpath(CONFIG_REPORT_LIST, "ReportCollection/Report[@HasSend='True'][@SendType='Day']");
                foreach (XmlNode item in dayNodeList)
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("HasSend", "False");
                    UpdateXmlAttributeByXPath(CONFIG_REPORT_LIST, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                }
            }
            //每周都要发送/保存的报表每周一凌晨修改当前报表状态
            if (dayOfWeek == "Monday" && currentHour == 0 && currentMinute >= 0 && currentMinute < interval)
            {
                XmlNodeList dayNodeList = GetXmlNodeListByXpath(CONFIG_REPORT_LIST, "ReportCollection/Report[@HasSend='True'][@SendType='Week']");
                foreach (XmlNode item in dayNodeList)
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("HasSend", "False");
                    UpdateXmlAttributeByXPath(CONFIG_REPORT_LIST, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                }
            }
            //每月都要发送/保存的报表每月1号凌晨修改报表状态
            if (currentDay == 1 && currentHour == 0 && currentMinute >= 0 && currentMinute < interval)
            {
                XmlNodeList dayNodeList = GetXmlNodeListByXpath(CONFIG_REPORT_LIST, "ReportCollection/Report[@HasSend='True'][@SendType='Month']");
                foreach (XmlNode item in dayNodeList)
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("HasSend", "False");
                    UpdateXmlAttributeByXPath(CONFIG_REPORT_LIST, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                }
            }
            //每年都要发送/保存的报表每年1月1日1号凌晨修改报表状态
            if (currentMonth == 1 && currentDay == 1 && currentHour == 0 && currentMinute >= 0 && currentMinute < interval)
            {
                //find all hasSend report and set the attribute hasSend false
                XmlNodeList dayNodeList = GetXmlNodeListByXpath(CONFIG_REPORT_LIST, "ReportCollection/Report[@HasSend='True'][@SendType='Year']");
                foreach (XmlNode item in dayNodeList)
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("HasSend", "False");
                    UpdateXmlAttributeByXPath(CONFIG_REPORT_LIST, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                }
            }
        }

        public static void UpdateReportDataAndSendEmail(DateTime dtNow)
        {
            int currentMonth = dtNow.Month;
            int currentDay = dtNow.Day;
            int currentHour = dtNow.Hour;
            int currentMinute = dtNow.Minute;

            XmlNodeList nodeList = GetXmlNodeListByXpath(CONFIG_REPORT_LIST, "ReportCollection/Report[@HasSend='False'][@AutoSend='是'or@AutoSave='是'][@SendTime!=''][@SendType!='']");
            if (nodeList != null && nodeList.Count > 0)
            {
                string sendTime = string.Empty;
                string address = string.Empty;
                string sendType = string.Empty;
                string hasSend = string.Empty;
                string reportType = string.Empty;
                string filePath = string.Empty;
                string id = string.Empty;
                string autoSend = "";
                string autoSave = "";
                string name = "";
                DateTime dtSendTime;

                int month = 0, day = 0, hour = 0, minute = 0;

                foreach (XmlNode item in nodeList)
                {
                    id = item.Attributes["Id"].Value;
                    name = item.Attributes["name"].Value;
                    autoSend = item.Attributes["AutoSend"].Value;
                    autoSave = item.Attributes["AutoSave"].Value;
                    sendTime = item.Attributes["SendTime"].Value;
                    address = item.Attributes["Address"].Value;
                    sendType = item.Attributes["SendType"].Value;
                    hasSend = item.Attributes["HasSend"].Value;
                    filePath = item.Attributes["FilePath"].Value;
                    dtSendTime = DateTime.Parse(sendTime);

                    PeriodType periodType = (PeriodType)Enum.Parse(typeof(PeriodType), sendType);
                    switch (periodType)
                    {
                        case PeriodType.Day:
                            hour = dtSendTime.Hour;
                            minute = dtSendTime.Minute;
                            if ((currentHour > hour || (currentHour == hour && currentMinute >= minute)) && hasSend == "False")
                            {
                                GetEmailBodyAndUpdateState(id, filePath, address, dtNow, CONFIG_REPORT_LIST, item, autoSend, autoSave, name, PeriodType.Day);
                            }
                            break;
                        case PeriodType.Week:
                            string dayofweek = dtSendTime.DayOfWeek.ToString();
                            string nowDayOfWeek = dtNow.DayOfWeek.ToString();
                            if ((dayofweek == nowDayOfWeek || dtNow >= dtSendTime) && hasSend == "False")
                            {
                                GetEmailBodyAndUpdateState(id, filePath, address, dtNow, CONFIG_REPORT_LIST, item, autoSend, autoSave, name, PeriodType.Week);
                            }
                            break;
                        case PeriodType.Month:
                            day = dtSendTime.Day;
                            hour = dtSendTime.Hour;
                            minute = dtSendTime.Minute;
                            if ((currentDay > day || ((currentDay == day && currentHour > hour) || (currentDay == day && currentHour == hour && currentMinute >= minute))) && hasSend == "False")
                            {
                                GetEmailBodyAndUpdateState(id, filePath, address, dtNow, CONFIG_REPORT_LIST, item, autoSend, autoSave, name, PeriodType.Month);
                            }

                            break;
                        case PeriodType.Year:
                            month = dtSendTime.Month;
                            day = dtSendTime.Day;
                            hour = dtSendTime.Hour;
                            minute = dtSendTime.Minute;
                            if ((currentMonth > month || ((currentMonth == month && currentDay > day) || (currentMonth == month && currentDay == day && currentHour > hour) || (currentMonth == month && currentDay == day && currentHour == hour && currentMinute >= minute))) && hasSend == "False")
                            {
                                GetEmailBodyAndUpdateState(id, filePath, address, dtNow, CONFIG_REPORT_LIST, item, autoSend, autoSave, name, PeriodType.Year);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void GetEmailBodyAndUpdateState(string id, string filePath, string xmlAddress, DateTime dtNow, string xlsPath, XmlNode item, string autoSend, string autoSave, string name, PeriodType periodType)
        {
            try
            {
                SmsContent content = NpoiExcelHelper.RefreshExcelData(id, filePath, xmlAddress, dtNow, autoSend, autoSave, name, periodType);
                if (autoSend == "是")
                {
                    content.Password = Encryption.DecryptDES(content.Password); //解密密码
                    if (SendEmailHelper.SendEmail(content, true)) //是否采用SSL加密
                    {
                        Dictionary<string, string> attrs = new Dictionary<string, string>();
                        attrs.Add("HasSend", "True");
                        UpdateXmlAttributeByXPath(xlsPath, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                        FileInfo file = new FileInfo(content.Attachment);
                        file.Attributes = FileAttributes.Temporary;
                    }
                }
                if (autoSave == "是")
                {
                    Dictionary<string, string> attrs = new Dictionary<string, string>();
                    attrs.Add("HasSend", "True");
                    UpdateXmlAttributeByXPath(xlsPath, "ReportCollection/Report[@Id='" + item.Attributes["Id"].Value + "']", attrs);
                    FileInfo file = new FileInfo(content.Attachment);
                    file.Attributes = FileAttributes.Temporary;
                }

            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error("报表发送信息不正确，报表名称:" + name, ex);
            }
        }

        public static SmsContent GetSendContentFromXmlFile(string address, string xPath)
        {
            SmsContent content = null;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(address);
                XmlNode root = xmlDoc.SelectSingleNode("ReportInfo");
                if (root != null && root.HasChildNodes)
                {
                    content = new SmsContent();
                    content.From = root.ChildNodes[0].InnerText;
                    content.Password = root.ChildNodes[1].InnerText;
                    content.ServerPort = root.ChildNodes[2].InnerText;
                    content.ServerHost = root.ChildNodes[3].InnerText;
                    content.To = root.ChildNodes[4].InnerText;
                    content.CC = root.ChildNodes[5].InnerText;
                    content.Subject = root.ChildNodes[6].InnerText;
                    content.Body = root.ChildNodes[7].InnerText;
                    content.Attachment = root.ChildNodes[8].InnerText;
                    content.StartTimePosition = root.ChildNodes[9].InnerText;
                    content.EndTimePosition = root.ChildNodes[10].InnerText;
                    content.WriteTime = root.ChildNodes[11].InnerText;
                    content.GetDataTime = root.ChildNodes[12].InnerText;

                    XmlNodeList cellMappings = xmlDoc.SelectNodes("ReportInfo/CellMappings/Mapping");
                    if (cellMappings != null && cellMappings.Count > 0)
                    {
                        content.Mappings = new Mappings();
                        content.Mappings.MappingsList = new List<Mapping>();
                        foreach (XmlNode item in cellMappings)
                        {
                            Mapping map = new Mapping();
                            map.CellAddress = item.Attributes["CellAddress"].Value;
                            map.FieldTypeId = int.Parse(item.Attributes["FieldTypeId"].Value);
                            map.Period = new Period() { Start = DateTime.Parse(item.Attributes["StartTime"].Value), End = DateTime.Parse(item.Attributes["EndTime"].Value) };
                            map.PeriodType = (PeriodType)Enum.Parse(typeof(PeriodType), item.Attributes["SourceType"].Value);
                            map.PriceType = item.Attributes["PriceType"].Value;
                            map.ServiceURL = item.Attributes["ServiceURL"].Value;
                            map.ValEnum = (ValEnum)Enum.Parse(typeof(ValEnum), item.Attributes["ValEnum"].Value);
                            if (item.Attributes["ChangingYeadAndMonth"] != null)
                            {
                                map.ChangingYeadAndMonth = Convert.ToBoolean(item.Attributes["ChangingYeadAndMonth"].Value);
                            }
                            if (item.Attributes["ChangingYear"] != null)
                            {
                                map.ChangingYear = Convert.ToBoolean(item.Attributes["ChangingYear"].Value);
                            }
                            content.Mappings.MappingsList.Add(map);
                        }
                    }
                }
            }
            catch (Exception) { throw; }

            return content;
        }

        #region BaseMethod

        public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFileName); //Load XML Document
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
                return xmlNode;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
                return null;
                //throw ex; 
            }
        }

        private static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version = "1.0", string encoding = "utf-8", string standalone = "yes")
        {
            bool isSuccess = false;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
                XmlNode root = xmlDoc.CreateElement(rootNodeName);
                xmlDoc.AppendChild(xmlDeclaration);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(xmlFileName);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        private static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(xmlFileName); //Load XML Document
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);
                return xmlNodeList;
            }
            catch (Exception ex)
            {
                SystemLogHelper.Logger.Error(ex.Message, ex.InnerException);
                return null;
                //throw ex; 
            }
        }

        private static bool UpdateXmlAttributeByXPath(string xmlFileName, string xpath, Dictionary<string, string> xmlAttributes)
        {
            bool isSuccess = false;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFileName); //加载XML文档
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
                if (xmlNode != null)
                {
                    //遍历xpath节点中的所有属性
                    foreach (var attribute in xmlAttributes)
                    {
                        //if this attribute is exists
                        if (xmlNode.Attributes[attribute.Key] != null)
                        {
                            xmlNode.Attributes[attribute.Key].Value = attribute.Value;
                        }
                        else
                        {
                            XmlAttribute xmlAttr = xmlDoc.CreateAttribute(attribute.Key);
                            xmlAttr.Value = attribute.Value;
                            xmlNode.Attributes.Append(xmlAttr);
                        }
                    }
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex; //这里可以定义你自己的异常处理
            }
            return isSuccess;
        }

        #endregion
    }
}
