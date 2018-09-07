using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using Domain.QueryModels;
using IRepositorys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public class PwdRepository : IPwdRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(PwdModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(PwdModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public PwdModels InsertEntity(PwdModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(PwdModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<PwdQueryItem> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(PwdModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<PwdQueryItem> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(PwdModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<PwdModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<PwdQueryItem> list)
        {
            return DBCore.Deleteable<PwdQueryItem>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<PwdModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<PwdQueryItem> GetTopList(int num)
        {
            return DBCore.Queryable<PwdQueryItem>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<PwdQueryItem>();
        }

        public PwdModels GetById(int id)
        {
            return DBCore.Queryable<PwdModels>().With(SqlWith.NoLock).InSingle(id) ?? new PwdModels();
        }

        public List<PwdQueryItem> GetAll()
        {
            return DBCore.Queryable<PwdQueryItem>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<PwdQueryItem>();
        }


        public PwdModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<PwdModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new PwdModels();
        }



        #endregion

        #region Business
        public PwdModels GetEntityByPwd(string pwd)
        {
            return DBCore.Queryable<PwdModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.PassWords == pwd).First() ?? new PwdModels();
        }
        public PwdModels GetEntityUserOpenId(string userOpenId)
        {
            return DBCore.Queryable<PwdModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.JoinOpenId == userOpenId).First() ?? new PwdModels();
        }
        #endregion

    }
}
