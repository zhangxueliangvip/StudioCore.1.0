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
    public interface IPwdRepository : IBaseRepository<PwdModels>
    {
        #region Business
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int InsertList(List<PwdQueryItem> list);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateList(List<PwdQueryItem> list);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteList(List<PwdQueryItem> list);
        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<PwdQueryItem> GetAll();
        /// <summary>
        /// 取前几条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        List<PwdQueryItem> GetTopList(int num);
        /// <summary>
        /// 密码查询实体
        /// </summary>
        /// <param name="pwd">加密密码</param>
        /// <returns></returns>
        PwdModels GetEntityByPwd(string pwd);
        /// <summary>
        /// 根据用户获取密码
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <returns></returns>
         PwdModels GetEntityUserOpenId(string userOpenId);
        #endregion
    }
}

