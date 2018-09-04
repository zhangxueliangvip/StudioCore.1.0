using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Infrastructure.Utility
{
    public class CookieCore
    {
        public static int defaultExpires = 525600;  // 7 天
        public HttpRequestBase Request { get; private set; }
        public HttpResponseBase Response { get; private set; }

        public CookieCore(HttpRequestBase request, HttpResponseBase response)
        {
            Request = request;
            Response = response;
        }

        public string DefaultDomain
        {
            get
            {
                var domain = Config.DomainCookie;
                if (string.IsNullOrEmpty(domain))
                {
                    var uri = Request.Url;
                    return uri.Host;
                }

                return domain;
            }
        }

        /// <summary>
        /// 清除某个 cookie 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            SetCookie(DefaultDomain, key, string.Empty, -100, true);
        }

        /// <summary>
        /// 返回Cookie值，解密
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCookie(string key)
        {
            return GetCookie(key, false);
        }

        /// <summary>
        /// 设置cookie, 加密存储
        /// </summary>
        public void SetCookie(string key, string value)
        {
            SetCookie(key, value, defaultExpires);
        }

        /// <summary>
        /// 设置cookie, 加密存储, cookie 有效期为当前 session
        /// </summary>
        public void SetSessionCookie(string key, string value)
        {
            SetCookie(key, value, 0);
        }

        /// <summary>
        /// 设置cookie, 加密存储
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        public  void SetCookie(string key, string value, int minute)
        {
            SetCookie(DefaultDomain, key, value, minute, true, false);
        }

        /// <summary>
        /// 设置 cookie
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="name"></param>
        /// <param name="value">如果 value 为空，为删除 cookie</param>
        /// <param name="minute">如果 minute &lt;=0, 则为 no-presist cookie(session cookie)</param>
        /// <param name="isCrypto">是否加密存储</param>
        public  void SetCookie(string domain, string name, string value, int minute, bool isCrypto)
        {
            SetCookie(domain, name, value, minute, false, isCrypto);
        }

        /// <summary>
        /// 设置 cookie
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="name"></param>
        /// <param name="value">如果 value 为空，为删除 cookie</param>
        /// <param name="minute">如果 minute &lt;=0, 则为 no-presist cookie(session cookie)</param>
        /// <param name="httponly">是否为 httpOnly cookie</param>
        /// <param name="isCrypto">是否加密存储</param>
        public  void SetCookie(string domain, string name, string value, int minute, bool httpOnly, bool isCrypto)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (string.IsNullOrEmpty(value))
            {
                var oldCookie = Response.Cookies[name];

                if (oldCookie == null)
                {
                    return;
                }
                oldCookie.Expires = DateTime.Now.AddDays(-10);
                oldCookie.Value = null;
                oldCookie.Domain = domain;
                return;
            }


            if (Request.Browser.Cookies == false)
            {
                return;
            }

            var cookie = Response.Cookies[name];

            if (cookie == null)
            {
                cookie = new HttpCookie(name);
            }

            if (minute > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(minute);
            }

            cookie.Domain = domain;

            if (Response.Cookies[name] != null)
            {
                Response.Cookies.Remove(name);
                Request.Cookies.Remove(name);
            }

            if (isCrypto)
            {
                cookie.Value = SecurityCore.Encode(value);
            }
            else
            {
                cookie.Value = value;
            }
            cookie.HttpOnly = httpOnly;

            Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 设置 cookie, 客户端可以获取使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        public  void SetClientCookie(string name, string value, int minute)
        {
            SetCookie(DefaultDomain, name, value, minute, false, false);
        }
        public  void SetClientCookie(string name, string value)
        {
            SetClientCookie(name, value, defaultExpires);
        }


        public  string GetCookie(string name, bool isCrypto)
        {
            if (Request == null)
            {
                return string.Empty;
            }

            HttpCookie cookie = Request.Cookies[name];

            if (cookie == null || cookie.Value == null)
            {
                return string.Empty;
            }

            if (isCrypto)
            {
                return SecurityCore.Decode(cookie.Value);
            }
            else
            {
                return cookie.Value;
            }

        }

        public  void SetCookie(string name, string value, int minute, bool isCrypto)
        {
            SetCookie(DefaultDomain, name, value, minute, isCrypto);
        }

    }
}
