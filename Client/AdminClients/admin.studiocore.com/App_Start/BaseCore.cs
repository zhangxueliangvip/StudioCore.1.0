﻿using Autofac;
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
        public static UsersModels GetUsersInfo
        {
            get
            {
                var obj = RedisCore.GetInstance.Get(Config.SessionUserInfo);
                var userOpenId = obj == null || obj.ToString() == "null" ? string.Empty : obj.ToString();
               var result= IocPlugin.IocGetUsersInfoPlugin.GetUsersInfo(PluginCore.GetInstance.SafetySecretValue, true, userOpenId);
                if (result.Code == (int)EnumCore.CodeType.成功)
                {
                    return result.TData;
                }
                return new UsersModels();
            }
        }

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
    }
}
