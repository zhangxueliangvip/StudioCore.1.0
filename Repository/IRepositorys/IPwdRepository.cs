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
    public interface IPwdRepository : IBaseRepository<PwdModels>
    {
        #region Business
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

