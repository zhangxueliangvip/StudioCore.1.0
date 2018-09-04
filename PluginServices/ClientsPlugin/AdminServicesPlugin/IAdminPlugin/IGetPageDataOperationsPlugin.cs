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
   public interface IGetPageDataOperationsPlugin
    {
        /// <summary>
        /// 操作分页数据
        /// </summary>
        /// <param name="queryItem"></param>
        /// <returns></returns>
         TBaseResult<OperationsQueryItem> GetPageDataOperations(OperationsQueryItem queryItem);
    }
}
