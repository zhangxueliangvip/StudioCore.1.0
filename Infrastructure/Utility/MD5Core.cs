using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Infrastructure.Utility
{
    public static class MD5Core
    {
        public static string GetStringMD5(string str)
        {
            string msg = "";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] newBy = md5.ComputeHash(buffer);

            for (int i = 0; i < newBy.Length; i++)
            {
                msg += newBy[i].ToString("X2");
            }
            return msg;
        }
    }
}