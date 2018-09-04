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
   public interface IDeleteDataIconsPlugin
    {
        /// <summary>
        /// 删除图标数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResult DeleteDataIcons(string safetySecretKey, bool isUsable, string openId);
    }
}
