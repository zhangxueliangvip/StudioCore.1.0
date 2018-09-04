using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositorys
{
    public interface IPagesRepository : IBaseRepository<PagesModels>
    {
        #region Business
        /// <summary>
        /// 获取页面全部数据（顶级）
        /// </summary>
        /// <returns></returns>
        List<PagesModels> GetPagesAll();
        /// <summary>
        /// 添加页面操作
        /// </summary>
        /// <param name="entity"></param>
        void AddPagesOperations(sy_pagesoperations entity);
        /// <summary>
        /// 删除页面操作
        /// </summary>
        /// <param name="usersOpenId"></param>
        void DelPagesOperations(string usersOpenId);
        #endregion
    }
}

