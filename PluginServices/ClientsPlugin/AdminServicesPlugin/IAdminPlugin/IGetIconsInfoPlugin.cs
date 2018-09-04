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
   public interface IGetIconsInfoPlugin
    {
        /// <summary>
        /// 获取图标信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
         TBaseResult<IconModels> GetIconsInfo(string safetySecretKey, bool isUsable,string openId);
    }
}
