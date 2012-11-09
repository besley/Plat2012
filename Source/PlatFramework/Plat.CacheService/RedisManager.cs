using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Plat.CacheService
{
    public class RedisManager
    {
        private static readonly int RedisStoreNumber = 12;
        private const string RedisOperationFailed = "Redis 操作失败!";

        /// <summary>
        /// 获取对应键的值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>字符串值</returns>
        public string GetString(string key)
        {
            string s = string.Empty;
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                var task = conn.Strings.GetString(RedisStoreNumber, key);
                s = conn.Wait(task);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }

            return s;
        }

        /// <summary>
        /// 插入键和值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">数值</param>
        public void InsertString<T>(string key, T value)
        {
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                conn.Strings.Set(RedisStoreNumber, key, value.ToString());
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
        }

        /// <summary>
        /// 获取xml节点的缓存对象
        /// </summary>
        /// <param name="key">指定的键值</param>
        /// <returns></returns>
        public XDocument GetXDocument(string key)
        {
            string strXml = GetString(key);
            XDocument xDoc = XDocument.Load(
                 new MemoryStream(System.Text.Encoding.UTF8.GetBytes(strXml))
                 );
            return xDoc;
        }

        /// <summary>
        /// 插入xml文档内容的缓存对象
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="xDoc">xml文档记录xml格式：<![CDATA[<DataSet><DataTable><DataRow><id>4</id><name>aaa</name></DataRow></DataTable></DataSet>]]></param>
        public void InsertXDocument(string key, XDocument xDoc)
        {
            InsertString(key, xDoc.ToString());
        }

        /// <summary>
        /// 保存方法包括插入或者编辑
        /// 说明：
        /// 1). 如果节点已经存在，则进行更新
        /// 2). 如果节点不存在，则插入
        /// </summary>
        /// <param name="key">缓存对象的key</param>
        /// <param name="id">新记录的Id</param>
        /// <param name="strXml">记录xml格式：<![CDATA[<DataSet><DataTable><DataRow><id>4</id><name>aaa</name></DataRow></DataTable></DataSet>]]></param>
        public void SaveXElement(string key, long id, string strXml)
        {
            var conn = RedisConnectionFactory.Current.GetConnection();
            string xmlContent = GetString(key);
            
            XDocument xDoc = XDocument.Load(
                new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlContent))
                );
            
            var node = from nm in xDoc.Element("DataSet").Element("DataTable").Elements("DataRow")
                where (string)nm.Element("ID") == id.ToString()
                select nm;

            if (node.Count<XElement>() == 0)
            {
                //插入操作
                var insXml = XDocument.Load(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(strXml)));
                var snode = insXml.Element("DataSet").Element("DataTable").Element("DataRow");
                           
                xDoc.Element("DataSet").Element("DataTable").Add(snode);
            }
            else
            {
                //编辑操作
                foreach (XElement e in node)
                {
                    e.SetElementValue("DataRow", strXml);
                    break;
                }
            }
            InsertString(key, xDoc.ToString());
        }

        /// <summary>
        /// 删除指定ID的记录ds
        /// </summary>
        /// <param name="key">缓存对象的key</param>
        /// <param name="id">要删除记录的Id</param>
        public void DeleteXElement(string key, long id)
        {
            var conn = RedisConnectionFactory.Current.GetConnection();
            string xmlContent = GetString(key);

            XDocument xDoc = XDocument.Load(
                new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlContent))
                );
            var node = from nm in xDoc.Element("DataSet").Element("DataTable").Elements("DataRow")
                     where (string)nm.Element("ID") == id.ToString()
                     select nm;
            node.Remove();

            InsertString(key, xDoc.ToString());
        }

        /// <summary>
        /// 插入列表数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="lst">列表</param>
        public void InsertList<T>(string key, IList<T> lst)
        {
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                for (int i = 0; i < lst.Count; i++)
                {
                    conn.Lists.AddLast(RedisStoreNumber, key, lst[i].ToString(), true);
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
        }

        /// <summary>
        /// 追加数据到列表末尾
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">数值</param>
        public void AppendListItem<T>(string key, T value)
        {
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                conn.Lists.AddLast(RedisStoreNumber, key, value.ToString());
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
        }

        /// <summary>
        /// 插入数据到列表某个位置后
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="startValue">开始值</param>
        /// <param name="insValue">要插入的值</param>
        public void InsertListItem<T>(string key, T startValue, T insValue)
        {
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                conn.Lists.InsertAfter(RedisStoreNumber, key, startValue.ToString(), insValue.ToString());
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
        }

        /// <summary>
        /// 移除列表中的某个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="value">要移除的值</param>
        public void RemoveListItem<T>(string key, T value)
        {
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                conn.Lists.Remove(RedisStoreNumber, key, value.ToString());
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
        }

        /// <summary>
        /// 选择指定范围的列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="start">开始位置</param>
        /// <param name="position">停止位置</param>
        /// <returns>字符串值的列表</returns>
        public IList<string> RangeList(string key, int start, int position)
        {
            IList<string> lst = new List<string>();
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                var task = conn.Lists.Range(RedisStoreNumber, key, start, position);
                conn.Wait(task);
                byte[][] bytes = task.Result;
                for (int i = 0; i < bytes.Length; i++)
                {
                    string s = System.Text.Encoding.UTF8.GetString(bytes[i]);
                    lst.Add(s);
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
            return lst;
        }

        /// <summary>
        /// 获取列表指定索引的值
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="index">索引</param>
        /// <returns>字符串</returns>
        public string GetListItem(string key, int index)
        {
            string s = string.Empty;
            try
            {
                var conn = RedisConnectionFactory.Current.GetConnection();
                var task = conn.Lists.Get(RedisStoreNumber, key, index);
                conn.Wait(task);
                s = System.Text.Encoding.UTF8.GetString(task.Result);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                throw new System.Exception(RedisOperationFailed, ex);
            }
            
            return s;
        }
    }
}
