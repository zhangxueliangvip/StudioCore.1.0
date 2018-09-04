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
   public interface IDeleteDataUsersPlugin
    {
        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResult DeleteDataUsers(string safetySecretKey, bool isUsable, string openId);
    }
}
