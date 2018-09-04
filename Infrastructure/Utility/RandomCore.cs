using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Utility
{
    public class RandomCore
    {

        #region 生成N位随机数
        /// <summary>
        /// 生成N位随机数 
        /// </summary>
        /// <param name="N">N位随机数</param>
        /// <returns>生成的N位随机数</returns>
        public static string RandCode(int N)
        {
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z' };
            StringBuilder num = new StringBuilder();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < N; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }
            return num.ToString();
           
        }
        public static string ReandNumlet(int N)
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < N; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }
            return str;
        }
        //public static List<string> GetRandom(int count)
        //{

        //    var list = new List<string>();
        //    for (int i = 0; i < count; i++)
        //    {
        //        var tempStr = RandCode(4);
        //        if (list.Contains(tempStr))
        //        {
        //            i--;
        //            continue;
        //        }
        //        list.Add(tempStr);
        //    }
        //    return list;

        //}
        #endregion

    }
}
