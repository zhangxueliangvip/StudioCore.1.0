using System;
using System.Text.RegularExpressions;

namespace Infrastructure.Utility
{
    /// <summary>
    /// 提供有关数值型的静态操作方法
    /// </summary>
    public class IntegerCore
    {

        private static Regex _isNumber = new Regex(@"^\d+$");


        /// <summary>
        /// 字符串到整数得转化（转化失败返回0）。
        /// </summary>
        /// <param name="str">要转换得字符串</param>
        /// <returns>整数值</returns>
        /// 
        public static int Parse(string str)
        {
            return Parse(str, 0);
        }

        /// <summary>
        /// 万能转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Parse(object obj)
        {
            if (obj is System.ValueType)
                return Convert.ToInt32(obj);
            else if (obj is System.String)
                return Parse(obj.ToString());
            else return 0;
        }

        /// <summary>
        ///  对比int 跟enum
        /// </summary>
        /// <param name="num"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool CompareIntAndEnum(int num, Enum e)
        {
            return num == Convert.ToInt32(e);
        }


        public static int Parse(object obj, int default_value)
        {
            try
            {
                if (obj.GetType().IsEnum) return Convert.ToInt32(obj);
                else return Convert.ToInt32(Convert.ToDouble(obj.ToString()));
            }
            catch
            {
                return default_value;
            }
        }

        /// <summary>
        /// 字符串到货币的转化（转化失败返回0）。
        /// </summary>
        /// <param name="str">要转换得字符串</param>
        /// <returns>货币值</returns>
        public static decimal ParseDecimal(string str)
        {
            return ParseDecimal(str, 0);
        }

        public static decimal ParseDecimal(object o)
        {
            if (o == null)
            {
                return decimal.Zero;
            }
            else
            {
                return ParseDecimal(o.ToString());
            }
        }

        public static decimal ParseDecimal(object obj, int default_value)
        {
            try
            {
                if (obj.GetType().IsEnum) return Convert.ToDecimal(obj);
                else return Convert.ToDecimal(obj.ToString());
            }
            catch
            {
                return default_value;
            }
        }
        public static DateTime ParseDateTime(object obj)
        {
            return ParseDateTime(obj, DateTime.Now);
        }

        public static DateTime ParseDateTime(object obj, DateTime default_value)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return default_value;
            }
        }
        public static long ParseInt64(object obj, int default_value)
        {
            try
            {
                if (obj.GetType().IsEnum) return Convert.ToInt64(obj);
                else return Convert.ToInt64(Convert.ToDouble(obj.ToString()));
            }
            catch
            {
                return default_value;
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
        public static decimal ParseDecimalSingle(string str, decimal default_value)
        {
            try
            {
                return decimal.Parse(str);
            }
            catch
            {
                return default_value;
            }
        }
        public static float ParseFloat(string str, float default_value)
        {
            try
            {
                return float.Parse(str);
            }
            catch
            {
                return default_value;
            }
        }

        public static bool IsInteger(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        } //IsInteger


        public static bool IsInArray(int[] array, int value)
        {
            if (array == null || array.Length == 0) return false;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value) return true;
            }

            return false;
        }

    }
}
