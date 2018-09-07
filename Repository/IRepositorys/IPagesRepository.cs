using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using Domain.QueryModels;
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
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int InsertList(List<PagesQueryItem> list);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateList(List<PagesQueryItem> list);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteList(List<PagesQueryItem> list);
        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<PagesQueryItem> GetAll();
        /// <summary>
        /// 取前几条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        List<PagesQueryItem> GetTopList(int num);
        /// <summary>
        /// 获取页面全部数据（顶级）
        /// </summary>
        /// <returns></returns>
        List<PagesQueryItem> GetPagesAll();
        /// <summary>
        /// 获取页面子级集合
        /// </summary>
        /// <returns></returns>
        List<PagesQueryItem> GetPagesChildList(int id);
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

