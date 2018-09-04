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
   public interface IGetPageDataIconsPlugin
    {
        /// <summary>
        /// 图标分页数据
        /// </summary>
        /// <param name="queryItem"></param>
        /// <returns></returns>
         TBaseResult<IconQueryItem> GetPageDataIcons(IconQueryItem queryItem);
    }
}
