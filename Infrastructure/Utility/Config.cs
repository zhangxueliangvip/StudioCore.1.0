using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
namespace Infrastructure.Utility
{
    public class Config
    {


        public static readonly string SessionUserInfo = "UserInfo";
        public static readonly string GetLoginUrl = "/Users/Login";
        public static readonly string MySqlConnectionString = @"server=localhost;database=StudioCore;uid=root;password=123123;charset=utf8;SslMode=None";
        public static readonly string CurrentBuildModelPath = @"\Domain\DBModels\";
        public static readonly string BuildNamespace = "Domain.Models";
        public static readonly string RedisConStr = "127.0.0.1:6379,abortConnect=false,connectRetry=3,connectTimeout=3000,defaultDatabase=1,syncTimeout=3000,version=3.2.1,responseTimeout=3000";
        public static readonly string SysName = "微领域";
        public static readonly string SysPostfix = "管理系统";
        public static readonly string SessionVerifyCode = "SessionVerifyCode";
        public static readonly string SafetySecretKey = "SafetySecretKey";









        /// <summary>
        /// 系统编码
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.3) Gecko/20070309 Firefox/2.0.0.3";
        /// <summary>
        /// 系统自定义加密
        /// </summary>
        public static readonly string SignKey = MD5Core.GetStringMD5("nmtaiwoo-2017");
        public static string DomainCookie
        {
            get
            {
                string temp = ConfigurationManager.AppSettings["DomainCookie"];
                return string.IsNullOrWhiteSpace(temp) ? string.Empty : temp;
            }
        }



        #region 验证码配置
        public static int Validate = 4;
        public static int ValidateWidth = 148;
        public static int ValidateHeight = 40;
        public static int ValidateFontsize = 20;
        #endregion
        #region 系统配置
        public static string GetLogo = "/Content/img/logo.png";
        public static string GetLoginTitle = "微领域后台管理系统";
        public static string GetLoginWelCome = "欢迎使用";
        public static string GetLoginBackgroundImg = "/Content/img/bg3.jpg";
        //public static string GetLoginUrl = "/Member/Login";
        public static string GetErrorUrl = "/Home/Error";
        public static string GetSysName = "微领域";
        public static string GetSysFullName = "微领域后台管理系统";
        public static string GetVersionNum = "V1.0";
        public static string DefaultController = "Home";
        public static string DefaultAPIController = "Home";
        public static string DefaultAPIAction = "Index";
        public static string DefaultAction = "Index";
        public static string DefaultRoutingRule = "{controller}/{action}/{id}";
        public static string DefaultAPIRoutingRule = "api/{controller}/{action}/{id}";
        #endregion
        #region Cookie配置
        public static string CookieToken = "token";
        #endregion
        #region Session配置
        public static string SessionValidate = "ValidateCode";
        public static string SessionToken = "token";
        #endregion
        #region cache配置
        public static string CacheUsersInfo = "UsersInfo";
        #endregion
        #region 操作配置
        public static string GetStrFormat = "-";
        public static int SetTokenExpiresHour = 2;
        #endregion
        #region 日志管理
        public static string LogAbsolutePath = "~/log/";
        public static string LogRelativePath = "/log/";
        public static string LogDefaultPrefix = "log";
        #endregion

        #region Redis
        public static int RDdfaultPort = 6379;
        public static string RHost = "127.0.0.1";
        #endregion


    }
}
