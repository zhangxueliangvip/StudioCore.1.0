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
    public class UsersRepository : IUsersRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(UsersModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(UsersModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public UsersModels InsertEntity(UsersModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(UsersModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<UsersModels> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(UsersModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<UsersModels> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(UsersModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<UsersModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<UsersModels> list)
        {
            return DBCore.Deleteable<UsersModels>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<UsersModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<UsersModels> GetTopList(int num)
        {
            return DBCore.Queryable<UsersModels>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<UsersModels>();
        }

        public UsersModels GetById(int id)
        {
            return DBCore.Queryable<UsersModels>().With(SqlWith.NoLock).InSingle(id) ?? new UsersModels();
        }

        public List<UsersModels> GetAll()
        {
            return DBCore.Queryable<UsersModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<UsersModels>();
        }


        public UsersModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<UsersModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new UsersModels();
        }

        #endregion

        #region Business
        public UsersModels GetUsersByName(string name)
        {
            return DBCore.Queryable<UsersModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.UserName == name).First() ?? new UsersModels();
        }
        public void AddUsersPages(sy_userspages entity)
        {
            entity.CTime = DateTime.Now;
            entity.IsDeleted = false;
            entity.OpenId = Guid.NewGuid().ToString();
            entity.Remark = "用户页面数据";
            entity.UTime = DateTime.Now;
            DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public void AddUsersRoles(sy_usersroles entity)
        {
            entity.CTime = DateTime.Now;
            entity.IsDeleted = false;
            entity.OpenId = Guid.NewGuid().ToString();
            entity.Remark = "用户角色数据";
            entity.UTime = DateTime.Now;
            DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public void DelUsersRoles(string usersOpenId)
        {
            DBCore.Deleteable<sy_usersroles>().With(SqlWith.RowLock).Where(m => m.UsersOpenId == usersOpenId).ExecuteCommand();
        }
        public void DelUsersPages(string usersOpenId)
        {
            DBCore.Deleteable<sy_userspages>().With(SqlWith.RowLock).Where(m => m.UsersOpenId == usersOpenId).ExecuteCommand();
        }
        #endregion

    }
}
