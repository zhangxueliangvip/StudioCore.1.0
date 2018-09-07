using Autofac;
using AutofacProxy;
using ClientsPlugin.AdminServicesPlugin;
using Domain.DTOModels;
using Domain.QueryModels;
using Infrastructure.Utility;
using IocCorePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.studiocore.com
{
    public class BaseCore
    {
        #region 用户信息
        public static UsersModels GetUsersInfo
        {
            get
            {
                var obj = RedisCore.GetInstance.Get(Config.SessionUserInfo);
                var userOpenId = obj == null || obj.ToString() == "null" ? string.Empty : obj.ToString();
                var result = IocPlugin.IocGetUsersInfoPlugin.GetUsersInfo(PluginCore.GetInstance.SafetySecretValue, true, userOpenId);
                if (result.Code == (int)EnumCore.CodeType.成功)
                {
                    return result.TData;
                }
                return new UsersModels();
            }
        }
        #endregion

        #region 菜单
        public static List<PagesQueryItem> GetPageList
        {
            get
            {
                var obj = RedisCore.GetInstance.Get(Config.SessionUserInfo);
                var userOpenId = obj == null || obj.ToString() == "null" ? string.Empty : obj.ToString();
                var result = IocPlugin.IocGetUsersMenuPlugin.GetUsersMenuList(PluginCore.GetInstance.SafetySecretValue, true, userOpenId);
                if (result.Code == (int)EnumCore.CodeType.成功)
                {
                    return result.TList;
                }
                return new List<PagesQueryItem>();
            }
        }

        public static List<PagesQueryItem> GetPageChildList(int id)
        {
            var result = IocPlugin.IocGetUsersChildMenuPlugin.GetUsersChildMenuList(PluginCore.GetInstance.SafetySecretValue, true, id);
            if (result.Code == (int)EnumCore.CodeType.成功)
            {
                return result.TList;
            }
            return new List<PagesQueryItem>();
        }
        #endregion

        #region 日志
        public static void AddLog(string title,string content,string creater="127.0.0.1",string remark="日志记录")
        {
            var entity = new LogRecordModels() {
                LogTitle=title,
                LogContents=content,
                Creater=creater,
                Remark=remark
            };
            IocPlugin.IocAddDataLogRecordPlugin.AddDataLogRecord(entity);
        }
        #endregion

        #region 配置

        #endregion
    }
}
