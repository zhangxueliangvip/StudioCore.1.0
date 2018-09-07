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
   public interface IGetUsersChildMenuPlugin
    {
        /// <summary>
        /// 获取用户的子菜单列表
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <returns></returns>
        TBaseResult<PagesQueryItem> GetUsersChildMenuList(string safetySecretKey, bool isUsable,int id);
    }
}
