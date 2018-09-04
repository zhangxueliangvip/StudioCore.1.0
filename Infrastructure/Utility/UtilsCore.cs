using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Web;

namespace Infrastructure.Utility
{
    public class UtilsCore
    {
        private static Random _random = new Random(DateTime.Now.Millisecond);

        public static string ParseString(object input)
        {
            return ParseString(input, string.Empty);
        }

        public static string ParseString(object input, string def)
        {
            if (input == null) return def;
            return input.ToString();
        }

        public static int ParseInt(object input)
        {
            return ParseInt(input, 0);
        }


        public static string DateTostring(DateTime dt)
        {
            return dt.ToString("yyyy年MM月dd日");
        }

        public static int ParseInt(object input, int def)
        {
            if (input == null) return def;
            if (input.ToString() == string.Empty) return def;

            try
            {
                return int.Parse(input.ToString());
            }
            catch
            {
                return def;
            }
        }

        public static Int64 ParseInt64(object input)
        {
            return ParseInt64(input, 0);
        }
        public static Int64 ParseInt64(object input, int def)
        {
            if (input == null) return def;
            if (input.ToString() == string.Empty) return def;

            try
            {
                return Int64.Parse(input.ToString());
            }
            catch
            {
                return def;
            }
        }
        public static DateTime ParseDatetime(object input)
        {
            return ParseDatetime(input, DateTime.Now);
        }

        public static DateTime ParseDatetime(object input, DateTime def)
        {
            if (input == null) return def;
            if (input.ToString() == string.Empty) return def;

            try
            {
                return Convert.ToDateTime(input);
            }
            catch
            {
                return def;
            }
        }

        public static double ParseDouble(string str, float default_value)
        {
            try
            {
                return Convert.ToDouble(str);
            }
            catch
            {
                return default_value;
            }
        }
        public static decimal ParseDecimal(string str, decimal default_value)
        {
            try
            {
                return Convert.ToDecimal(str);
            }
            catch
            {
                return default_value;
            }
        }
        public static bool IsInteger(object input)
        {
            if (input == null) return false;
            Regex regex = new Regex("^(+|-)?\\d{1,11}");
            return regex.IsMatch(input.ToString());
        }

        public static bool IsNumeric(object input)
        {
            if (input == null) return false;
            Regex regex = new Regex("^(+|-)?\\d{1,11}(\\.\\d{1,11})?");
            return regex.IsMatch(input.ToString());
        }

        public static bool IsFloat(object input)
        {
            string pattern = "^\\d+\\.\\d+$";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(input.ToString());
        }


        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="input">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string input)
        {
            if (input == null || input == string.Empty || input.Length < 7 || input.Length > 15) return false;

            string pattern = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        public static string GetIPAddress()
        {
            if (HttpContext.Current == null) return string.Empty;

            string result = string.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(result) && result.IndexOf(".") != -1)
            {
                if (IsIPAddress(result)) return result; //代理即是IP格式 

                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];     //找到不是内网的地址 
                        }
                    }
                }


            }

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }


        public static int GetLength(string input)
        {
            Regex regex = new Regex("[^\x00-\xff]");
            input = regex.Replace(input, "**");
            return input.Length;
        }


        public static string GetSafeString(string input)
        {
            input = new Regex(";|exec", RegexOptions.IgnoreCase | RegexOptions.Singleline).Replace(input, "");
            input = input.Replace("'", "\'");
            return input;
        }


        public static string Abbreviate(string s, int length)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (s.Length > length)
            {
                return s.Substring(0, length - 1) + "..";
            }
            return s;
        }

        /// <summary>
        /// 只取字串中的字母或数字
        /// </summary>
        /// <returns></returns>
        public static string GetNLString(string s)
        {
            if (s == null)
                return string.Empty;
            return new Regex("[^0-9a-zA-Z]").Replace(s, "");
        }

        public static string Abbreviate(string s, int length, string ReplaceStr)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            if (s.Length > length)
            {
                return s.Substring(0, length - 1) + ReplaceStr;
            }

            return s;
        }

        #region 随机数相关

        public static string GetRandomNumber(int length)
        {
            string ret = string.Empty;

            for (int i = 0; i < length; i++)
            {
                ret += GetRandom(10);
            }
            return ret;
        }

        public static int GetRandom(int max)
        {
            return _random.Next(max);
        }

        public static int GetRandom(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static double GetRandom(double min, double max)
        {
            return (double)_random.Next((int)(min * 100), (int)(max * 100)) / 100;
        }




        #endregion
    }
}
