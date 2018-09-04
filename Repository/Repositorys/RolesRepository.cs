using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using IRepositorys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public class RolesRepository : IRolesRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(RolesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(RolesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public RolesModels InsertEntity(RolesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(RolesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<RolesModels> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(RolesModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<RolesModels> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(RolesModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<RolesModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<RolesModels> list)
        {
            return DBCore.Deleteable<RolesModels>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<RolesModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<RolesModels> GetTopList(int num)
        {
            return DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<RolesModels>();
        }

        public RolesModels GetById(int id)
        {
            return DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).InSingle(id) ?? new RolesModels();
        }

        public List<RolesModels> GetAll()
        {
            return DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<RolesModels>();
        }


        public RolesModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new RolesModels();
        }


        #endregion

        #region Business
        public void AddRolesPages(sy_rolespages entity)
        {
            entity.CTime = DateTime.Now;
            entity.IsDeleted = false;
            entity.OpenId = Guid.NewGuid().ToString();
            entity.Remark = "角色页面数据";
            entity.UTime = DateTime.Now;
            DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }

        public void DelRolesPages(string rolesOpenId)
        {
            DBCore.Deleteable<sy_rolespages>().With(SqlWith.RowLock).Where(m => m.RolesOpenId == rolesOpenId).ExecuteCommand();
        }
        #endregion

    }
}
