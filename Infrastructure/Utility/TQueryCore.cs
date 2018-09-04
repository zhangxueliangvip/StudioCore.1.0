using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Infrastructure.Utility
{
    public class TQueryCore
    {

        private Dictionary<string, object> _item = new Dictionary<string, object>();
        public Dictionary<string, object> QueryString
        {
            get { return _item; }
        }


        public int Count
        {
            get
            {
                return _item.Count;
            }
        }

        public TQueryCore() { }

        public TQueryCore(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string _query = new Regex("^\\?").Replace(query, "");
                string[] tokens = _query.Split('&');

                foreach (string token in tokens)
                {
                    string[] item = token.Split('=');

                    if (item.Length >= 2 && item[0].Length > 0)
                    {
                        this[item[0]] = item[1];
                    }
                }

            }
        }

        public string Query
        {
            get
            {
                if (_item == null || _item.Count == 0) return string.Empty;

                string query = string.Empty;

                foreach (string key in _item.Keys)
                {
                    query += string.Format("&{0}={1}", key, _item[key]);
                }

                query = new Regex("^&").Replace(query, "?");

                return query;

            }
        }

        public string ToSqlQuery()
        {
            if (_item == null || _item.Count == 0) return string.Empty;

            string query = string.Empty;

            foreach (string key in _item.Keys)
            {
                if (key.StartsWith("s_", StringComparison.OrdinalIgnoreCase))
                    query += string.Format(" and {0}={1}", key.Substring(2), _item[key]);
            }

            query = new Regex("^ and").Replace(query, "");

            return query;
        }

        public override string ToString()
        {
            return Query;
        }

        public void Add(string key, object value)
        {
            if (value == null) _item.Remove(key);

            _item.Add(key.ToLower(), value);
        }


        public void Remove(string key)
        {
            _item.Remove(key.ToLower());
        }

        public object this[string key]
        {
            get
            {
                if (!_item.ContainsKey(key.ToLower())) return string.Empty;

                return _item[key.ToLower()];
            }
            set
            {
                _item[key.ToLower()] = value;
            }
        }



        public static TQueryCore PageQuery
        {
            get
            {
                TQueryCore query = new TQueryCore(System.Web.HttpContext.Current.Request.Url.Query);
                return query;
            }
        }


        #region Request


        public static string GetSafeString(string key)
        {
            return UtilsCore.GetSafeString(GetString(key, ""));
        }
        public static string GetSafeString(string key, int maxLength)
        {
            string back = UtilsCore.GetSafeString(GetString(key, ""));
            return UtilsCore.Abbreviate(back, maxLength, "");
        }
        public static string GetString(string key)
        {
            return GetString(key, "");
        }

        public static string GetString(string key, string def)
        {
            string ret = UtilsCore.ParseString(System.Web.HttpContext.Current.Request[key]);
            return (ret == string.Empty) ? def : ret;
        }

        public static string GetNumber(string key)
        {
            return GetNumber(key, 0);
        }

        /// <summary>
        /// 取字符串，大于长度返回空
        /// </summary>
        /// <param name="key"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string Get(string key, int size)
        {
            string temp = GetString(key);
            return temp.Length > size ? string.Empty : temp;
        }

        public static string GetNumber(string key, int def)
        {
            return GetInt(key, def).ToString();
        }

        public static int GetInt(string key)
        {
            return GetInt(key, 0);
        }
        public static DateTime GetDateTime(string key)
        {
            return IntegerCore.ParseDateTime(GetSafeString(key), new DateTime(1900, 1, 1));
        }


        public static int GetInt(string key, int def)
        {
            return UtilsCore.ParseInt(System.Web.HttpContext.Current.Request[key], def);
        }

        public static decimal GetDecimal(string key, decimal def)
        {
            return UtilsCore.ParseDecimal(System.Web.HttpContext.Current.Request[key], def);
        }
        public static decimal GetDecimal(string key)
        {
            return UtilsCore.ParseDecimal(System.Web.HttpContext.Current.Request[key], decimal.Zero);
        }

        public static T GetContent<T>(string key, T def)
        {
            T result;

            string o = System.Web.HttpContext.Current.Request[key];

            if (string.IsNullOrEmpty(o))
                result = def;
            else
            {
                try
                {
                    object temp = new object();
                    temp = o;
                    result = (T)temp;
                }
                catch
                {
                    result = def;
                }
            }
            return result;
        }



        public static string GetFomart(string key, string format)
        {
            return GetFomart(key, format, "");
        }

        public static string GetFomart(string key, string format, string def)
        {
            string tstr = UtilsCore.ParseString(System.Web.HttpContext.Current.Request[key]);
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(format);
            if (reg.IsMatch(tstr)) return tstr; else return def;
        }


        #endregion
    }
}
