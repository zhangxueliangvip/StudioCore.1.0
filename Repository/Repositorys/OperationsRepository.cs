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
    public class OperationsRepository : IOperationsRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(OperationsModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(OperationsModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public OperationsModels InsertEntity(OperationsModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(OperationsModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<OperationsModels> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(OperationsModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<OperationsModels> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(OperationsModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<OperationsModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<OperationsModels> list)
        {
            return DBCore.Deleteable<OperationsModels>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<OperationsModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<OperationsModels> GetTopList(int num)
        {
            return DBCore.Queryable<OperationsModels>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<OperationsModels>();
        }

        public OperationsModels GetById(int id)
        {
            return DBCore.Queryable<OperationsModels>().With(SqlWith.NoLock).InSingle(id) ?? new OperationsModels();
        }

        public List<OperationsModels> GetAll()
        {
            return DBCore.Queryable<OperationsModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<OperationsModels>();
        }


        public OperationsModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<OperationsModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new OperationsModels();
        }



        #endregion

        #region Business
       
        #endregion

    }
}
