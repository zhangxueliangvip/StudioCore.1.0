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
    public interface IIconsRepository : IBaseRepository<IconModels>
    {
        #region Business
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int InsertList(List<IconQueryItem> list);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int UpdateList(List<IconQueryItem> list);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteList(List<IconQueryItem> list);
        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<IconQueryItem> GetAll();
        /// <summary>
        /// 取前几条
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        List<IconQueryItem> GetTopList(int num);
        #endregion
    }
}

