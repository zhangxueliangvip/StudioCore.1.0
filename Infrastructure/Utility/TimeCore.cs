using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Utility
{
    public class TimeCore
    {
        /// <summary>
        /// 判断某个日期是否是一个月的最后一天
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool CheckIsMonthEnd(DateTime time)
        {
            DateTime tempDate = time.AddDays(1);
            return (tempDate.Month != time.Month);
        }

        /// <summary>
        /// 根据个数返回日期列表
        /// </summary>
        /// <param name="time"></param>
        /// <param name="monthCount"></param>
        /// <returns></returns>
        public static List<DateTime> GetListDate(DateTime time, int monthCount)
        {
            List<DateTime> listDT = new List<DateTime>();
            for (int i = 0; i < monthCount; i++)
            {
                listDT.Add(time.AddMonths(i));
            }
            return listDT;
        }

        public static string GetLeftTimeString(DateTime time)
        {
            int days = (time - DateTime.Now).Days;
            int hours = (time - DateTime.Now).Hours;
            int minutes = (time - DateTime.Now).Minutes;
            if (days > 0)
            {
                return days + "天";
            }
            else if (hours > 0)
            {
                return hours + "小时";
            }
            else
            {
                minutes = minutes < 0 ? 0 : minutes;
                return minutes + "分钟";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="offset">周一为第一天偏移1</param>
        /// <returns></returns>
        public static int GetWeekOfYear(DateTime date, int offset)
        {
            DateTime firstDate = new DateTime(date.Year, 1, 1);
            return (date.DayOfYear + (int)firstDate.DayOfWeek - 1 - offset) / 7 + 1;
        }


        public static int GetWeekOfYear(DateTime date)
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(date.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = date.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //    刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        public static string GetWeekName(DayOfWeek week)
        {
            switch (week)
            {
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                case DayOfWeek.Sunday:
                    return "星期日";
            }

            return "未知";
        }

        /// <summary>
        /// 从1970年1月1日00:00:00至今的秒数 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }


        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;


                if (ts.TotalDays >= 1 && ts.TotalDays <= 30)
                {
                    dateDiff = Convert.ToInt32(ts.TotalDays) + " 天前";
                }
                else if (ts.TotalDays > 30 && ts.TotalDays <= 365)
                {

                    dateDiff = Convert.ToInt32(ts.TotalDays / 30) + " 月前";

                }
                else if (ts.TotalDays > 365)
                {

                    dateDiff = Convert.ToInt32(ts.TotalDays / (30 * 12)) + " 年前";

                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }

        /// <summary>
        /// (秒)时间转Int
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ConvertToDateTimeStr(DateTime time)
        {
            try
            {
                return time.ToString("yyyyMMddhhmmss");
            }
            catch (Exception ex)
            {
                LOGCore.Trace(LOGCore.ST.Day, "【TimeHelper】", ex.ToString());
                return "0";
            }
        }
    }
}
