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
    public interface IUsersRepository : IBaseRepository<UsersModels>
    {
        #region Business
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int InsertList(List<UsersQueryItem> list);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateList(List<UsersQueryItem> list);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteList(List<UsersQueryItem> list);
        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<UsersQueryItem> GetAll();
        /// <summary>
        /// 取前几条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        List<UsersQueryItem> GetTopList(int num);
        /// <summary>
        /// 根据用户名查实体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        UsersModels GetUsersByName(string name);
        /// <summary>
        /// 添加用户页面
        /// </summary>
        /// <param name="entity"></param>
        void AddUsersPages(sy_userspages entity);
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="entity"></param>
        void AddUsersRoles(sy_usersroles entity);
        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="usersOpenId"></param>
        void DelUsersRoles(string usersOpenId);
        /// <summary>
        /// 删除用户页面
        /// </summary>
        /// <param name="usersOpenId"></param>
        void DelUsersPages(string usersOpenId);
        #endregion
    }
}
