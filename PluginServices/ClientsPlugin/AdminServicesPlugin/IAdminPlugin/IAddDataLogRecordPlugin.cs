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
   public interface IAddDataLogRecordPlugin
    {
        /// <summary>
        /// 添加日志记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResult AddDataLogRecord(LogRecordModels entity);
    }
}
