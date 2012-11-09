using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using BookSleeve;

namespace Plat.CacheService
{
    /// <summary>
    /// 调用redis的示例程序
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //PutString();
            //PutList();
            PutXmlString();
        }

        static void PutXmlString()
        {
            XDocument xDoc = XDocument.Load(new System.IO.StreamReader("dt_sample.xml"));
            string s = xDoc.ToString();

            RedisManager rm = new RedisManager();
            rm.InsertString("myxmlstring", s);

            string sCached = rm.GetString("myxmlstring");

            //deleted
            rm.DeleteXElement("myxmlstring", 2);
            string sDel = rm.GetXDocument("myxmlstring").ToString();

            //inserted
            string insXml = "<DataSet><DataTable><DataRow><ID>2</ID><cod>P</cod><txt>涤</txt><supID>1</supID><suptxt>面料</suptxt><supcod>F</supcod><lvl>2</lvl><dir>1</dir></DataRow></DataTable></DataSet>";
            rm.SaveXElement("myxmlstring", 2, insXml);
            string sCachedIns = rm.GetXDocument("myxmlstring").ToString();
        }

        /// <summary>
        /// 插入和读取字符串类型
        /// </summary>
        static void PutString()
        {
            RedisManager rm = new RedisManager();
            rm.InsertString<string>("mykey", " money");
            var value = rm.GetString("mykey");
                
            Console.WriteLine(value);
            //Console.ReadLine();
        }

        /// <summary>
        /// 插入和读取列表类型
        /// </summary>
        static void PutList()
        {
            List<string> lstWeek = new List<string>();
            lstWeek.Add("Monday");
            lstWeek.Add("Wedday");
            lstWeek.Add("Sunday");

            RedisManager rm = new RedisManager();
            rm.InsertList<string>("mylist", lstWeek);

            string mstr = rm.GetListItem("mylist", 0);
            Debug.Assert("Monday"== mstr, "it's wrong");

            List<string> lst = rm.RangeList("mylist", 0, 1).ToList();
            Console.WriteLine(lst.Count);
            Console.ReadLine();
            
        }
    }
}
