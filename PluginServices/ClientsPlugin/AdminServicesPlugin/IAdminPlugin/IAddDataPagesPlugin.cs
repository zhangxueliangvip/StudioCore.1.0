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
   public interface IAddDataPagesPlugin
    {
        /// <summary>
        /// 添加页面数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        BaseResult AddDataPages(PagesModels entity);
    }
}
