using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ReadXmlSpeedComparsion
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> entities = new List<Entity>();
            for (int i = 0; i < 1000; i++)
            {

                Entity entity = new Entity
                  {
                      AA = "121212",
                      BB = "231234",
                      CC = "ejlwer",
                      DD = "2323we",
                      EE = "23df23f",
                      FF = "23df3rdf",
                      HH = "asdf323d",
                      II = "14124asd",
                      JJ = "143243kld"
                  };

                List<SubEntity> subList = new List<SubEntity>();
                for (int j = 0; j < 20; j++)
                {
                    subList.Add(new SubEntity
                    {
                        KK = "2werqe",
                        LL = "asd23ds",
                        MM = "asdfrqr234",
                        NN = "4123werqwe"
                    });
                }
                entity.SubEntities = subList;
                entities.Add(entity);
            }

            var xmlstr = XmlSerializeHelper.ConverterEntityToXmlStr<Entity>(entities);
            XmlSerializeHelper.WriteEntityToXmlFile(xmlstr, @"D:\test.xml");

            Console.WriteLine("read xml file using xPath technology");
            Console.WriteLine("开始时间" + DateTime.Now.ToString("hh:mm:ss:fff"));
            var xml = XmlSerializeHelper.LoadXmlFile(@"D:\test.xml");
            List<Entity> entitylist = XmlSerializeHelper.ConverterXmlToEntity<Entity>(xml);
            Console.WriteLine(entitylist[0].AA + " SubEntity " + entitylist[0].SubEntities[0].KK);
            Console.WriteLine("开始时间" + DateTime.Now.ToString("hh:mm:ss:fff"));

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\test.xml");
            Console.WriteLine("开始时间" + DateTime.Now.ToString("hh:mm:ss:fff"));
            var node = xmlDoc.SelectNodes("ArrayOfEntity//Entity")[0];
            Console.WriteLine(node.FirstChild.InnerText);
            var subnode = xmlDoc.SelectNodes("ArrayOfEntity//Entity//SubEntities//SubEntity")[0];
            Console.WriteLine(subnode.FirstChild.InnerText);
            Console.WriteLine("结束时间" + DateTime.Now.ToString("hh:mm:ss:fff"));
            Console.ReadKey();
            Console.WriteLine("read xml file using serializer technology");

            Console.ReadKey();
        }
    }
}
