using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ReadXmlSpeedComparsion
{
    public class XmlSerializeHelper
    {
        public static string LoadXmlFile(string filePath)
        {
            TextReader reader = new StreamReader(filePath, Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Dispose();
            reader.Close();

            return content;
        }

        public static void WriteEntityToXmlFile(string xml, string filepath)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            TextWriter writer = new StreamWriter(filepath, false);
            xmlDoc.Save(writer);
            writer.Close();
        }

        public static List<T> ConverterXmlToEntity<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            XmlReader reader = new XmlNodeReader(xmldoc);
            var obj = (List<T>)serializer.Deserialize(reader);
            return obj;
        }

        public static string ConverterEntityToXmlStr<T>(List<T> list)
        {
            var serializer = new XmlSerializer(list.GetType());
            var strWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(strWriter);
            serializer.Serialize(xmlWriter, list);
            return strWriter.ToString();
        }


    }

    public class Entity
    {
        public string AA { get; set; }
        public string BB { get; set; }
        public string CC { get; set; }
        public string DD { get; set; }
        public string EE { get; set; }
        public string FF { get; set; }
        public string HH { get; set; }
        public string II { get; set; }
        public string JJ { get; set; }

        public List<SubEntity> SubEntities { get; set; }
    }

    public class SubEntity
    {
        public string KK { get; set; }
        public string LL { get; set; }
        public string MM { get; set; }
        public string NN { get; set; }

    }
}
