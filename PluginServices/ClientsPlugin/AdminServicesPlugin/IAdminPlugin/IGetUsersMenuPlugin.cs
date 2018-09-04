using Domain.BaseModels;
using Domain.DTOModels;
using Domain.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsPlugin.AdminServicesPlugin
{
   public interface IGetUsersMenuPlugin
    {
        /// <summary>
        /// 获取用户的菜单列表
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <returns></returns>
        TBaseResult<PagesModels> GetUsersMenuList(string safetySecretKey, bool isUsable,string userOpenId = "");
    }
}
