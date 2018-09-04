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
   public interface IGetRolesInfoPlugin
    {
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
         TBaseResult<RolesModels> GetRolesInfo(string safetySecretKey, bool isUsable,string openId);
    }
}
