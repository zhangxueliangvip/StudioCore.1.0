using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Utility
{
   public class CacheCore
    {
        protected static volatile System.Web.Caching.Cache webCache = System.Web.HttpRuntime.Cache;
        protected static int _timeOut = 3; // 默认缓存存活期为1440分钟(24小时)

        private static object syncObj = new object();


        public static System.Web.Caching.Cache WebCache
        {
            get { return webCache; }
        }

        //设置到期相对时间[单位：／分钟] 
        public static int TimeOut
        {
            set { _timeOut = value > 0 ? value : 0; }
            get { return _timeOut; }
        }

        public static void Add(string key, object obj)
        {


            Add(key, obj, TimeOut);
        }

        public static void Add(string key, object obj, int timeOut)
        {
            if (string.IsNullOrEmpty(key) || obj == null) return;

            DateTime expiration = timeOut == 0 ? DateTime.MaxValue : DateTime.Now.AddMinutes(timeOut);

            webCache.Insert(key, obj, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public static void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            webCache.Remove(key);
        }

        public static void Clear(string prefix)
        {
            IDictionaryEnumerator cacheEnum = webCache.GetEnumerator();
            while (cacheEnum.MoveNext())//找出所有的Cache
            {
                string key = cacheEnum.Key.ToString();

                if (key.StartsWith(prefix)) webCache.Remove(key);
            }
        }


        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;

            return webCache.Get(key);
        }





        public static DataView GetCacheView(string tableName, string connStr)
        {
            DataTable dt = GetCacheTable(tableName, connStr);

            if (dt == null) return null;

            DataView dv = dt.DefaultView;

            dv.RowFilter =string.Empty;

            return dv;
        }


        /// <summary>
        /// 获取远程XML
        /// </summary>
        /// <param name="key"></param>
        /// <param name="xmlUrl"></param>
        /// <returns></returns>
        public static DataSet GetUrlXmlByCache(string key, string xmlUrl)
        {
            DataSet ds = CacheCore.Get("GetCacheXml_" + key) as DataSet;
            if (ds == null)
            {
                string xmlContent = GetHtml(xmlUrl);
                byte[] bt = System.Text.Encoding.UTF8.GetBytes(xmlContent);
                MemoryStream ms = new MemoryStream(bt);
                ds = CacheCore.GetCacheXML(ms, key);
            }
            return ds;
        }

        public static DataSet GetCacheXML(string fileName, string keyName)
        {
            string key = "GetCacheXml_" + keyName;
            DataSet ds = CacheCore.Get(key) as DataSet;
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            ds = new DataSet();
            ds.ReadXml(fileName);
            CacheCore.Add(key, ds);
            return ds;
        }

        public static DataSet GetCacheXML(Stream stream, string keyName)
        {
            string key = "GetCacheXml_" + keyName;

            DataSet ds = CacheCore.Get(key) as DataSet;
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            ds = new DataSet();
            ds.ReadXml(stream);
            CacheCore.Add(key, ds);
            return ds;
        }

        public static DataTable GetCacheTable(string tableName, string connStr)
        {
            string key = "GetCacheTable_" + tableName;

            DataTable dt = CacheCore.Get(key) as DataTable;

            if (dt != null) return dt;

            string sql = "select * from [" + tableName + "]";

            dt = SQLCore.ExecuteDatatable(connStr, CommandType.Text, sql, null);

            if (dt == null || dt.Rows.Count == 0) return null;

            CacheCore.Add(key, dt);

            return dt;

        }

        /// <summary>
        /// 缓存网络字符串
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCacheStrByUrl(string keyName, string url)
        {
            string chcheKey = "GetCacheStr_" + keyName;
            string result = CacheCore.Get(chcheKey) as String;
            if (!string.IsNullOrEmpty(result))
                return result;
            string content = GetHtml(url);
            CacheCore.Add(chcheKey, content);
            return content;
        }
        /// <summary>
        /// 缓存本地txt文件
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetCacheStrByTxt(string keyName, string fileName)
        {

            string chcheKey = "GetCacheStr_" + keyName;
            string result = CacheCore.Get(chcheKey) as String;
            if (!string.IsNullOrEmpty(result))
                return result;
            string content = FileCore.ReadTextFileString(fileName);
            CacheCore.Add(chcheKey, content);
            return content;
        }


        public delegate T InsertCacheFun<T>();
        public static T Get<T>(string key, int timeOut, InsertCacheFun<T> getDataFun)
        {
            object obj = Get(key);
            if (obj == null)
            {
                obj = getDataFun();
                if (timeOut > 0) Add(key, obj, timeOut);
            }
            return (T)obj;
        }

        /// <summary>
        /// 获取html
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            string address = string.Empty;
            return Get(url, null, Config.DefaultUserAgent, Config.DefaultEncoding, ref address);
        }

        /// <returns></returns>
        public static string Get(string url, CookieContainer container, string agent, Encoding encoding, ref string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.CookieContainer = container;
            request.Method = "GET";

            request.Timeout = 5000;
            request.ServicePoint.ConnectionLimit = 1024;
            request.KeepAlive = false;


            HttpWebResponse response = null;
            StreamReader reader = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                address = request.Address.ToString();
                reader = new StreamReader(response.GetResponseStream(), encoding);
                return reader.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (response != null) response.Close();
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }


        }

    }
}
