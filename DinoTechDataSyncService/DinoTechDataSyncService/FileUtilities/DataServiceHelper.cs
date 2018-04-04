using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DinoTechDataSyncService.WCF.FileUtilities
{
    public class DataServiceHelper
    {
        public static readonly string InstantaneousDatasPath;
        public static readonly string AddUpdataPath;

        private static readonly object LockInstantaneousDatas = new object();
        private static readonly object LockAddUpData = new object();

        public static object LockCreateFile = new object();

        static DataServiceHelper()
        {
            //读取采集参数存储位置
            var fileDataPath = HelperSystem.SystemDataFilePath;
            //判断存放瞬时值的路径
            InstantaneousDatasPath = fileDataPath + @"InstantaneousDatas\";
            if (!Directory.Exists(InstantaneousDatasPath))
            {
                Directory.CreateDirectory(InstantaneousDatasPath);
            }
            AddUpdataPath = fileDataPath + @"AddUpDatas\";
            if (!Directory.Exists(AddUpdataPath))
            {
                Directory.CreateDirectory(AddUpdataPath);
            }
        }

        public string GetLastUpdateTime(string mid, int dType)
        {
            DirectoryInfo folder = null;
            if (dType == 0)
                folder = new DirectoryInfo(AddUpdataPath);
            else
                folder = new DirectoryInfo(InstantaneousDatasPath);

            foreach (DirectoryInfo directoryInfo in folder.GetDirectories().OrderByDescending(a => a.Name))
            {
                string filePath = directoryInfo.FullName + @"\" + mid + ".txt";
                if (!File.Exists(filePath)) continue;
                var addUpdatas = GetAutoDataByPath<InstantaneousDatas>(filePath).OrderByDescending(a => a.CollectionTime);
                var faud = addUpdatas.FirstOrDefault();
                if (faud != null)
                {
                    return faud.CollectionTime.ToString();
                }
            }
            return null;
        }

        private List<T> GetAutoDataByPath<T>(string path) where T : AutoData
        {
            List<T> autoDataList = null;
            FileStream stream = null;
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(List<T>));
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var sReader = new StreamReader(stream);
                var str = sReader.ReadToEnd().Replace("\0", ""); ;
                if (!string.IsNullOrEmpty(str))
                {
                    str = "[" + str.Substring(0, str.Length - 1) + "]";
                    var mStream = new MemoryStream(Encoding.UTF8.GetBytes(str));
                    autoDataList = serializer.ReadObject(mStream) as List<T>;
                    mStream.Close();
                    mStream.Dispose();
                }
                sReader.Close();
                sReader.Dispose();
            }
            catch (Exception ex)
            {
                //SystemLogHelper.Logger.Error("读取文件失败,路径:" + path, ex);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }

            var result = autoDataList ?? new List<T>();
            //return result.DistinctBy(a => a.CollectionTime).ToList();
            var distinctData = (from data in result group data by data.CollectionTime into g select g.First()).ToList();

            return distinctData;
        }

        public bool UploadData(ICollection<InstantaneousDatas> instantaneousDatas)
        {
            bool result = true;
            try
            {
                Task.Factory.StartNew((s) => SaveInfoToFile(instantaneousDatas), TaskContinuationOptions.None).ContinueWith(r => { try { r.Wait(); } catch (AggregateException ex) { } });
            }
            catch (Exception ex)
            {
                result = false;
                //SystemLogHelper.Logger.Error(ex.Message, ex);
            }

            return result;
        }

        public bool UploadAddUpData(ICollection<AddUpData> addUpDatas)
        {
            bool result = true;
            try
            {
                Task.Factory.StartNew(() => SaveDataFile(AddUpdataPath, addUpDatas, LockAddUpData)).ContinueWith(r => { try { r.Wait(); } catch (AggregateException ex) { } }, TaskContinuationOptions.None);
            }
            catch (Exception ex)
            {
                //SystemLogHelper.Logger.Error("累计值数据存储！", ex);
                result = false;
            }

            return result;
        }

        private void SaveInfoToFile(IEnumerable<InstantaneousDatas> data)
        {
            try
            {
                SaveDataFile(InstantaneousDatasPath, data, LockInstantaneousDatas);
            }
            catch (Exception ex)
            {
                //SystemLogHelper.Logger.Error("瞬时值存储!", ex);
            }
        }

        private void SaveDataFile<T>(string dataPath, IEnumerable<T> data, object obj) where T : AutoData
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            //为了避免每次添加数据都判断一次路径，存放是按照日期（天）/参数id.txt
            //根据参数集合的日期来分组
            var dayGroup = data.GroupBy(a => a.CollectionTime.ToString("yyyy-MM-dd"));
            foreach (IGrouping<string, T> grouping in dayGroup)
            {
                //判断并获取路径
                var path = CheckPath(dataPath, grouping.Key);
                //根据参数id分组
                var pGroup = grouping.GroupBy(a => a.MeterFieldTypeId);
                foreach (IGrouping<int, T> datases in pGroup)
                {
                    var filePath = CheckFilePath(path, datases.Key);
                    Task.Factory.StartNew((s) =>
                    {
                        StringBuilder param = new StringBuilder();
                        MemoryStream stream = null;
                        foreach (T item in datases)
                        {
                            if (item.CollectionTime.ToString("yyyy-MM-dd") == "0001-01-01")
                            {
                                continue;
                            }

                            try
                            {
                                stream = new MemoryStream();
                                serializer.WriteObject(stream, item);
                                var addStr = Encoding.UTF8.GetString(stream.ToArray());
                                if (!string.IsNullOrEmpty(addStr))
                                {
                                    param.Append(addStr + ",");
                                }
                            }
                            catch
                            {
                                //SystemLogHelper.Logger.Error("序列化对象失败！" + item.CollectionTime + ":" + item.MeterValue);
                            }
                            finally
                            {
                                if (stream != null)
                                {
                                    stream.Close();
                                    stream.Dispose();
                                    stream = null;
                                }
                            }
                        }

                        if (param.Length > 0)
                        {
                            bool checkWrite = false;
                            //当数据插入的时候报错，继续插入，直到成功为止
                            do
                            {
                                lock (obj)
                                {
                                    try
                                    {
                                        var sstream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                                        StreamWriter sReader = new StreamWriter(sstream, Encoding.UTF8);
                                        sReader.Write(param.ToString());
                                        sReader.Flush();
                                        sReader.Close();
                                        sReader.Dispose();
                                        sstream.Close();
                                        sstream.Dispose();
                                        checkWrite = true;

                                    }
                                    catch (Exception ex)
                                    {
                                        //SystemLogHelper.Logger.Error("文件写入失败,重新写入！", ex);
                                    }
                                }
                            } while (!checkWrite);
                        }

                    }, TaskContinuationOptions.None).ContinueWith(r => { try { r.Wait(); } catch (AggregateException ex) { } });
                }
            }

        }

        private string CheckPath(string path, string time)
        {
            var filePath = path + time + "\\";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            };
            return filePath;
        }

        private string CheckFilePath(string path, int pId)
        {
            var filePath = path + pId + ".txt";
            lock (LockCreateFile)
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
            }
            return filePath;
        }
    }
}
