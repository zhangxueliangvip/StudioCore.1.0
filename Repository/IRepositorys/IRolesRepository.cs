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
    public interface IRolesRepository : IBaseRepository<RolesModels>
    {
        #region Business
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

