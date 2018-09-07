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
    public interface IRolesRepository : IBaseRepository<RolesModels>
    {
        #region Business
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int InsertList(List<RolesQueryItem> list);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateList(List<RolesQueryItem> list);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteList(List<RolesQueryItem> list);
        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<RolesQueryItem> GetAll();
        /// <summary>
        /// 取前几条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        List<RolesQueryItem> GetTopList(int num);
        /// <summary>
        /// 添加角色页面数据
        /// </summary>
        /// <param name="entity"></param>
        void AddRolesPages(sy_rolespages entity);
        /// <summary>
        /// 删除角色页面数据
        /// </summary>
        /// <param name="rolesOpenId"></param>
        void DelRolesPages(string rolesOpenId);
        #endregion
    }
}

