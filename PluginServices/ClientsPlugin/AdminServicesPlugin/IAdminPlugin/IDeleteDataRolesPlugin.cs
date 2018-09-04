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
   public interface IDeleteDataRolesPlugin
    {
        /// <summary>
        /// 删除角色数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResult DeleteDataRoles(string safetySecretKey, bool isUsable, string openId);
    }
}
